using MediatR;

namespace DocumentManager.Application.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}