using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;

namespace ALMISA.Controllers
{
    public class CategoriasController : Controller
    {
        // GET: CategoriasController

        private readonly ALMISAContext _DBContext;

        public CategoriasController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Categorias()
        {
            List<Categoria> Lista = _DBContext.Categorias.ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_categorias(int IdCategoria)
        {
            CategoriaVM oCategoriaVM = new CategoriaVM()
            {
                oCategoria = new Categoria()
               
            };

            if (IdCategoria != 0)
            {
                oCategoriaVM.oCategoria = _DBContext.Categorias.Find(IdCategoria);
            }
            return View(oCategoriaVM);
        }

        [HttpPost]
        public IActionResult detalle_categorias(CategoriaVM oCategoriaVM)
        {
            if(oCategoriaVM.oCategoria.IdCategoria == 0)
            {
                _DBContext.Categorias.Add(oCategoriaVM.oCategoria);
            }
            else
            {
                _DBContext.Categorias.Update(oCategoriaVM.oCategoria);
            }



            _DBContext.SaveChanges();

            return RedirectToAction("Categorias", "Categorias");
        }

        // GET: CategoriasController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriasController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoriasController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
