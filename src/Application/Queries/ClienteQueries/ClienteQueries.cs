using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Core.Entities;
using MediatR;

namespace ClientesCRM.src.Application.Queries.ClienteQueries
{
    public record GetClienteByIdQuery(
        Guid Id
    ) : IRequest<ClienteResponseDto>;

    public record GetAllClientesQuery(

    ): IRequest<IReadOnlyList<ClienteResponseDto>>;
}