using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class ProductosDelPedido
    {

        public int IdPedidosProducto { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int CantidadUnidadesRequeridas { get; set; }
        public int? CantidadUnidadesDespachadas { get; set; }
        public decimal? PrecioUnitarioAplicado { get; set; }
        public decimal? PorcentajeDeDescuentoAplicado { get; set; }

        public virtual Pedido oPedido { get; set; } = null!;
        public virtual Producto oProductos { get; set; } = null!;
    }
}
