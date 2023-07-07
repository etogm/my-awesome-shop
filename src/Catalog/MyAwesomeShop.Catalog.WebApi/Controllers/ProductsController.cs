using Microsoft.AspNetCore.Mvc;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.Dtos;

namespace MyAwesomeShop.Catalog.WebApi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}")]
    public Task<ProductDto?> GetProductAsync(Guid id) => _productService.GetProductAsync(id);

    [HttpPost]
    public Task<ProductDto> CreateProductAsync(CreateProductRequest request) => _productService.CreateProductAsync(request);
}
