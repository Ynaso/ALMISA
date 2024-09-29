using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string? CodigoBarras { get; set; }
        public string Nombre { get; set; } = null!;
        public string? PresentacionComercial { get; set; }
        public decimal? PrecioUnitarioActual { get; set; }
        public decimal? PesoEnKilogramos { get; set; }
        public int? UnidadesEnBodega { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdProductor { get; set; }

        public virtual Categoria? oCategoria { get; set; }
        public virtual Productore? oProductor { get; set; }
    }
}
