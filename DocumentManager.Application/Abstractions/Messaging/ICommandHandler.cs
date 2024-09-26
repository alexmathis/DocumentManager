using MediatR;

namespace DocumentManager.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where  TCommand : ICommand<TResponse>
{
}