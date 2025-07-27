using ClientesCRM.src.Core.Enums;

namespace ClientesCRM.src.Application.ApplicationDTOs.PropiedadDTOs
{
    public class PropiedadResponseDto
    {
        public Guid ClienteId { get; set; }
        public string Calle { get; set; } = null!;
        public StateCity Estado { get; set; }
        public int CodigoZip { get; set; }  
    }
}