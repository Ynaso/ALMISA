using Microsoft.AspNetCore.Mvc.Rendering;
namespace ALMISA.Models.ViewModels
{
    public class ProductorVM
    {
     public Productore oProductor { get; set; }
     public List<SelectListItem> oCiudad { get; set; }

    }
}
