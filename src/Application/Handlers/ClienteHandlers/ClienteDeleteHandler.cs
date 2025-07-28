using ClientesCRM.src.Application.Commands.ClienteCommands;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using MediatR;

namespace ClientesCRM.src.Application.Handlers.ClienteHandlers
{
    public class ClienteDeleteHandler : IRequestHandler<ClienteDeleteCommand, Guid>
    {
        private readonly IClienteRepository _repo;

        public ClienteDeleteHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(ClienteDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _repo.DeleteClienteById(request.Id, cancellationToken);
        }
    }
}

