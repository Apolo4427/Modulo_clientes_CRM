using AutoMapper;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Application.Commands.ClienteCommands;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using MediatR;

namespace ClientesCRM.src.Application.Handlers.ClienteHandlers
{
    public class ClienteUpdateHandler : IRequestHandler<ClienteUpdateCommand, ClienteResponseDto>
    {
        private readonly IClienteRepository _repo;
        private readonly IMapper _mapper;

        public ClienteUpdateHandler(IClienteRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<ClienteResponseDto> Handle(ClienteUpdateCommand request, CancellationToken cancellationToken)
        {
           
            var cliente = await _repo.GetClienteById(request.Id, cancellationToken);

            cliente.ActualizarContacto(
                request.Telefono,
                request.CorreoElectronico,
                request.DireccionPrincipal
            );

            var updated = await _repo.UpdateCliente(cliente, cancellationToken);

            return _mapper.Map<ClienteResponseDto>(updated);
        }
    }
}

