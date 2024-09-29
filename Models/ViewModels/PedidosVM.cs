using Microsoft.AspNetCore.Mvc.Rendering;
namespace ALMISA.Models.ViewModels
{
    public class PedidosVM
    {
     public Pedido oPedidos { get; set; }
     public List<SelectListItem> oVendedor { get; set; }

     public List<ProductosDelPedido> oProductosDelPedido { get; set; }
     public List<SelectListItem> oCiudad { get; set; }
     public List<Cliente> oCliente { get; set; }
     public List<Producto> oProducto { get; set; }

    

    }
}
