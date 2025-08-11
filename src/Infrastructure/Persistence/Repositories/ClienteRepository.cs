using ClientesCRM.src.Core.Entities;
using ClientesCRM.src.Core.Interfaces.IRepositories;
using ClientesCRM.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientesCRM.src.Infrastructure.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private ClientesDbContext _ctx;

        public ClienteRepository(ClientesDbContext context)
        {
            _ctx = context;
        }

        public async Task<Guid> Add(Cliente cliente, CancellationToken ct = default)
        {
            await _ctx.Clientes.AddAsync(cliente, ct);
            await _ctx.SaveChangesAsync(ct);
            return cliente.Id;
        }

        public async Task<Cliente> GetClienteById(Guid id, CancellationToken ct = default)
        {
            return await _ctx.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct)
                ?? throw new KeyNotFoundException($"El cliente con id: {id} no ha sido encontrado");
        }

        public async Task<IReadOnlyList<Cliente>> GetAllAsync(CancellationToken ct = default)
        {
            return await _ctx.Clientes.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente, CancellationToken ct = default)
        {
            _ctx.Clientes.Update(cliente);
            await _ctx.SaveChangesAsync(ct);
            return cliente;
        }

        public async Task<Guid> DeleteClienteById(Guid id, CancellationToken ct = default)
        {
            var cliente = await _ctx.Clientes.FindAsync(new object[] { id }, ct)
                ?? throw new KeyNotFoundException($"El cliente con id: {id} no se ha encontrado");

            _ctx.Clientes.Remove(cliente);
            await _ctx.SaveChangesAsync(ct);
            return id;
        }
    }
}