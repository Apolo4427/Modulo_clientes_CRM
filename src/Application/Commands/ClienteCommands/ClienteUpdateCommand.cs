using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using MediatR;

namespace ClientesCRM.src.Application.Commands.ClienteCommands
{
    public record ClienteUpdateCommand(
        Guid Id,
        string Telefono,
        string CorreoElectronico,
        string DireccionPrincipal
    ) : IRequest<ClienteResponseDto>;
}
