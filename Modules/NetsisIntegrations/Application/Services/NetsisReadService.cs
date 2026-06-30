using System.Globalization;
using Microsoft.Data.SqlClient;
using salesdesk_api.Modules.NetsisIntegrations.Application.Dtos;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.NetsisIntegrations.Application.Services;

public sealed class NetsisReadService : INetsisReadService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<NetsisReadService> _logger;

    public NetsisReadService(
        IConfiguration configuration,
        ILogger<NetsisReadService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<ApiResponse<List<BranchDto>>> GetBranchesAsync(
        int? branchNo = null,
        CancellationToken cancellationToken = default)
    {
        var connectionString = ResolveReadConnectionString();
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return ApiResponse<List<BranchDto>>.ErrorResult(
                "ERP bağlantısı yapılandırılmadı.",
                "ConnectionStrings:ErpConnection veya ConnectionStrings:DefaultConnection boş.",
                StatusCodes.Status503ServiceUnavailable);
        }

        try
        {
            var branches = new List<BranchDto>();

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT SUBE_KODU, UNVAN FROM dbo.RII_FN_BRANCHES(@branchNo)";
            command.CommandTimeout = 60;
            command.Parameters.Add(new SqlParameter("@branchNo", branchNo.HasValue ? branchNo.Value : DBNull.Value));

            await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
            var branchOrdinal = reader.GetOrdinal("SUBE_KODU");
            var titleOrdinal = reader.GetOrdinal("UNVAN");

            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
            {
                branches.Add(new BranchDto
                {
                    SubeKodu = Convert.ToInt16(reader.GetValue(branchOrdinal), CultureInfo.InvariantCulture),
                    Unvan = reader.IsDBNull(titleOrdinal) ? null : reader.GetString(titleOrdinal)
                });
            }

            return ApiResponse<List<BranchDto>>.SuccessResult(
                branches
                    .OrderBy(x => x.SubeKodu)
                    .ToList(),
                "Şubeler başarıyla getirildi.");
        }
        catch (SqlException ex) when (IsMissingBranchesObject(ex))
        {
            _logger.LogWarning(ex, "ERP branch function dbo.RII_FN_BRANCHES bulunamadı. BranchNo: {BranchNo}", branchNo);
            return ApiResponse<List<BranchDto>>.ErrorResult(
                "ERP şube fonksiyonu bulunamadı.",
                "dbo.RII_FN_BRANCHES ERP veritabanında oluşturulmalı.",
                StatusCodes.Status500InternalServerError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ERP şube listesi okunamadı. BranchNo: {BranchNo}", branchNo);
            return ApiResponse<List<BranchDto>>.ErrorResult(
                "Şubeler yüklenemedi.",
                ex.Message,
                StatusCodes.Status500InternalServerError);
        }
    }

    private string? ResolveReadConnectionString()
    {
        return _configuration.GetConnectionString("ErpConnection")
            ?? _configuration.GetConnectionString("DefaultConnection");
    }

    private static bool IsMissingBranchesObject(SqlException ex)
    {
        return ex.Number == 208
            || ex.Message.Contains("RII_FN_BRANCHES", StringComparison.OrdinalIgnoreCase)
            && ex.Message.Contains("Invalid object name", StringComparison.OrdinalIgnoreCase);
    }
}
