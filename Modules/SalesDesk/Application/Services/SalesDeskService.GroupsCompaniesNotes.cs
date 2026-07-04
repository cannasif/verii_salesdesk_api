using Microsoft.EntityFrameworkCore;
using salesdesk_api.Helpers;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Domain.Entities;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Application.Services;

public partial class SalesDeskService
{
    public async Task<ApiResponse<List<SalesDeskGroupDto>>> GetGroupsAsync(CancellationToken cancellationToken = default)
    {
        var groups = await _db.SalesDeskGroups.AsNoTracking()
            .Include(x => x.Members.Where(m => !m.IsDeleted))
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return ApiResponse<List<SalesDeskGroupDto>>.SuccessResult(groups.Select(ToGroupDto).ToList(), "Gruplar getirildi.");
    }

    public async Task<ApiResponse<SalesDeskGroupDto>> GetGroupAsync(long id, CancellationToken cancellationToken = default)
    {
        var group = await LoadGroupQuery().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        return group == null
            ? NotFound<SalesDeskGroupDto>("Grup bulunamadi.")
            : ApiResponse<SalesDeskGroupDto>.SuccessResult(ToGroupDto(group), "Grup getirildi.");
    }

    public async Task<ApiResponse<SalesDeskGroupDto>> CreateGroupAsync(SalesDeskGroupCreateDto request, CancellationToken cancellationToken = default)
    {
        var name = request.Name.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            return ApiResponse<SalesDeskGroupDto>.ErrorResult("Grup adi zorunludur.", statusCode: StatusCodes.Status400BadRequest);
        }

        var entity = new SalesDeskGroup
        {
            Name = name,
            Description = request.Description?.Trim()
        };

        await _db.SalesDeskGroups.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        await ReplaceGroupMembersAsync(entity, NormalizeUserIds(request.MemberUserIds), cancellationToken);

        var created = await LoadGroupQuery().FirstAsync(x => x.Id == entity.Id, cancellationToken);
        var response = ApiResponse<SalesDeskGroupDto>.SuccessResult(ToGroupDto(created), "Grup olusturuldu.");
        response.StatusCode = StatusCodes.Status201Created;
        return response;
    }

    public async Task<ApiResponse<SalesDeskGroupDto>> UpdateGroupAsync(long id, SalesDeskGroupUpdateDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskGroups.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskGroupDto>("Grup bulunamadi.");

        entity.Name = request.Name.Trim();
        entity.Description = request.Description?.Trim();
        entity.UpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);

        var updated = await LoadGroupQuery().FirstAsync(x => x.Id == id, cancellationToken);
        return ApiResponse<SalesDeskGroupDto>.SuccessResult(ToGroupDto(updated), "Grup guncellendi.");
    }

    public async Task<ApiResponse<SalesDeskGroupDto>> SetGroupMembersAsync(long id, SalesDeskGroupMembersDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskGroups.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskGroupDto>("Grup bulunamadi.");

        await ReplaceGroupMembersAsync(entity, NormalizeUserIds(request.MemberUserIds), cancellationToken);
        entity.UpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);

        var updated = await LoadGroupQuery().FirstAsync(x => x.Id == id, cancellationToken);
        return ApiResponse<SalesDeskGroupDto>.SuccessResult(ToGroupDto(updated), "Grup uyeleri guncellendi.");
    }

    public async Task<ApiResponse<object>> DeleteGroupAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskGroups
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null)
        {
            return ApiResponse<object>.ErrorResult("Grup bulunamadi.", statusCode: StatusCodes.Status404NotFound);
        }

        var now = DateTime.UtcNow;
        entity.IsDeleted = true;
        entity.DeletedDate = now;
        foreach (var member in entity.Members.Where(x => !x.IsDeleted))
        {
            member.IsDeleted = true;
            member.DeletedDate = now;
        }

        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<object>.SuccessResult(new { id }, "Grup silindi.");
    }

    public Task<ApiResponse<PagedResponse<SalesDeskCompanyDto>>> GetCompaniesAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskCompanies.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search,
                nameof(SalesDeskCompany.Name),
                nameof(SalesDeskCompany.IpAddress),
                nameof(SalesDeskCompany.VpnName),
                nameof(SalesDeskCompany.LoginUrl),
                nameof(SalesDeskCompany.Description),
                nameof(SalesDeskCompany.Description1));

        return PageAsync(query, request, ToCompanyDto, cancellationToken, nameof(SalesDeskCompany.Name));
    }

    public Task<ApiResponse<SalesDeskCompanyDto>> GetCompanyAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskCompanies.AsNoTracking(), id, ToCompanyDto, "Sirket bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskCompanyDto>> CreateCompanyAsync(SalesDeskCompanyUpsertDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return ApiResponse<SalesDeskCompanyDto>.ErrorResult("Sirket adi zorunludur.", statusCode: StatusCodes.Status400BadRequest);
        }

        var entity = new SalesDeskCompany();
        ApplyCompany(request, entity);
        return await AddAsync(entity, ToCompanyDto, "Sirket olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskCompanyDto>> UpdateCompanyAsync(long id, SalesDeskCompanyUpsertDto request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return ApiResponse<SalesDeskCompanyDto>.ErrorResult("Sirket adi zorunludur.", statusCode: StatusCodes.Status400BadRequest);
        }

        var entity = await _db.SalesDeskCompanies.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskCompanyDto>("Sirket bulunamadi.");
        ApplyCompany(request, entity);
        return await SaveUpdatedAsync(entity, ToCompanyDto, "Sirket guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteCompanyAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskCompanies, id, "Sirket silindi.", cancellationToken);

    public async Task<ApiResponse<List<SalesDeskNoteDto>>> GetNotesForUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        var notes = await _db.SalesDeskNotes.AsNoTracking()
            .Include(x => x.Recipients.Where(r => !r.IsDeleted))
            .Where(x => !x.IsDeleted && (x.CreatedByUserId == userId || x.Recipients.Any(r => !r.IsDeleted && r.UserId == userId)))
            .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
            .ToListAsync(cancellationToken);

        return ApiResponse<List<SalesDeskNoteDto>>.SuccessResult(notes.Select(ToNoteDto).ToList(), "Notlar getirildi.");
    }

    public async Task<ApiResponse<SalesDeskNoteDto>> GetNoteAsync(long id, CancellationToken cancellationToken = default)
    {
        var note = await LoadNoteQuery().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        return note == null
            ? NotFound<SalesDeskNoteDto>("Not bulunamadi.")
            : ApiResponse<SalesDeskNoteDto>.SuccessResult(ToNoteDto(note), "Not getirildi.");
    }

    public async Task<ApiResponse<SalesDeskNoteDto>> CreateNoteAsync(SalesDeskNoteCreateDto request, CancellationToken cancellationToken = default)
    {
        var title = request.Title.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            return ApiResponse<SalesDeskNoteDto>.ErrorResult("Not basligi zorunludur.", statusCode: StatusCodes.Status400BadRequest);
        }

        if (request.CreatedByUserId <= 0)
        {
            return ApiResponse<SalesDeskNoteDto>.ErrorResult("Olusturan kullanici gerekli.", statusCode: StatusCodes.Status400BadRequest);
        }

        var entity = new SalesDeskNote
        {
            Title = title,
            Content = request.Content?.Trim() ?? string.Empty,
            CreatedByUserId = request.CreatedByUserId,
            CreatedByName = request.CreatedByName.Trim()
        };

        var recipientIds = NormalizeUserIds(request.RecipientUserIds);
        foreach (var userId in recipientIds)
        {
            entity.Recipients.Add(new SalesDeskNoteRecipient { UserId = userId });
        }

        await _db.SalesDeskNotes.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        if (request.NotifyRecipients)
        {
            await QueueNoteNotificationsAsync(entity, recipientIds, request.CreatedByUserId, cancellationToken);
        }

        var created = await LoadNoteQuery().FirstAsync(x => x.Id == entity.Id, cancellationToken);
        var response = ApiResponse<SalesDeskNoteDto>.SuccessResult(ToNoteDto(created), "Not olusturuldu.");
        response.StatusCode = StatusCodes.Status201Created;
        return response;
    }

    public async Task<ApiResponse<SalesDeskNoteDto>> UpdateNoteAsync(long id, SalesDeskNoteUpdateDto request, CancellationToken cancellationToken = default)
    {
        var title = request.Title.Trim();
        if (string.IsNullOrWhiteSpace(title))
        {
            return ApiResponse<SalesDeskNoteDto>.ErrorResult("Not basligi zorunludur.", statusCode: StatusCodes.Status400BadRequest);
        }

        var entity = await _db.SalesDeskNotes
            .Include(x => x.Recipients)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskNoteDto>("Not bulunamadi.");

        entity.Title = title;
        entity.Content = request.Content?.Trim() ?? string.Empty;
        entity.UpdatedDate = DateTime.UtcNow;

        var recipientIds = NormalizeUserIds(request.RecipientUserIds);
        await ReplaceNoteRecipientsAsync(entity, recipientIds, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        if (request.NotifyRecipients)
        {
            await QueueNoteNotificationsAsync(entity, recipientIds, entity.CreatedByUserId, cancellationToken);
        }

        var updated = await LoadNoteQuery().FirstAsync(x => x.Id == id, cancellationToken);
        return ApiResponse<SalesDeskNoteDto>.SuccessResult(ToNoteDto(updated), "Not guncellendi.");
    }

    public async Task<ApiResponse<object>> DeleteNoteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskNotes
            .Include(x => x.Recipients)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null)
        {
            return ApiResponse<object>.ErrorResult("Not bulunamadi.", statusCode: StatusCodes.Status404NotFound);
        }

        var now = DateTime.UtcNow;
        entity.IsDeleted = true;
        entity.DeletedDate = now;
        foreach (var recipient in entity.Recipients.Where(x => !x.IsDeleted))
        {
            recipient.IsDeleted = true;
            recipient.DeletedDate = now;
        }

        var notifications = await _db.SalesDeskNoteNotifications
            .Where(x => !x.IsDeleted && x.NoteId == id)
            .ToListAsync(cancellationToken);
        foreach (var notification in notifications)
        {
            notification.IsDeleted = true;
            notification.DeletedDate = now;
        }

        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<object>.SuccessResult(new { id }, "Not silindi.");
    }

    public async Task<ApiResponse<List<SalesDeskNoteNotificationDto>>> PullPendingNoteNotificationsAsync(long userId, CancellationToken cancellationToken = default)
    {
        if (userId <= 0)
        {
            return ApiResponse<List<SalesDeskNoteNotificationDto>>.SuccessResult(new List<SalesDeskNoteNotificationDto>(), "Bildirim yok.");
        }

        var pending = await _db.SalesDeskNoteNotifications
            .Where(x => !x.IsDeleted && x.RecipientUserId == userId && x.DeliveredAt == null)
            .OrderBy(x => x.CreatedDate)
            .ToListAsync(cancellationToken);

        if (pending.Count == 0)
        {
            return ApiResponse<List<SalesDeskNoteNotificationDto>>.SuccessResult(new List<SalesDeskNoteNotificationDto>(), "Bildirim yok.");
        }

        var now = DateTime.UtcNow;
        foreach (var item in pending)
        {
            item.DeliveredAt = now;
            item.UpdatedDate = now;
        }

        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<List<SalesDeskNoteNotificationDto>>.SuccessResult(
            pending.Select(ToNoteNotificationDto).ToList(),
            "Bekleyen not bildirimleri getirildi.");
    }

    public async Task<ApiResponse<object>> AcknowledgeNoteNotificationAsync(long id, CancellationToken cancellationToken = default)
    {
        var notification = await _db.SalesDeskNoteNotifications
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (notification == null)
        {
            return ApiResponse<object>.ErrorResult("Bildirim bulunamadi.", statusCode: StatusCodes.Status404NotFound);
        }

        notification.DeliveredAt ??= DateTime.UtcNow;
        notification.UpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<object>.SuccessResult(new { id }, "Bildirim onaylandi.");
    }

    private IQueryable<SalesDeskGroup> LoadGroupQuery() =>
        _db.SalesDeskGroups.AsNoTracking().Include(x => x.Members.Where(m => !m.IsDeleted));

    private IQueryable<SalesDeskNote> LoadNoteQuery() =>
        _db.SalesDeskNotes.AsNoTracking().Include(x => x.Recipients.Where(r => !r.IsDeleted));

    private async Task ReplaceGroupMembersAsync(SalesDeskGroup group, IReadOnlyCollection<long> memberUserIds, CancellationToken cancellationToken)
    {
        var allMembers = await _db.SalesDeskGroupMembers
            .Where(x => x.GroupId == group.Id)
            .ToListAsync(cancellationToken);

        var now = DateTime.UtcNow;
        foreach (var member in allMembers)
        {
            if (memberUserIds.Contains(member.UserId))
            {
                if (member.IsDeleted)
                {
                    member.IsDeleted = false;
                    member.DeletedDate = null;
                    member.UpdatedDate = now;
                }
            }
            else if (!member.IsDeleted)
            {
                member.IsDeleted = true;
                member.DeletedDate = now;
            }
        }

        var knownUserIds = allMembers.Select(x => x.UserId).ToHashSet();
        foreach (var userId in memberUserIds.Where(x => !knownUserIds.Contains(x)))
        {
            await _db.SalesDeskGroupMembers.AddAsync(new SalesDeskGroupMember
            {
                GroupId = group.Id,
                UserId = userId
            }, cancellationToken);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    private async Task ReplaceNoteRecipientsAsync(SalesDeskNote note, IReadOnlyCollection<long> recipientUserIds, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        foreach (var recipient in note.Recipients)
        {
            if (recipientUserIds.Contains(recipient.UserId))
            {
                if (recipient.IsDeleted)
                {
                    recipient.IsDeleted = false;
                    recipient.DeletedDate = null;
                    recipient.UpdatedDate = now;
                }
            }
            else if (!recipient.IsDeleted)
            {
                recipient.IsDeleted = true;
                recipient.DeletedDate = now;
            }
        }

        var knownUserIds = note.Recipients.Select(x => x.UserId).ToHashSet();
        foreach (var userId in recipientUserIds.Where(x => !knownUserIds.Contains(x)))
        {
            note.Recipients.Add(new SalesDeskNoteRecipient { UserId = userId });
        }

        await Task.CompletedTask;
    }

    private async Task QueueNoteNotificationsAsync(
        SalesDeskNote note,
        IReadOnlyCollection<long> recipientUserIds,
        long actorUserId,
        CancellationToken cancellationToken)
    {
        var preview = string.IsNullOrWhiteSpace(note.Content)
            ? "Yeni bir not paylasildi."
            : note.Content.Trim();
        if (preview.Length > 160)
        {
            preview = preview[..160];
        }

        foreach (var recipientUserId in recipientUserIds.Where(x => x != actorUserId))
        {
            await _db.SalesDeskNoteNotifications.AddAsync(new SalesDeskNoteNotification
            {
                NoteId = note.Id,
                RecipientUserId = recipientUserId,
                Title = note.Title,
                Message = preview,
                CreatedByUserId = note.CreatedByUserId,
                CreatedByName = note.CreatedByName
            }, cancellationToken);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    private static List<long> NormalizeUserIds(IEnumerable<long>? userIds)
    {
        if (userIds == null) return new List<long>();
        return userIds.Where(x => x > 0).Distinct().ToList();
    }

    private static void ApplyCompany(SalesDeskCompanyUpsertDto source, SalesDeskCompany target)
    {
        static string OrEmpty(string? value) => value?.Trim() ?? string.Empty;

        target.Name = source.Name.Trim();
        target.IpAddress = OrEmpty(source.IpAddress);
        target.IpUsername = OrEmpty(source.IpUsername);
        target.IpPassword = OrEmpty(source.IpPassword);
        target.VpnName = OrEmpty(source.VpnName);
        target.VpnUsername = OrEmpty(source.VpnUsername);
        target.VpnPassword = OrEmpty(source.VpnPassword);
        target.VpnIpAddress = OrEmpty(source.VpnIpAddress);
        target.VpnPort = OrEmpty(source.VpnPort);
        target.DatabaseUsername = OrEmpty(source.DatabaseUsername);
        target.DatabasePassword = OrEmpty(source.DatabasePassword);
        target.LoginUrl = OrEmpty(source.LoginUrl);
        target.Description = OrEmpty(source.Description);
        target.Description1 = OrEmpty(source.Description1);
    }

    private static SalesDeskGroupDto ToGroupDto(SalesDeskGroup group)
    {
        var memberUserIds = group.Members.Where(x => !x.IsDeleted).Select(x => x.UserId).Distinct().ToList();
        return new SalesDeskGroupDto
        {
            Id = group.Id,
            Name = group.Name,
            Description = group.Description ?? string.Empty,
            MemberUserIds = memberUserIds,
            MemberCount = memberUserIds.Count,
            CreatedAt = group.CreatedDate,
            UpdatedAt = group.UpdatedDate ?? group.CreatedDate
        };
    }

    private static SalesDeskCompanyDto ToCompanyDto(SalesDeskCompany company) => new()
    {
        Id = company.Id,
        Name = company.Name,
        IpAddress = company.IpAddress,
        IpUsername = company.IpUsername,
        IpPassword = company.IpPassword,
        VpnName = company.VpnName,
        VpnUsername = company.VpnUsername,
        VpnPassword = company.VpnPassword,
        VpnIpAddress = company.VpnIpAddress,
        VpnPort = company.VpnPort,
        DatabaseUsername = company.DatabaseUsername,
        DatabasePassword = company.DatabasePassword,
        LoginUrl = company.LoginUrl,
        Description = company.Description,
        Description1 = company.Description1,
        CreatedAt = company.CreatedDate,
        UpdatedAt = company.UpdatedDate ?? company.CreatedDate
    };

    private static SalesDeskNoteDto ToNoteDto(SalesDeskNote note)
    {
        var recipientUserIds = note.Recipients.Where(x => !x.IsDeleted).Select(x => x.UserId).Distinct().ToList();
        return new SalesDeskNoteDto
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            CreatedByUserId = note.CreatedByUserId,
            CreatedByName = note.CreatedByName,
            RecipientUserIds = recipientUserIds,
            CreatedAt = note.CreatedDate,
            UpdatedAt = note.UpdatedDate ?? note.CreatedDate
        };
    }

    private static SalesDeskNoteNotificationDto ToNoteNotificationDto(SalesDeskNoteNotification notification) => new()
    {
        Id = notification.Id,
        NoteId = notification.NoteId,
        RecipientUserId = notification.RecipientUserId,
        Title = notification.Title,
        Message = notification.Message,
        CreatedByUserId = notification.CreatedByUserId,
        CreatedByName = notification.CreatedByName,
        CreatedAt = notification.CreatedDate
    };
}
