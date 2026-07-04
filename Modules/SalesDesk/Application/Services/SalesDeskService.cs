using Microsoft.EntityFrameworkCore;
using salesdesk_api.Helpers;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Domain.Entities;
using salesdesk_api.Modules.SalesDesk.Domain.Enums;
using salesdesk_api.Shared.Common.Application;
using salesdesk_api.Shared.Domain.Entities.Common;
using salesdesk_api.Shared.Infrastructure.Persistence;

namespace salesdesk_api.Modules.SalesDesk.Application.Services;

public partial class SalesDeskService : ISalesDeskService
{
    private readonly SalesDeskDbContext _db;

    public SalesDeskService(SalesDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ApiResponse<SalesDeskDashboardDto>> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var today = now.Date;

        var dto = new SalesDeskDashboardDto
        {
            CustomerCount = await _db.SalesDeskCustomers.CountAsync(x => !x.IsDeleted, cancellationToken),
            PotentialCount = await _db.SalesDeskPotentialCustomers.CountAsync(x => !x.IsDeleted, cancellationToken),
            ProductCount = await _db.SalesDeskProducts.CountAsync(x => !x.IsDeleted, cancellationToken),
            OpenTaskCount = await _db.SalesDeskTasks.CountAsync(
                x => !x.IsDeleted &&
                     (x.Status == SalesDeskTaskStatus.Open || x.Status == SalesDeskTaskStatus.InProgress),
                cancellationToken),
            TodayVisitCount = await _db.SalesDeskVisits.CountAsync(x => !x.IsDeleted && x.VisitDate.Date == today, cancellationToken),
            PendingQuoteCount = await _db.SalesDeskQuotes.CountAsync(x => !x.IsDeleted && x.Status == SalesDeskDocumentStatus.Approved, cancellationToken),
            ToBeIssuedInvoiceCount = await _db.SalesDeskInvoices.CountAsync(x => !x.IsDeleted && x.Status == SalesDeskDocumentStatus.ToBeIssued, cancellationToken),
            MonthlySalesTotal = await _db.SalesDeskInvoices
                .Where(x => !x.IsDeleted &&
                            x.InvoiceType == SalesDeskInvoiceType.Sales &&
                            x.Status == SalesDeskDocumentStatus.Issued &&
                            x.InvoiceDate >= monthStart)
                .SumAsync(x => (decimal?)x.GrandTotal, cancellationToken) ?? 0
        };

        return ApiResponse<SalesDeskDashboardDto>.SuccessResult(dto, "SalesDesk dashboard verileri getirildi.");
    }

    public async Task<ApiResponse<List<SalesDeskSearchResultDto>>> SearchAsync(string? query, int take = 12, CancellationToken cancellationToken = default)
    {
        take = Math.Clamp(take, 1, 30);
        var results = new List<SalesDeskSearchResultDto>();

        var customers = await _db.SalesDeskCustomers.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(query, nameof(SalesDeskCustomer.Code), nameof(SalesDeskCustomer.Name), nameof(SalesDeskCustomer.ContactName), nameof(SalesDeskCustomer.Email), nameof(SalesDeskCustomer.Phone))
            .OrderBy(x => x.Name)
            .Take(take)
            .Select(x => new SalesDeskSearchResultDto { Type = "customer", Id = x.Id, Code = x.Code, Title = x.Name, Subtitle = x.ContactName, Url = $"/salesdesk/customers/{x.Id}" })
            .ToListAsync(cancellationToken);
        results.AddRange(customers);

        if (results.Count < take)
        {
            var products = await _db.SalesDeskProducts.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ApplySearch(query, nameof(SalesDeskProduct.Code), nameof(SalesDeskProduct.Name), nameof(SalesDeskProduct.Category), nameof(SalesDeskProduct.SearchText))
                .OrderBy(x => x.Name)
                .Take(take - results.Count)
                .Select(x => new SalesDeskSearchResultDto { Type = "product", Id = x.Id, Code = x.Code, Title = x.Name, Subtitle = x.Category, Url = $"/salesdesk/products/{x.Id}" })
                .ToListAsync(cancellationToken);
            results.AddRange(products);
        }

        if (results.Count < take)
        {
            var quotes = await _db.SalesDeskQuotes.AsNoTracking()
                .Include(x => x.Customer)
                .Where(x => !x.IsDeleted)
                .ApplySearch(query, nameof(SalesDeskQuote.QuoteNumber), "Customer.Name")
                .OrderByDescending(x => x.QuoteDate)
                .Take(take - results.Count)
                .Select(x => new SalesDeskSearchResultDto { Type = "quote", Id = x.Id, Code = x.QuoteNumber, Title = x.QuoteNumber, Subtitle = x.Customer != null ? x.Customer.Name : null, Url = $"/salesdesk/quotes/{x.Id}" })
                .ToListAsync(cancellationToken);
            results.AddRange(quotes);
        }

        if (results.Count < take)
        {
            var invoices = await _db.SalesDeskInvoices.AsNoTracking()
                .Include(x => x.Customer)
                .Where(x => !x.IsDeleted)
                .ApplySearch(query, nameof(SalesDeskInvoice.InvoiceNumber), "Customer.Name")
                .OrderByDescending(x => x.InvoiceDate)
                .Take(take - results.Count)
                .Select(x => new SalesDeskSearchResultDto { Type = "invoice", Id = x.Id, Code = x.InvoiceNumber, Title = x.InvoiceNumber, Subtitle = x.Customer != null ? x.Customer.Name : null, Url = $"/salesdesk/invoices/{x.Id}" })
                .ToListAsync(cancellationToken);
            results.AddRange(invoices);
        }

        return ApiResponse<List<SalesDeskSearchResultDto>>.SuccessResult(results, "SalesDesk arama sonuclari getirildi.");
    }

    public Task<ApiResponse<PagedResponse<SalesDeskCustomerDto>>> GetCustomersAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskCustomers.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskCustomer.Code), nameof(SalesDeskCustomer.Name), nameof(SalesDeskCustomer.ContactName), nameof(SalesDeskCustomer.Email), nameof(SalesDeskCustomer.Phone), nameof(SalesDeskCustomer.City));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskCustomer.Id));
    }

    public Task<ApiResponse<SalesDeskCustomerDto>> GetCustomerAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskCustomers.AsNoTracking(), id, ToDto, "Cari bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskCustomerDto>> CreateCustomerAsync(SalesDeskCustomerUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskCustomer();
        Apply(request, entity);
        entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? await NextCodeAsync("CR", _db.SalesDeskCustomers, x => x.Code, cancellationToken) : entity.Code.Trim();
        return await AddAsync(entity, ToDto, "Cari olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskCustomerDto>> UpdateCustomerAsync(long id, SalesDeskCustomerUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskCustomers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskCustomerDto>("Cari bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Cari guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteCustomerAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskCustomers, id, "Cari silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskPotentialCustomerDto>>> GetPotentialCustomersAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskPotentialCustomers.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskPotentialCustomer.Code), nameof(SalesDeskPotentialCustomer.CompanyName), nameof(SalesDeskPotentialCustomer.ContactName), nameof(SalesDeskPotentialCustomer.Email), nameof(SalesDeskPotentialCustomer.Phone), nameof(SalesDeskPotentialCustomer.City));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskPotentialCustomer.Id));
    }

    public Task<ApiResponse<SalesDeskPotentialCustomerDto>> GetPotentialCustomerAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskPotentialCustomers.AsNoTracking(), id, ToDto, "Potansiyel cari bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskPotentialCustomerDto>> CreatePotentialCustomerAsync(SalesDeskPotentialCustomerUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskPotentialCustomer();
        Apply(request, entity);
        entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? await NextCodeAsync("POT", _db.SalesDeskPotentialCustomers, x => x.Code, cancellationToken) : entity.Code.Trim();
        return await AddAsync(entity, ToDto, "Potansiyel cari olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskPotentialCustomerDto>> UpdatePotentialCustomerAsync(long id, SalesDeskPotentialCustomerUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskPotentialCustomers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskPotentialCustomerDto>("Potansiyel cari bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Potansiyel cari guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeletePotentialCustomerAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskPotentialCustomers, id, "Potansiyel cari silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskProductDto>>> GetProductsAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskProducts.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskProduct.Code), nameof(SalesDeskProduct.Name), nameof(SalesDeskProduct.Category), nameof(SalesDeskProduct.SearchText));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskProduct.Id));
    }

    public Task<ApiResponse<SalesDeskProductDto>> GetProductAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskProducts.AsNoTracking(), id, ToDto, "Urun bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskProductDto>> CreateProductAsync(SalesDeskProductUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskProduct();
        Apply(request, entity);
        entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? await NextCodeAsync("STK", _db.SalesDeskProducts, x => x.Code, cancellationToken) : entity.Code.Trim();
        return await AddAsync(entity, ToDto, "Urun olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskProductDto>> UpdateProductAsync(long id, SalesDeskProductUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskProducts.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskProductDto>("Urun bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Urun guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteProductAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskProducts, id, "Urun silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskProductCustomerDto>>> GetProductCustomersAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskProductCustomers.AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .Include(x => x.PotentialCustomer)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, "Product.Code", "Product.Name", "Customer.Name", "PotentialCustomer.CompanyName");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskProductCustomer.Id));
    }

    public async Task<ApiResponse<SalesDeskProductCustomerDto>> CreateProductCustomerAsync(SalesDeskProductCustomerUpsertDto request, CancellationToken cancellationToken = default)
    {
        if (!request.CustomerId.HasValue && !request.PotentialCustomerId.HasValue)
        {
            return ApiResponse<SalesDeskProductCustomerDto>.ErrorResult("Cari veya potansiyel cari secilmelidir.", statusCode: StatusCodes.Status400BadRequest);
        }

        var entity = new SalesDeskProductCustomer { ProductId = request.ProductId, CustomerId = request.CustomerId, PotentialCustomerId = request.PotentialCustomerId };
        await _db.SalesDeskProductCustomers.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        var created = await _db.SalesDeskProductCustomers.AsNoTracking().Include(x => x.Product).Include(x => x.Customer).Include(x => x.PotentialCustomer).FirstAsync(x => x.Id == entity.Id, cancellationToken);
        return ApiResponse<SalesDeskProductCustomerDto>.SuccessResult(ToDto(created), "Urun cari baglantisi olusturuldu.");
    }

    public Task<ApiResponse<object>> DeleteProductCustomerAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskProductCustomers, id, "Urun cari baglantisi silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskQuoteDto>>> GetQuotesAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskQuotes.AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Lines).ThenInclude(x => x.Product)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskQuote.QuoteNumber), "Customer.Name", nameof(SalesDeskQuote.Notes));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskQuote.Id));
    }

    public Task<ApiResponse<SalesDeskQuoteDto>> GetQuoteAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskQuotes.AsNoTracking().Include(x => x.Customer).Include(x => x.Lines).ThenInclude(x => x.Product), id, ToDto, "Teklif bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskQuoteDto>> CreateQuoteAsync(SalesDeskQuoteUpsertDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new SalesDeskQuote();
            await ApplyQuoteAsync(request, entity, cancellationToken);
            entity.QuoteNumber = string.IsNullOrWhiteSpace(entity.QuoteNumber)
                ? await NextCodeAsync("TKL", _db.SalesDeskQuotes, x => x.QuoteNumber, cancellationToken)
                : entity.QuoteNumber.Trim();
            return await AddAsync(entity, ToDto, "Teklif olusturuldu.", cancellationToken);
        }
        catch (DbUpdateException ex) when (DbUpdateExceptionHelper.TryGetUniqueViolation(ex, out _))
        {
            return ApiResponse<SalesDeskQuoteDto>.ErrorResult(
                "Bu teklif numarasi zaten kayitli.",
                statusCode: StatusCodes.Status409Conflict);
        }
    }

    public async Task<ApiResponse<SalesDeskQuoteDto>> UpdateQuoteAsync(long id, SalesDeskQuoteUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskQuotes.Include(x => x.Lines).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskQuoteDto>("Teklif bulunamadi.");
        await ApplyQuoteAsync(request, entity, cancellationToken);
        return await SaveUpdatedAsync(entity, ToDto, "Teklif guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteQuoteAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskQuotes, id, "Teklif silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskInvoiceDto>>> GetInvoicesAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskInvoices.AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Quote)
            .Include(x => x.Lines).ThenInclude(x => x.Product)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskInvoice.InvoiceNumber), "Customer.Name", "Quote.QuoteNumber", nameof(SalesDeskInvoice.Notes));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskInvoice.Id));
    }

    public Task<ApiResponse<SalesDeskInvoiceDto>> GetInvoiceAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskInvoices.AsNoTracking().Include(x => x.Customer).Include(x => x.Quote).Include(x => x.Lines).ThenInclude(x => x.Product), id, ToDto, "Fatura bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskInvoiceDto>> CreateInvoiceAsync(SalesDeskInvoiceUpsertDto request, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new SalesDeskInvoice();
            await ApplyInvoiceAsync(request, entity, cancellationToken);
            entity.InvoiceNumber = string.IsNullOrWhiteSpace(entity.InvoiceNumber)
                ? await NextCodeAsync("FTR", _db.SalesDeskInvoices, x => x.InvoiceNumber, cancellationToken)
                : entity.InvoiceNumber.Trim();
            return await AddAsync(entity, ToDto, "Fatura olusturuldu.", cancellationToken);
        }
        catch (DbUpdateException ex) when (DbUpdateExceptionHelper.TryGetUniqueViolation(ex, out _))
        {
            return ApiResponse<SalesDeskInvoiceDto>.ErrorResult(
                "Bu fatura numarasi ve tipi icin kayit zaten mevcut.",
                statusCode: StatusCodes.Status409Conflict);
        }
    }

    public async Task<ApiResponse<SalesDeskInvoiceDto>> UpdateInvoiceAsync(long id, SalesDeskInvoiceUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskInvoices.Include(x => x.Lines).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskInvoiceDto>("Fatura bulunamadi.");
        await ApplyInvoiceAsync(request, entity, cancellationToken);
        return await SaveUpdatedAsync(entity, ToDto, "Fatura guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteInvoiceAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskInvoices, id, "Fatura silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskTaskDto>>> GetTasksAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskTasks.AsNoTracking()
            .Include(x => x.Customer)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskTask.Title), nameof(SalesDeskTask.Description), nameof(SalesDeskTask.GroupName), "Customer.Name");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskTask.Id));
    }

    public Task<ApiResponse<PagedResponse<SalesDeskTaskDto>>> GetOpenTasksAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskTasks.AsNoTracking()
            .Include(x => x.Customer)
            .Where(x => !x.IsDeleted &&
                        (x.Status == SalesDeskTaskStatus.Open || x.Status == SalesDeskTaskStatus.InProgress) &&
                        (x.GroupName == null ||
                         (x.GroupName != "HaftalikPlan" &&
                          !x.GroupName.StartsWith("HaftalikPlan|") &&
                          x.GroupName.ToLower() != "aktivite" &&
                          !x.GroupName.ToLower().StartsWith("aktivite|") &&
                          x.GroupName.ToLower() != "proje" &&
                          !x.GroupName.ToLower().StartsWith("proje|"))))
            .ApplySearch(request.Search, nameof(SalesDeskTask.Title), nameof(SalesDeskTask.Description), nameof(SalesDeskTask.GroupName), "Customer.Name");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskTask.DueDate));
    }

    public Task<ApiResponse<SalesDeskTaskDto>> GetTaskAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskTasks.AsNoTracking().Include(x => x.Customer), id, ToDto, "Madde bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskTaskDto>> CreateTaskAsync(SalesDeskTaskUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskTask();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "Madde olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskTaskDto>> UpdateTaskAsync(long id, SalesDeskTaskUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskTasks.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskTaskDto>("Madde bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Madde guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteTaskAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskTasks, id, "Madde silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskVisitDto>>> GetVisitsAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskVisits.AsNoTracking()
            .Include(x => x.Customer)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskVisit.Title), nameof(SalesDeskVisit.VisitType), nameof(SalesDeskVisit.Notes), "Customer.Name");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskVisit.Id));
    }

    public Task<ApiResponse<SalesDeskVisitDto>> GetVisitAsync(long id, CancellationToken cancellationToken = default) =>
        FindAsync(_db.SalesDeskVisits.AsNoTracking().Include(x => x.Customer), id, ToDto, "Ziyaret bulunamadi.", cancellationToken);

    public async Task<ApiResponse<SalesDeskVisitDto>> CreateVisitAsync(SalesDeskVisitUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskVisit();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "Ziyaret olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskVisitDto>> UpdateVisitAsync(long id, SalesDeskVisitUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskVisits.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskVisitDto>("Ziyaret bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Ziyaret guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteVisitAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskVisits, id, "Ziyaret silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskVisitFormDto>>> GetVisitFormsAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskVisitForms.AsNoTracking()
            .Include(x => x.Customer)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskVisitForm.Title), nameof(SalesDeskVisitForm.Content), "Customer.Name");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskVisitForm.Id));
    }

    public async Task<ApiResponse<SalesDeskVisitFormDto>> CreateVisitFormAsync(SalesDeskVisitFormUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskVisitForm();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "Ziyaret formu olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskVisitFormDto>> UpdateVisitFormAsync(long id, SalesDeskVisitFormUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskVisitForms.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskVisitFormDto>("Ziyaret formu bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Ziyaret formu guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteVisitFormAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskVisitForms, id, "Ziyaret formu silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskFixedAssetDto>>> GetFixedAssetsAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskFixedAssets.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskFixedAsset.Code), nameof(SalesDeskFixedAsset.Name), nameof(SalesDeskFixedAsset.Category));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskFixedAsset.Id));
    }

    public async Task<ApiResponse<SalesDeskFixedAssetDto>> CreateFixedAssetAsync(SalesDeskFixedAssetUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskFixedAsset();
        Apply(request, entity);
        entity.Code = string.IsNullOrWhiteSpace(entity.Code) ? await NextCodeAsync("DMR", _db.SalesDeskFixedAssets, x => x.Code, cancellationToken) : entity.Code.Trim();
        return await AddAsync(entity, ToDto, "Demirbas olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskFixedAssetDto>> UpdateFixedAssetAsync(long id, SalesDeskFixedAssetUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskFixedAssets.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskFixedAssetDto>("Demirbas bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Demirbas guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteFixedAssetAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskFixedAssets, id, "Demirbas silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskRecurringPaymentDto>>> GetRecurringPaymentsAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskRecurringPayments.AsNoTracking()
            .Include(x => x.Customer)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskRecurringPayment.Name), nameof(SalesDeskRecurringPayment.Category), "Customer.Name");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskRecurringPayment.Id));
    }

    public async Task<ApiResponse<SalesDeskRecurringPaymentDto>> CreateRecurringPaymentAsync(SalesDeskRecurringPaymentUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskRecurringPayment();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "Standart odeme/gelir kalemi olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskRecurringPaymentDto>> UpdateRecurringPaymentAsync(long id, SalesDeskRecurringPaymentUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskRecurringPayments.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskRecurringPaymentDto>("Standart odeme/gelir kalemi bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Standart odeme/gelir kalemi guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteRecurringPaymentAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskRecurringPayments, id, "Standart odeme/gelir kalemi silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskSoftwareResearchDto>>> GetSoftwareResearchesAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskSoftwareResearches.AsNoTracking()
            .Include(x => x.PotentialCustomer)
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskSoftwareResearch.Provider), nameof(SalesDeskSoftwareResearch.Keywords), nameof(SalesDeskSoftwareResearch.Host), "PotentialCustomer.CompanyName");

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskSoftwareResearch.Id));
    }

    public async Task<ApiResponse<SalesDeskSoftwareResearchDto>> CreateSoftwareResearchAsync(SalesDeskSoftwareResearchUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskSoftwareResearch();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "Yazilim arastirma kaydi olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskSoftwareResearchDto>> UpdateSoftwareResearchAsync(long id, SalesDeskSoftwareResearchUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskSoftwareResearches.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskSoftwareResearchDto>("Yazilim arastirma kaydi bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Yazilim arastirma kaydi guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteSoftwareResearchAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskSoftwareResearches, id, "Yazilim arastirma kaydi silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskErpNewsItemDto>>> GetErpNewsAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskErpNewsItems.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskErpNewsItem.Topic), nameof(SalesDeskErpNewsItem.Title), nameof(SalesDeskErpNewsItem.Source));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskErpNewsItem.Id));
    }

    public async Task<ApiResponse<SalesDeskErpNewsItemDto>> CreateErpNewsAsync(SalesDeskErpNewsItemUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskErpNewsItem();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "ERP haber kaydi olusturuldu.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskErpNewsItemDto>> UpdateErpNewsAsync(long id, SalesDeskErpNewsItemUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskErpNewsItems.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskErpNewsItemDto>("ERP haber kaydi bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "ERP haber kaydi guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteErpNewsAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskErpNewsItems, id, "ERP haber kaydi silindi.", cancellationToken);

    public Task<ApiResponse<PagedResponse<SalesDeskGmailMessageDto>>> GetGmailMessagesAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _db.SalesDeskGmailMessages.AsNoTracking()
            .Where(x => !x.IsDeleted)
            .ApplySearch(request.Search, nameof(SalesDeskGmailMessage.Sender), nameof(SalesDeskGmailMessage.Subject), nameof(SalesDeskGmailMessage.Preview));

        return PageAsync(query, request, ToDto, cancellationToken, nameof(SalesDeskGmailMessage.Id));
    }

    public async Task<ApiResponse<SalesDeskGmailMessageDto>> CreateGmailMessageAsync(SalesDeskGmailMessageUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = new SalesDeskGmailMessage();
        Apply(request, entity);
        return await AddAsync(entity, ToDto, "Gmail mesaji kaydedildi.", cancellationToken);
    }

    public async Task<ApiResponse<SalesDeskGmailMessageDto>> UpdateGmailMessageAsync(long id, SalesDeskGmailMessageUpsertDto request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.SalesDeskGmailMessages.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null) return NotFound<SalesDeskGmailMessageDto>("Gmail mesaji bulunamadi.");
        Apply(request, entity);
        return await SaveUpdatedAsync(entity, ToDto, "Gmail mesaji guncellendi.", cancellationToken);
    }

    public Task<ApiResponse<object>> DeleteGmailMessageAsync(long id, CancellationToken cancellationToken = default) =>
        SoftDeleteAsync(_db.SalesDeskGmailMessages, id, "Gmail mesaji silindi.", cancellationToken);

    private static async Task<ApiResponse<PagedResponse<TDto>>> PageAsync<TEntity, TDto>(
        IQueryable<TEntity> query,
        PagedRequest request,
        Func<TEntity, TDto> map,
        CancellationToken cancellationToken,
        string defaultSortBy)
        where TEntity : BaseEntity
    {
        request.SortBy = string.IsNullOrWhiteSpace(request.SortBy) ? defaultSortBy : request.SortBy;
        request.SortDirection = string.IsNullOrWhiteSpace(request.SortDirection) ? "desc" : request.SortDirection;
        query = query.ApplyFilters(request.Filters, request.FilterLogic).ApplySorting(request.SortBy, request.SortDirection);

        var total = await query.CountAsync(cancellationToken);
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : Math.Min(request.PageSize, 500);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return ApiResponse<PagedResponse<TDto>>.SuccessResult(new PagedResponse<TDto>
        {
            Items = items.Select(map).ToList(),
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        }, "Liste getirildi.");
    }

    private static async Task<ApiResponse<TDto>> FindAsync<TEntity, TDto>(IQueryable<TEntity> query, long id, Func<TEntity, TDto> map, string notFoundMessage, CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        var entity = await query.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        return entity == null ? NotFound<TDto>(notFoundMessage) : ApiResponse<TDto>.SuccessResult(map(entity), "Kayit getirildi.");
    }

    private async Task<ApiResponse<TDto>> AddAsync<TEntity, TDto>(TEntity entity, Func<TEntity, TDto> map, string message, CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        await _db.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<TDto>.SuccessResult(map(entity), message);
    }

    private async Task<ApiResponse<TDto>> SaveUpdatedAsync<TEntity, TDto>(TEntity entity, Func<TEntity, TDto> map, string message, CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        entity.UpdatedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<TDto>.SuccessResult(map(entity), message);
    }

    private async Task<ApiResponse<object>> SoftDeleteAsync<TEntity>(DbSet<TEntity> set, long id, string message, CancellationToken cancellationToken)
        where TEntity : BaseEntity
    {
        var entity = await set.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        if (entity == null)
        {
            return ApiResponse<object>.ErrorResult("Kayit bulunamadi.", statusCode: StatusCodes.Status404NotFound);
        }

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);
        return ApiResponse<object>.SuccessResult(new { id }, message);
    }

    private static ApiResponse<T> NotFound<T>(string message) =>
        ApiResponse<T>.ErrorResult(message, statusCode: StatusCodes.Status404NotFound);

    private static async Task<string> NextCodeAsync<TEntity>(string prefix, IQueryable<TEntity> query, Func<TEntity, string> codeSelector, CancellationToken cancellationToken)
    {
        var count = await query.CountAsync(cancellationToken);
        return $"{prefix}{DateTime.UtcNow:yyyy}{count + 1:000000}";
    }

    private static void Apply(SalesDeskCustomerUpsertDto source, SalesDeskCustomer target)
    {
        target.Code = source.Code?.Trim() ?? target.Code;
        target.Name = source.Name.Trim();
        target.ContactName = source.ContactName?.Trim();
        target.Phone = source.Phone?.Trim();
        target.Email = source.Email?.Trim();
        target.Kind = source.Kind;
        target.Balance = source.Balance;
        target.City = source.City?.Trim();
        target.District = source.District?.Trim();
    }

    private static void Apply(SalesDeskPotentialCustomerUpsertDto source, SalesDeskPotentialCustomer target)
    {
        target.Code = source.Code?.Trim() ?? target.Code;
        target.CompanyName = source.CompanyName.Trim();
        target.ContactName = source.ContactName?.Trim();
        target.Phone = source.Phone?.Trim();
        target.Email = source.Email?.Trim();
        target.City = source.City?.Trim();
        target.District = source.District?.Trim();
        target.Status = source.Status;
        target.MatchScore = source.MatchScore;
        target.LastResearchDate = source.LastResearchDate;
    }

    private static void Apply(SalesDeskProductUpsertDto source, SalesDeskProduct target)
    {
        target.Code = source.Code?.Trim() ?? target.Code;
        target.Name = source.Name.Trim();
        target.Category = source.Category?.Trim();
        target.Unit = source.Unit.Trim();
        target.SalesPrice = source.SalesPrice;
        target.StockQuantity = source.StockQuantity;
        target.MinimumStockQuantity = source.MinimumStockQuantity;
        target.SearchText = string.Join(' ', new[] { target.Code, target.Name, target.Category, target.Unit }.Where(x => !string.IsNullOrWhiteSpace(x)));
    }

    private async Task ApplyQuoteAsync(SalesDeskQuoteUpsertDto source, SalesDeskQuote target, CancellationToken cancellationToken)
    {
        target.QuoteNumber = source.QuoteNumber?.Trim() ?? target.QuoteNumber;
        target.CustomerId = source.CustomerId;
        target.QuoteDate = source.QuoteDate;
        target.Status = source.Status;
        target.Notes = source.Notes?.Trim();
        target.Lines.Clear();

        var products = await _db.SalesDeskProducts.AsNoTracking()
            .Where(x => source.Lines.Select(l => l.ProductId).Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        foreach (var line in source.Lines)
        {
            var unitPrice = line.UnitPrice > 0 ? line.UnitPrice : products.GetValueOrDefault(line.ProductId)?.SalesPrice ?? 0;
            var lineTotal = line.Quantity * unitPrice;
            target.Lines.Add(new SalesDeskQuoteLine
            {
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                UnitPrice = unitPrice,
                VatRate = line.VatRate,
                LineTotal = lineTotal
            });
        }

        target.SubTotal = target.Lines.Sum(x => x.LineTotal);
        target.VatTotal = target.Lines.Sum(x => x.LineTotal * x.VatRate / 100);
        target.GrandTotal = target.SubTotal + target.VatTotal;
    }

    private async Task ApplyInvoiceAsync(SalesDeskInvoiceUpsertDto source, SalesDeskInvoice target, CancellationToken cancellationToken)
    {
        target.InvoiceNumber = source.InvoiceNumber?.Trim() ?? target.InvoiceNumber;
        target.InvoiceType = source.InvoiceType;
        target.CustomerId = source.CustomerId;
        target.QuoteId = source.QuoteId;
        target.InvoiceDate = source.InvoiceDate;
        target.DueDate = source.DueDate;
        target.Status = source.Status;
        target.DiscountRate = source.DiscountRate;
        target.Notes = source.Notes?.Trim();
        target.Lines.Clear();

        var products = await _db.SalesDeskProducts.AsNoTracking()
            .Where(x => source.Lines.Select(l => l.ProductId).Contains(x.Id))
            .ToDictionaryAsync(x => x.Id, cancellationToken);

        foreach (var line in source.Lines)
        {
            var unitPrice = line.UnitPrice > 0 ? line.UnitPrice : products.GetValueOrDefault(line.ProductId)?.SalesPrice ?? 0;
            var lineTotal = line.Quantity * unitPrice;
            target.Lines.Add(new SalesDeskInvoiceLine
            {
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                UnitPrice = unitPrice,
                VatRate = line.VatRate,
                LineTotal = lineTotal
            });
        }

        target.SubTotal = target.Lines.Sum(x => x.LineTotal);
        target.DiscountTotal = target.SubTotal * target.DiscountRate / 100;
        var discountedSubTotal = target.SubTotal - target.DiscountTotal;
        target.VatTotal = target.Lines.Sum(x => x.LineTotal * x.VatRate / 100);
        target.GrandTotal = discountedSubTotal + target.VatTotal;
    }

    private static void Apply(SalesDeskTaskUpsertDto source, SalesDeskTask target)
    {
        target.Title = source.Title.Trim();
        target.Description = source.Description?.Trim();
        target.GroupName = source.GroupName?.Trim();
        target.CustomerId = source.CustomerId;
        target.AssignedUserId = source.AssignedUserId;
        target.Priority = source.Priority;
        target.Status = source.Status;
        target.DueDate = source.DueDate;
    }

    private static void Apply(SalesDeskVisitUpsertDto source, SalesDeskVisit target)
    {
        target.VisitDate = source.VisitDate;
        target.VisitTime = source.VisitTime;
        target.Title = source.Title.Trim();
        target.CustomerId = source.CustomerId;
        target.VisitType = source.VisitType.Trim();
        target.Status = source.Status;
        target.Notes = source.Notes?.Trim();
    }

    private static void Apply(SalesDeskVisitFormUpsertDto source, SalesDeskVisitForm target)
    {
        target.VisitId = source.VisitId;
        target.CustomerId = source.CustomerId;
        target.Title = source.Title.Trim();
        target.FormDate = source.FormDate;
        target.Content = source.Content?.Trim();
        target.OwnerUserId = source.OwnerUserId;
    }

    private static void Apply(SalesDeskFixedAssetUpsertDto source, SalesDeskFixedAsset target)
    {
        target.Code = source.Code?.Trim() ?? target.Code;
        target.Name = source.Name.Trim();
        target.Category = source.Category?.Trim();
        target.PurchaseDate = source.PurchaseDate;
        target.Value = source.Value;
        target.Status = source.Status;
    }

    private static void Apply(SalesDeskRecurringPaymentUpsertDto source, SalesDeskRecurringPayment target)
    {
        target.Name = source.Name.Trim();
        target.Type = source.Type;
        target.Category = source.Category?.Trim();
        target.DayOfMonth = source.DayOfMonth;
        target.Amount = source.Amount;
        target.CustomerId = source.CustomerId;
        target.IsActive = source.IsActive;
    }

    private static void Apply(SalesDeskSoftwareResearchUpsertDto source, SalesDeskSoftwareResearch target)
    {
        target.PotentialCustomerId = source.PotentialCustomerId;
        target.Provider = source.Provider.Trim();
        target.Keywords = source.Keywords?.Trim();
        target.Host = source.Host?.Trim();
        target.SourceUrl = source.SourceUrl?.Trim();
        target.Score = source.Score;
        target.Status = source.Status;
        target.ResearchedAt = source.ResearchedAt;
    }

    private static void Apply(SalesDeskErpNewsItemUpsertDto source, SalesDeskErpNewsItem target)
    {
        target.Topic = source.Topic.Trim();
        target.Title = source.Title.Trim();
        target.Source = source.Source?.Trim();
        target.SourceUrl = source.SourceUrl?.Trim();
        target.Score = source.Score;
        target.IsCritical = source.IsCritical;
        target.IsRead = source.IsRead;
        target.PublishedAt = source.PublishedAt;
    }

    private static void Apply(SalesDeskGmailMessageUpsertDto source, SalesDeskGmailMessage target)
    {
        target.GmailMessageId = source.GmailMessageId.Trim();
        target.Sender = source.Sender.Trim();
        target.Subject = source.Subject.Trim();
        target.Preview = source.Preview?.Trim();
        target.ReceivedAt = source.ReceivedAt;
        target.IsUnread = source.IsUnread;
        target.IsMeeting = source.IsMeeting;
        target.ThreadId = source.ThreadId?.Trim();
    }

    private static SalesDeskCustomerDto ToDto(SalesDeskCustomer x) => new()
    {
        Id = x.Id,
        Code = x.Code,
        Name = x.Name,
        ContactName = x.ContactName,
        Phone = x.Phone,
        Email = x.Email,
        Kind = x.Kind,
        Balance = x.Balance,
        City = x.City,
        District = x.District
    };

    private static SalesDeskPotentialCustomerDto ToDto(SalesDeskPotentialCustomer x) => new()
    {
        Id = x.Id,
        Code = x.Code,
        CompanyName = x.CompanyName,
        ContactName = x.ContactName,
        Phone = x.Phone,
        Email = x.Email,
        City = x.City,
        District = x.District,
        Status = x.Status,
        MatchScore = x.MatchScore,
        LastResearchDate = x.LastResearchDate
    };

    private static SalesDeskProductDto ToDto(SalesDeskProduct x) => new()
    {
        Id = x.Id,
        Code = x.Code,
        Name = x.Name,
        Category = x.Category,
        Unit = x.Unit,
        SalesPrice = x.SalesPrice,
        StockQuantity = x.StockQuantity,
        MinimumStockQuantity = x.MinimumStockQuantity
    };

    private static SalesDeskProductCustomerDto ToDto(SalesDeskProductCustomer x) => new()
    {
        Id = x.Id,
        ProductId = x.ProductId,
        ProductCode = x.Product?.Code ?? string.Empty,
        ProductName = x.Product?.Name ?? string.Empty,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name,
        PotentialCustomerId = x.PotentialCustomerId,
        PotentialCustomerName = x.PotentialCustomer?.CompanyName
    };

    private static SalesDeskQuoteDto ToDto(SalesDeskQuote x) => new()
    {
        Id = x.Id,
        QuoteNumber = x.QuoteNumber,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name ?? string.Empty,
        QuoteDate = x.QuoteDate,
        Status = x.Status,
        SubTotal = x.SubTotal,
        VatTotal = x.VatTotal,
        GrandTotal = x.GrandTotal,
        Notes = x.Notes,
        Lines = x.Lines.Select(ToDto).ToList()
    };

    private static SalesDeskInvoiceDto ToDto(SalesDeskInvoice x) => new()
    {
        Id = x.Id,
        InvoiceNumber = x.InvoiceNumber,
        InvoiceType = x.InvoiceType,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name ?? string.Empty,
        QuoteId = x.QuoteId,
        QuoteNumber = x.Quote?.QuoteNumber,
        InvoiceDate = x.InvoiceDate,
        DueDate = x.DueDate,
        Status = x.Status,
        DiscountRate = x.DiscountRate,
        DiscountTotal = x.DiscountTotal,
        SubTotal = x.SubTotal,
        VatTotal = x.VatTotal,
        GrandTotal = x.GrandTotal,
        Notes = x.Notes,
        Lines = x.Lines.Select(ToDto).ToList()
    };

    private static SalesDeskLineDto ToDto(SalesDeskQuoteLine x) => new()
    {
        Id = x.Id,
        ProductId = x.ProductId,
        ProductCode = x.Product?.Code,
        ProductName = x.Product?.Name,
        Quantity = x.Quantity,
        UnitPrice = x.UnitPrice,
        VatRate = x.VatRate,
        LineTotal = x.LineTotal
    };

    private static SalesDeskLineDto ToDto(SalesDeskInvoiceLine x) => new()
    {
        Id = x.Id,
        ProductId = x.ProductId,
        ProductCode = x.Product?.Code,
        ProductName = x.Product?.Name,
        Quantity = x.Quantity,
        UnitPrice = x.UnitPrice,
        VatRate = x.VatRate,
        LineTotal = x.LineTotal
    };

    private static SalesDeskTaskDto ToDto(SalesDeskTask x) => new()
    {
        Id = x.Id,
        Title = x.Title,
        Description = x.Description,
        GroupName = x.GroupName,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name,
        AssignedUserId = x.AssignedUserId,
        Priority = x.Priority,
        Status = x.Status,
        DueDate = x.DueDate
    };

    private static SalesDeskVisitDto ToDto(SalesDeskVisit x) => new()
    {
        Id = x.Id,
        VisitDate = x.VisitDate,
        VisitTime = x.VisitTime,
        Title = x.Title,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name,
        VisitType = x.VisitType,
        Status = x.Status,
        Notes = x.Notes
    };

    private static SalesDeskVisitFormDto ToDto(SalesDeskVisitForm x) => new()
    {
        Id = x.Id,
        VisitId = x.VisitId,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name,
        Title = x.Title,
        FormDate = x.FormDate,
        Content = x.Content,
        OwnerUserId = x.OwnerUserId
    };

    private static SalesDeskFixedAssetDto ToDto(SalesDeskFixedAsset x) => new()
    {
        Id = x.Id,
        Code = x.Code,
        Name = x.Name,
        Category = x.Category,
        PurchaseDate = x.PurchaseDate,
        Value = x.Value,
        Status = x.Status
    };

    private static SalesDeskRecurringPaymentDto ToDto(SalesDeskRecurringPayment x) => new()
    {
        Id = x.Id,
        Name = x.Name,
        Type = x.Type,
        Category = x.Category,
        DayOfMonth = x.DayOfMonth,
        Amount = x.Amount,
        CustomerId = x.CustomerId,
        CustomerName = x.Customer?.Name,
        IsActive = x.IsActive
    };

    private static SalesDeskSoftwareResearchDto ToDto(SalesDeskSoftwareResearch x) => new()
    {
        Id = x.Id,
        PotentialCustomerId = x.PotentialCustomerId,
        PotentialCustomerName = x.PotentialCustomer?.CompanyName,
        Provider = x.Provider,
        Keywords = x.Keywords,
        Host = x.Host,
        SourceUrl = x.SourceUrl,
        Score = x.Score,
        Status = x.Status,
        ResearchedAt = x.ResearchedAt
    };

    private static SalesDeskErpNewsItemDto ToDto(SalesDeskErpNewsItem x) => new()
    {
        Id = x.Id,
        Topic = x.Topic,
        Title = x.Title,
        Source = x.Source,
        SourceUrl = x.SourceUrl,
        Score = x.Score,
        IsCritical = x.IsCritical,
        IsRead = x.IsRead,
        PublishedAt = x.PublishedAt
    };

    private static SalesDeskGmailMessageDto ToDto(SalesDeskGmailMessage x) => new()
    {
        Id = x.Id,
        GmailMessageId = x.GmailMessageId,
        Sender = x.Sender,
        Subject = x.Subject,
        Preview = x.Preview,
        ReceivedAt = x.ReceivedAt,
        IsUnread = x.IsUnread,
        IsMeeting = x.IsMeeting,
        ThreadId = x.ThreadId
    };
}
