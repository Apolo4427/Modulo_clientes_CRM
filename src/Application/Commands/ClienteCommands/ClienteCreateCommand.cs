
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using MediatR;

namespace ClientesCRM.src.Application.Commands.ClienteCommands
{
    public record ClienteCreateCommand(
        string Nombre,
        string Apellido,
        string Telefono,
        string CorreoElectronico,
        string DireccionPrincipal,
        string NotasGenerales
    ) : IRequest<ClienteResponseDto>;
}