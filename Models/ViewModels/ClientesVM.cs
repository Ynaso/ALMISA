using Microsoft.AspNetCore.Mvc.Rendering;
namespace ALMISA.Models.ViewModels
{
    public class ClientesVM
    {
     public Cliente oCliente { get; set; }
     public List<SelectListItem> oCiudad { get; set; }
    }
}
