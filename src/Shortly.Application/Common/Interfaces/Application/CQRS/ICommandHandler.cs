using ErrorOr;
using MediatR;

namespace Shortly.Application.Common.Interfaces.Application.CQRS
{
    public interface ICommandHandler<TCommand, TResponse>
        : IRequestHandler<TCommand, ErrorOr<TResponse>>
        where TCommand : ICommand<TResponse>;
}