using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ALMISA.Controllers
{
    public class ProductoresController : Controller
    {
        // GET: CategoriasController

        private readonly ALMISAContext _DBContext;

        public ProductoresController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Productores()
        {
            List<Productore> Lista = _DBContext.Productores.Include(c=>c.oCiudad).ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_productores(int IdProductor)
        {
            ProductorVM oProductorVM = new ProductorVM()
            {
                oProductor = new Productore(),

                oCiudad = _DBContext.Ciudades.Select(Ciudad => new SelectListItem()
                  {
                      Text = Ciudad.Nombre,
                      Value = Ciudad.IdCiudad.ToString()
                  }).ToList(),

            };

            if (IdProductor != 0)
            {
                oProductorVM.oProductor = _DBContext.Productores.Find(IdProductor);
            }


            return View(oProductorVM);
        }

        [HttpPost]
        public IActionResult detalle_productores(ProductorVM oProductorVM)
        {
            if (oProductorVM.oProductor.IdProductor == 0)
            {
                _DBContext.Productores.Add(oProductorVM.oProductor);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Productores", "Productores");
        }

    }
}
