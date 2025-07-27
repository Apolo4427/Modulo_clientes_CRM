using ClientesCRM.src.Application.ApplicationDTOs.PropiedadDTOs;

namespace ClientesCRM.src.Core.Interfaces.IRepositories
{
    public interface IPropiedadRepository
    {
        public Task<Guid> Add(PropiedadCreateDto propiedad, CancellationToken ct = default);
        public Task<PropiedadResponseDto> GetPropiedad(Guid id, CancellationToken ct = default);
    }
}