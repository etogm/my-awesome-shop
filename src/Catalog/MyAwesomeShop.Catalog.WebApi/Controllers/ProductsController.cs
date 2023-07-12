using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MyAwesomeShop.Basket;
using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.Dtos;
using MyAwesomeShop.Shared.Application;
using MyAwesomeShop.Shared.Infrastructure.PubSub;

namespace MyAwesomeShop.Catalog.WebApi.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public async Task<Results<NotFound, Ok<ProductDto>>> GetProductAsync(Guid id)
    {
        var product = await _productService.GetProductAsync(id);
        return product == null ? TypedResults.NotFound() : TypedResults.Ok(product);
    }

    [HttpGet()]
    public async Task<Ok<PaginatedList<ProductDto>>> GetProductsAsync(int currentPage = 1, int perPage = 50)
    {
        return TypedResults.Ok(await _productService.GetProductsAsync(currentPage, perPage));
    }

    [HttpPost]
    public async Task<Results<BadRequest, Created<ProductDto>>> CreateProductAsync(CreateProductRequest request)
    {
        if (!ModelState.IsValid)
        {
            return TypedResults.BadRequest();
        }

        var product = await _productService.CreateProductAsync(request);

        var location = Url.Action(nameof(GetProductAsync), new { id = product.Id }) ?? $"/{product.Id}";
        return TypedResults.Created(location, product);
    }

    [HttpPut]
    public async Task<Results<BadRequest, Ok<ProductDto>>> UpdateProductAsync(UpdateProductRequest request)
    {
        var product = await _productService.UpdateProductAsync(request);

        return TypedResults.Ok(product);
    }

    [HttpDelete]
    public async Task<Ok> DeleteProductAsync(Guid id)
    {
        await _productService.DeleteProductAsync(id);

        return TypedResults.Ok();
    }
}
