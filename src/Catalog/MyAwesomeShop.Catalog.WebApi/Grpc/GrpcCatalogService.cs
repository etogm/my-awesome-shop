using Grpc.Core;

using Mapster;

using MediatR;

using MyAwesomeShop.Catalog.Application.Products;
using MyAwesomeShop.Catalog.Grpc;

using static MyAwesomeShop.Catalog.Grpc.Catalog;

namespace MyAwesomeShop.Catalog.WebApi.Grpc;

internal class GrpcCatalogService : CatalogBase
{
    private readonly IMediator _mediator;

    public GrpcCatalogService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetProductResponse> GetProduct(GetProductRequest request, ServerCallContext context)
    {
        var response = await _mediator.Send(request.Adapt<GetProductQuery>());
        return response.Adapt<GetProductResponse>();
    }
}
