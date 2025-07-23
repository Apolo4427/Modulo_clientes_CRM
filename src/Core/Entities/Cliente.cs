namespace ClientesCRM.src.Core.Entities
{
    public class Cliente
    {
        public Guid Id { get; private set; }
        public string NombreCompleto { get; private set; }
        public string Telefono { get; private set; }
        public string CorreoElectronico { get; private set; }
        public string DireccionPrincipal { get; private set; }
        public string NotasGenerales { get; private set; }

        // Relaciones
        public ICollection<Propiedad> Propiedades { get; private set; } = new List<Propiedad>();
        // public ICollection<Comunicacion> Comunicaciones { get; private set; } = new List<Comunicacion>();

        public Cliente(string nombreCompleto, string telefono, string correoElectronico, string direccionPrincipal, string notasGenerales)
        {
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            DireccionPrincipal = direccionPrincipal;
            NotasGenerales = notasGenerales;
        }

        // Comportamientos de agregado
        public void ActualizarContacto(string telefono, string correoElectronico, string direccionPrincipal)
        {
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            DireccionPrincipal = direccionPrincipal;
        }

        public void AgregarNota(string nota)
            => NotasGenerales += Environment.NewLine + nota;

        public void AgregarPropiedad(Propiedad propiedad)
            => Propiedades.Add(propiedad);

        // public void AgregarComunicacion(Comunicacion comunicacion)
        //     => Comunicaciones.Add(comunicacion);
    }
}
