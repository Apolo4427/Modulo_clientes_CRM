namespace ClientesCRM.src.Infrastructure.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository 
    {
        private ClientesDbContext _ctx;

        public ClienteRepository(ClientesDbContext context)
        {
            _ctx = context;
        }

        public async Task<Guid id> Add(Cliente cliente, CancellationToken ct = default)
        {
            await _ctx.Cliente.AddAsync(cliente, ct);
            await _ctx.SaveChanesAsync(ct);
        }

        public async Task<Cliente> GetClienteById(Guid id, CancellationToken ct = default)
        {
            return await _ctx.Cliente.FindAsync(id, ct)
                ?? throw new KeyNotFoundException($"El cliente con id: {id} no ha sido encontrado");
        }

        public async Task<Cliente> UpdateCliente(Cliente cliente, CancellationToken ct = default)
        {
            _ctx.Cliente.Update(cliente);
            await _ctx.Cliente.SaveChanesAsync(ct);
            return cliente;
        }

        public async Task<Guid id> DeleteClienteById(Guid id, CancellationToken ct = default)
        {
            var cliente = _ctx.Cliente.FindAsync(id) 
                ?? throw new KeyNotFoundException($"El cliente con id: {id} no se ha encontrado");

            _ctx.Cliente.Remove(cliente);
            await _ctx.Cliente.SaveChanesAsync(ct);
            return id
        }
    }
}