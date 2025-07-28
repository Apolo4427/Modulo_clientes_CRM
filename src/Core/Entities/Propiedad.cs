using ClientesCRM.src.Core.Enums;

namespace ClientesCRM.src.Core.Entities
{
    public class Propiedad
    {
        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public string Calle { get; private set; }
        public StateCity Estado { get; private set; }
        public int CodigoZip { get; private set; }
        protected Propiedad() { }

        public Propiedad(Guid ClienteId, string Calle, StateCity stateCity, int CodigoZip)
        {
            this.ClienteId = ClienteId;
            this.Calle = Calle;
            this.Estado = stateCity;
            this.CodigoZip = CodigoZip;
        }
         
    }
}
