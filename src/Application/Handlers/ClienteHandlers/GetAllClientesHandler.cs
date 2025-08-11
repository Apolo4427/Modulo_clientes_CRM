using AutoMapper;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Application.Queries.ClienteQueries;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using MediatR;

namespace ClientesCRM.src.Application.Handlers.ClienteHandlers
{

    public class GetAllClientesHandler
        : IRequestHandler<GetAllClientesQuery, IReadOnlyList<ClienteResponseDto>>
    {
        private readonly IClienteRepository _repo;
        private readonly IMapper _mapper;

        public GetAllClientesHandler(IClienteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ClienteResponseDto>> Handle(
            GetAllClientesQuery request,
            CancellationToken cancellationToken)
        {
            var entities = await _repo.GetAllAsync(cancellationToken);
            // AutoMapper mapea a List<ClienteResposeDto>, que implementa IReadOnlyList<ClienteResposeDto>
            var dtos = _mapper.Map<List<ClienteResponseDto>>(entities);
            return dtos;
        }
    }
}
