using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class Productore
    {
        public Productore()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdProductor { get; set; }
        public string Nombre { get; set; } = null!;
        public string? DireccionOficinaPrincipal { get; set; }
        public string? Telefono { get; set; }
        public int? IdCiudadOficinaPrincipal { get; set; }

        public virtual Ciudade? oCiudad { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
