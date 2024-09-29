using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ALMISA.Controllers
{
    public class CiudadesController : Controller
    {
        // GET: CategoriasController

        private readonly ALMISAContext _DBContext;

        public CiudadesController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Ciudades()
        {
            List<Ciudade> Lista = _DBContext.Ciudades.ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_ciudades(int IdCiudad)
        {
            CiudadVM oCiudadVM = new CiudadVM()
            {
                oCiudad = new Ciudade(),


            };

            if (IdCiudad != 0)
            {
                oCiudadVM.oCiudad = _DBContext.Ciudades.Find(IdCiudad);
            }

            return View(oCiudadVM);
        }

        [HttpPost]
        public IActionResult detalle_ciudades(CiudadVM oCiudadVM)
        {
            if (oCiudadVM.oCiudad.IdCiudad == 0)
            {
                _DBContext.Ciudades.Add(oCiudadVM.oCiudad);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Ciudades", "Ciudades");
        }

    }
}
