using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ALMISA.Controllers
{
    public class ClientesController : Controller
    {
        // GET: CategoriasController

        private readonly ALMISAContext _DBContext;

        public ClientesController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Clientes()
        {
            List<Cliente> Lista = _DBContext.Clientes.Include(c=>c.oCiudad).ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_clientes(int IdCliente)
        {

            ClientesVM oClienteVM = new ClientesVM()
            {
                oCliente = new Cliente(),

                oCiudad = _DBContext.Ciudades.Select(ciudad => new SelectListItem()
                {
                    Text = ciudad.Nombre,
                    Value = ciudad.IdCiudad.ToString()
                }).ToList()
            };


            if (IdCliente != 0)
            {
                oClienteVM.oCliente = _DBContext.Clientes.Find(IdCliente);
            }



            return View(oClienteVM);
        }

        [HttpPost]
        public IActionResult detalle_clientes(ClientesVM oClienteVM)
        {
            if (oClienteVM.oCliente.IdCliente == 0)
            {
                _DBContext.Clientes.Add(oClienteVM.oCliente);
            }

            _DBContext.SaveChanges();

            return RedirectToAction("Clientes", "Clientes");
        }

    }
}
