using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ALMISA.Controllers
{
    public class ProductosController : Controller
    {
        // GET: CategoriasController

        private readonly ALMISAContext _DBContext;

        public ProductosController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Productos()
        {
            List<Producto> Lista = _DBContext.Productos.Include(c=>c.oCategoria).Include(p=>p.oProductor).ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_productos(int IdProducto)
        {
            ProductosVM oProductoVM = new ProductosVM()
            {
                oProducto = new Producto(),

                oCategoria = _DBContext.Categorias.Select(categoria => new SelectListItem()
                  {
                      Text = categoria.Nombre,
                      Value = categoria.IdCategoria.ToString()
                  }).ToList(),


                oProductor = _DBContext.Productores.Select(Productores => new SelectListItem()
                {
                    Text = Productores.Nombre,
                    Value = Productores.IdProductor.ToString()
                }).ToList(),

            };
            return View(oProductoVM);
        }

        [HttpPost]
        public IActionResult detalle_productos(ProductosVM oProductoVM)
        {
            if (oProductoVM.oProducto.IdProducto == 0)
            {
                _DBContext.Productos.Add(oProductoVM.oProducto);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Productos", "Productos");
        }

    }
}
