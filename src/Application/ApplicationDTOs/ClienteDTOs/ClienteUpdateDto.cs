// ClienteUpdateDto.cs
namespace ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs
{
    public class ClienteUpdateDto
    {
        public string Telefono { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string DireccionPrincipal { get; set; } = null!;
    }
}
