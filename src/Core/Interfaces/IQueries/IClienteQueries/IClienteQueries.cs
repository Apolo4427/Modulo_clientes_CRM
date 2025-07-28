using ClientesCRM.src.Core.Entities;
using MediatR;
using ModuloClientes.Core.Ports.Queries;

namespace ClientesCRM.src.Core.Interfaces.IQueries.IClienteQueries
{
    public record GetClienteByIdQuery(
        Guid Id
    ) : IRequest<Cliente>;

    public interface IGetClienteByIdHandler : IQueryHandler<GetClienteByIdQuery, Cliente>
    {}
}