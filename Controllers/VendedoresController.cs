using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ALMISA.Controllers
{
    public class VendedoresController : Controller
    {
        // GET: CategoriasController

        private readonly ALMISAContext _DBContext;

        public VendedoresController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Vendedores()
        {
            List<Vendedore> Lista = _DBContext.Vendedores.Include(c=>c.oCiudad).ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_vendedores(int IdVendedor)
        {
            VendedorVM oVendedorVM = new VendedorVM()
            {
                oVendedor = new Vendedore(),

                oCiudad = _DBContext.Ciudades.Select(Ciudad => new SelectListItem()
                  {
                      Text = Ciudad.Nombre,
                      Value = Ciudad.IdCiudad.ToString()
                  }).ToList(),

            };

            if (IdVendedor != 0)
            {
                oVendedorVM.oVendedor = _DBContext.Vendedores.Find(IdVendedor);
            }

            return View(oVendedorVM);
        }

        [HttpPost]
        public IActionResult detalle_vendedores(VendedorVM oVendedorVM)
        {
            if (oVendedorVM.oVendedor.IdVendedor == 0)
            {
                _DBContext.Vendedores.Add(oVendedorVM.oVendedor);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Vendedores", "Vendedores");
        }

    }
}
