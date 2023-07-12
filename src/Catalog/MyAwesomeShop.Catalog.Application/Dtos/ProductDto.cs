using MyAwesomeShop.Shared;

namespace MyAwesomeShop.Catalog.Application.Dtos;

public record ProductDto(Guid Id, string Name, string Description, Money Price);

public record CreateProductRequest(string Name, string Description, Money Price);

public record UpdateProductRequest(Guid Id, string Name, string Description, Money Price);
