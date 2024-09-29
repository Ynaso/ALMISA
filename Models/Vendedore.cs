using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class Vendedore
    {
        public Vendedore()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdVendedor { get; set; }
        public string DocumentoIdentidad { get; set; } = null!;
        public string? TipoDocumento { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaVinculacion { get; set; }
        public decimal? SalarioBase { get; set; }
        public int? IdVendedorJefe { get; set; }
        public int? IdCiudadBaseDeOperaciones { get; set; }

        public virtual Ciudade? oCiudad { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
