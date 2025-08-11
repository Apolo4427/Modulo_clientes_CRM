namespace ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs
{
    public class ClienteResponseDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string direccionPrincipal { get; set; } = null!;
        public string NotasGenerales { get; set; } = null!;
    }


}