using ClientesCRM.src.Core.Entities;
using ClientesCRM.src.Core.Interfaces.IQueries.IClienteQueries;
using ClientesCRM.src.Core.Interfaces.IRepositories;

namespace ClientesCRM.src.Application.Handlers.ClienteHandlers
{
    public class GetClienteByIdHandler : IGetClienteByIdHandler
    {
        private readonly IClienteRepository _repo;

        public GetClienteByIdHandler(IClienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<Cliente> HandleAsync(GetClienteByIdQuery query)
        {
            return await _repo.GetClienteById(query.Id);
        }
    }
}

