using MyAwesomeShop.Shared;

namespace MyAwesomeShop.Catalog.Application.Products;

public record ProductDto(Guid Id, string Name, string Description, Money Price);
