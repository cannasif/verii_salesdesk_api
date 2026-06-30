using Microsoft.AspNetCore.Mvc;
using salesdesk_api.Modules.SalesDesk.Application.Dtos;
using salesdesk_api.Modules.SalesDesk.Application.Services;
using salesdesk_api.Shared.Common.Application;

namespace salesdesk_api.Modules.SalesDesk.Api;

[Route("api/salesdesk/products")]
public sealed class ProductsController : ApiControllerBase
{
    public ProductsController(ISalesDeskService service) : base(service)
    {
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PagedRequest request, CancellationToken cancellationToken)
    {
        var result = await Service.GetProductsAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPost("query")]
    public async Task<IActionResult> Query([FromBody] PagedRequest? request, CancellationToken cancellationToken)
    {
        var result = await Service.GetProductsAsync(request ?? new PagedRequest(), cancellationToken);
        return Respond(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
    {
        var result = await Service.GetProductAsync(id, cancellationToken);
        return Respond(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SalesDeskProductUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.CreateProductAsync(request, cancellationToken);
        return Respond(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] SalesDeskProductUpsertDto request, CancellationToken cancellationToken)
    {
        var result = await Service.UpdateProductAsync(id, request, cancellationToken);
        return Respond(result);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Service.DeleteProductAsync(id, cancellationToken);
        return Respond(result);
    }
}
