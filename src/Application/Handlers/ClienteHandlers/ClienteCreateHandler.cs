using AutoMapper;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Application.Commands.ClienteCommands;
using ClientesCRM.src.Core.Entities;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using MediatR;

namespace ClientesCRM.src.Application.Handlers.ClienteHandlers
{
    public class ClienteCreateHandler : IRequestHandler<ClienteCreateCommand, ClienteResponseDto>
    {
        private readonly IClienteRepository _repo;
        private readonly IMapper _mapper;

        public ClienteCreateHandler(IClienteRepository repo, IMapper mapper)
        {
            _repo   = repo;
            _mapper = mapper;
        }

        public async Task<ClienteResponseDto> Handle(ClienteCreateCommand request, CancellationToken cancellationToken)
        {
            var cliente = _mapper.Map<Cliente>(request);

            var newId = await _repo.Add(cliente, cancellationToken);

            var created = await _repo.GetClienteById(newId, cancellationToken);

            return _mapper.Map<ClienteResponseDto>(created);
        }
    }
}

