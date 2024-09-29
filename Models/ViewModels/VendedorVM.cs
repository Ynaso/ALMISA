using Microsoft.AspNetCore.Mvc.Rendering;
namespace ALMISA.Models.ViewModels
{
    public class VendedorVM
    {
     public Vendedore oVendedor { get; set; }
     public List<SelectListItem> oCiudad { get; set; }

    }
}
