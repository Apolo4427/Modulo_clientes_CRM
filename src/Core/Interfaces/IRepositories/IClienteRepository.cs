using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;

namespace ClientesCRM.src.Core.Interfaces.IRepositories
{
    public interface IClienteRepository
    {
        public Task Add(ClienteCreateDto cliente, CancellationToken ct = default);
        public Task<ClienteResponseDto> GetCliente(Guid id, CancellationToken ct = default);
        // public Task<ClienteUpdateDto> UpdateCliente(Guid id, )
    }
}