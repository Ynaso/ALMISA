using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? DireccionOficinaPrincipal { get; set; }
        public string? Telefono { get; set; }
        public int? IdCiudadOficinaPrincipal { get; set; }

        public virtual Ciudade? oCiudad { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
