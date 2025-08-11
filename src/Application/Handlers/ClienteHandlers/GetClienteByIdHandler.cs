using AutoMapper;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Application.Queries.ClienteQueries;
using ClientesCRM.src.Core.Entities;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using MediatR;

public class GetClienteByIdHandler 
    : IRequestHandler<GetClienteByIdQuery, ClienteResponseDto>  
{
    private readonly IClienteRepository _repo;
    private readonly IMapper _mapper;
    public GetClienteByIdHandler(IClienteRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ClienteResponseDto> Handle(GetClienteByIdQuery query, CancellationToken cancellationToken)
    {
        var cliente = await _repo.GetClienteById(query.Id, cancellationToken);

        if (cliente is null)
            throw new KeyNotFoundException($"No se encontró un cliente con Id = {query.Id}");

        var dto = _mapper.Map<ClienteResponseDto>(cliente);

        return dto;
    }
}


