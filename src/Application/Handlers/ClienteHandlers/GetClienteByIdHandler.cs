using ClientesCRM.src.Core.Entities;
using ClientesCRM.src.Core.Interfaces.IQueries.IClienteQueries;
using ClientesCRM.src.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetClienteByIdHandler 
    : IRequestHandler<GetClienteByIdQuery, Cliente>  
{
    private readonly ClientesDbContext _ctx;
    public GetClienteByIdHandler(ClientesDbContext ctx) => _ctx = ctx;

    public async Task<Cliente> Handle(GetClienteByIdQuery query, CancellationToken cancellationToken)
    {
        var cliente = await _ctx.Clientes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(c => c.Id == query.Id, cancellationToken);

        if (cliente is null)
            throw new KeyNotFoundException($"No se encontró un cliente con Id = {query.Id}");

        return cliente;
    }
}


