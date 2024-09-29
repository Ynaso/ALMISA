using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime? FechaRealizacion { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public DateTime? FechaRealEntrega { get; set; }
        public string? EstadoActual { get; set; }
        public string? DireccionDestino { get; set; }
        public string? PersonaQueRecibe { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdCiudadDestino { get; set; }
        public int? IdCliente { get; set; }

        public virtual Ciudade? oCiudad { get; set; }
        public virtual Cliente? oCliente { get; set; }
        public virtual Vendedore? oVendedor { get; set; }
    }
}
