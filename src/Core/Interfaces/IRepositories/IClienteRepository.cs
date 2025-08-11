using ClientesCRM.src.Core.Entities;

namespace ClientesCRM.src.Core.Interfaces.IRepositories
{
    public interface IClienteRepository
    {
        public Task<Guid> Add(Cliente cliente, CancellationToken ct = default);
        public Task<Cliente> GetClienteById(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Cliente>> GetAllAsync(CancellationToken ct = default);
        public Task<Cliente> UpdateCliente(Cliente cliente, CancellationToken ct = default);
        public Task<Guid> DeleteClienteById(Guid id, CancellationToken ct = default);
    }
}