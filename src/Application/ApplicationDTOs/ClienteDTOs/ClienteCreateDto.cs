namespace ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs
{
    public class ClienteCreateDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string DireccionPrincipal { get; set; } = null!;
        public string NotasGenerales { get; set; } = null!;
    }
}