using MediatR;

namespace ClientesCRM.src.Application.Commands.ClienteCommands
{
    public record ClienteDeleteCommand(Guid Id) : IRequest<Guid>;
}
