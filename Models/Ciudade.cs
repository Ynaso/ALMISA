using System;
using System.Collections.Generic;

namespace ALMISA.Models
{
    public partial class Ciudade
    {
        public Ciudade()
        {
            Clientes = new HashSet<Cliente>();
            Pedidos = new HashSet<Pedido>();
            Productores = new HashSet<Productore>();
            Vendedores = new HashSet<Vendedore>();
        }

        public int IdCiudad { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Productore> Productores { get; set; }
        public virtual ICollection<Vendedore> Vendedores { get; set; }
    }
}
