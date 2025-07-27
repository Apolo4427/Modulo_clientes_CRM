using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;

namespace ClientesCRM.src.Core.Interfaces.IRepositories
{
    public interface IClienteRepository
    {
        public Task<Guid id> Add(Cliente cliente, CancellationToken ct = default);
        public Task<Cliente> GetClienteById(Guid id, CancellationToken ct = default);
        public Task<Guid id> UpdateCliente(Cliente cliente,  CancellationToken ct = default);
        public Task<Guid id> DeleteClienteById(Guid id, CancellationToken ct = default);
    }
}