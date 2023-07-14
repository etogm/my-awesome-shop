using MediatR;

namespace MyAwesomeShop.Shared.Application.Messaging;

public interface ICommand : IRequest { }

public interface ICommand<out TCommandResult> : IRequest<TCommandResult> { }

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand { }

public interface ICommandHandler<in TCommand, TCommandResult> : IRequestHandler<TCommand, TCommandResult> where TCommand : ICommand<TCommandResult> { }
