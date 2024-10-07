using ErrorOr;
using MediatR;

namespace Shortly.Application.Common.Interfaces.Application.CQRS
{
    public interface ICommand<TResponse>
        : IRequest<ErrorOr<TResponse>>;
}