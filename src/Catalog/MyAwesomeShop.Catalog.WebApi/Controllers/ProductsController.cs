using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyAwesomeShop.Catalog.Application.Products;
using MyAwesomeShop.Shared.Application.Extensions;

namespace MyAwesomeShop.Catalog.WebApi.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<Results<NotFound, Ok<ProductDto>>> GetProductAsync(Guid id)
    {
        var product = await _mediator.Send(new GetProductQuery(id));
        return product == null ? TypedResults.NotFound() : TypedResults.Ok(product);
    }

    [HttpGet()]
    public async Task<Ok<PaginatedList<ProductDto>>> GetProductsAsync([FromQuery]GetProductsQuery request)
    {
        return TypedResults.Ok(await _mediator.Send(request));
    }

    [HttpPost]
    public async Task<Results<BadRequest, Created<ProductDto>>> CreateProductAsync(CreateProductCommand request)
    {
        if (!ModelState.IsValid)
        {
            return TypedResults.BadRequest();
        }

        var product = await _mediator.Send(request);

        var location = Url.Action(nameof(GetProductAsync), new { id = product.Id }) ?? $"/{product.Id}";
        return TypedResults.Created(location, product);
    }

    [HttpPut]
    public async Task<Results<BadRequest, Ok<ProductDto>>> UpdateProductAsync(UpdateProductCommand request)
    {
        var product = await _mediator.Send(request);

        return TypedResults.Ok(product);
    }

    [HttpDelete]
    public async Task<Ok> DeleteProductAsync(Guid id)
    {
        await _mediator.Send(id);

        return TypedResults.Ok();
    }
}
