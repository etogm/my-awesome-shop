using MediatR;

namespace MyAwesomeShop.Shared.Application.Messaging;

public interface IQuery<out TIQueryResult> : IRequest<TIQueryResult> { }

public interface IQueryHandler<in TQuery, TQueryResult> : IRequestHandler<TQuery, TQueryResult> where TQuery : IQuery<TQueryResult> { }