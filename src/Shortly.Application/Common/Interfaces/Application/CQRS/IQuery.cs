using ErrorOr;
using MediatR;

namespace Shortly.Application.Common.Interfaces.Application.CQRS
{
    public interface IQuery<TResponse>
        : IRequest<ErrorOr<TResponse>>;
}