using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Application.Services;

public interface ISalesDeskService
{
    Task<ApiResponse<SalesDeskDashboardDto>> GetDashboardAsync(CancellationToken cancellationToken = default);
    Task<ApiResponse<List<SalesDeskSearchResultDto>>> SearchAsync(string? query, int take = 12, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskCustomerDto>>> GetCustomersAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskCustomerDto>> GetCustomerAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskCustomerDto>> CreateCustomerAsync(SalesDeskCustomerUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskCustomerDto>> UpdateCustomerAsync(long id, SalesDeskCustomerUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteCustomerAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskPotentialCustomerDto>>> GetPotentialCustomersAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskPotentialCustomerDto>> GetPotentialCustomerAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskPotentialCustomerDto>> CreatePotentialCustomerAsync(SalesDeskPotentialCustomerUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskPotentialCustomerDto>> UpdatePotentialCustomerAsync(long id, SalesDeskPotentialCustomerUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeletePotentialCustomerAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskProductDto>>> GetProductsAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskProductDto>> GetProductAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskProductDto>> CreateProductAsync(SalesDeskProductUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskProductDto>> UpdateProductAsync(long id, SalesDeskProductUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteProductAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskProductCustomerDto>>> GetProductCustomersAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskProductCustomerDto>> CreateProductCustomerAsync(SalesDeskProductCustomerUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteProductCustomerAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskQuoteDto>>> GetQuotesAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskQuoteDto>> GetQuoteAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskQuoteDto>> CreateQuoteAsync(SalesDeskQuoteUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskQuoteDto>> UpdateQuoteAsync(long id, SalesDeskQuoteUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteQuoteAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskInvoiceDto>>> GetInvoicesAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskInvoiceDto>> GetInvoiceAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskInvoiceDto>> CreateInvoiceAsync(SalesDeskInvoiceUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskInvoiceDto>> UpdateInvoiceAsync(long id, SalesDeskInvoiceUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteInvoiceAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskTaskDto>>> GetTasksAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<PagedResponse<SalesDeskTaskDto>>> GetOpenTasksAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskTaskDto>> GetTaskAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskTaskDto>> CreateTaskAsync(SalesDeskTaskUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskTaskDto>> UpdateTaskAsync(long id, SalesDeskTaskUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteTaskAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskVisitDto>>> GetVisitsAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskVisitDto>> GetVisitAsync(long id, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskVisitDto>> CreateVisitAsync(SalesDeskVisitUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskVisitDto>> UpdateVisitAsync(long id, SalesDeskVisitUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteVisitAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskVisitFormDto>>> GetVisitFormsAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskVisitFormDto>> CreateVisitFormAsync(SalesDeskVisitFormUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskVisitFormDto>> UpdateVisitFormAsync(long id, SalesDeskVisitFormUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteVisitFormAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskFixedAssetDto>>> GetFixedAssetsAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskFixedAssetDto>> CreateFixedAssetAsync(SalesDeskFixedAssetUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskFixedAssetDto>> UpdateFixedAssetAsync(long id, SalesDeskFixedAssetUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteFixedAssetAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskRecurringPaymentDto>>> GetRecurringPaymentsAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskRecurringPaymentDto>> CreateRecurringPaymentAsync(SalesDeskRecurringPaymentUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskRecurringPaymentDto>> UpdateRecurringPaymentAsync(long id, SalesDeskRecurringPaymentUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteRecurringPaymentAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskSoftwareResearchDto>>> GetSoftwareResearchesAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskSoftwareResearchDto>> CreateSoftwareResearchAsync(SalesDeskSoftwareResearchUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskSoftwareResearchDto>> UpdateSoftwareResearchAsync(long id, SalesDeskSoftwareResearchUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteSoftwareResearchAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskErpNewsItemDto>>> GetErpNewsAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskErpNewsItemDto>> CreateErpNewsAsync(SalesDeskErpNewsItemUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskErpNewsItemDto>> UpdateErpNewsAsync(long id, SalesDeskErpNewsItemUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteErpNewsAsync(long id, CancellationToken cancellationToken = default);

    Task<ApiResponse<PagedResponse<SalesDeskGmailMessageDto>>> GetGmailMessagesAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskGmailMessageDto>> CreateGmailMessageAsync(SalesDeskGmailMessageUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<SalesDeskGmailMessageDto>> UpdateGmailMessageAsync(long id, SalesDeskGmailMessageUpsertDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteGmailMessageAsync(long id, CancellationToken cancellationToken = default);
}
