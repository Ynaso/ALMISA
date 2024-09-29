using Microsoft.AspNetCore.Mvc.Rendering;
namespace ALMISA.Models.ViewModels
{
    public class ProductosVM
    {
     public Producto oProducto { get; set; }
     public List<SelectListItem> oCategoria { get; set; }

        public List<SelectListItem> oProductor { get; set; }

    }
}
