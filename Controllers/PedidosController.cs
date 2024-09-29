using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ALMISA.Models;
using ALMISA.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;



namespace ALMISA.Controllers
{
    
    public class PedidosController : Controller
    {
     
        private readonly ALMISAContext _DBContext;

        public PedidosController(ALMISAContext context)
        {
            _DBContext = context;
        }

        public IActionResult Pedidos()
        {
            List<Pedido> Lista = _DBContext.Pedidos.Include(v=>v.oVendedor).Include(c => c.oCiudad).Include(c => c.oCliente).ToList();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult detalle_pedidos(int IdPedido)
        {
            PedidosVM oPedidosVM = new PedidosVM()
            {

                oPedidos =  new Pedido(),

                oVendedor = _DBContext.Vendedores.Select(vendedor => new SelectListItem()
                {
                    Text = vendedor.Nombres + " " + vendedor.Apellidos,
                    Value = vendedor.IdVendedor.ToString()
                }).ToList(),

                oCiudad = _DBContext.Ciudades.Select(Ciudad => new SelectListItem()
                {
                    Text = Ciudad.Nombre,
                    Value = Ciudad.IdCiudad.ToString()
                }).ToList(),

                oCliente = _DBContext.Clientes.ToList(),

                oProducto = _DBContext.Productos.ToList()
            };

            if (IdPedido != 0)
            {
                // Obtener el pedido
                oPedidosVM.oPedidos = _DBContext.Pedidos.Find(IdPedido);

                oPedidosVM.oProductosDelPedido = _DBContext.ProductosDelPedidos
                 .Where(p => p.IdPedido == IdPedido)
                 .Include(p => p.oProductos)
                 .ToList();
            }



            return View(oPedidosVM);
        }

        /* [HttpPost]
         public IActionResult detalle_pedidos(PedidosVM oPedidosVM, List<int> arrayIdProducto, List<string> arrayNombreProducto, List<int> arrayCantidadProducto, List<decimal> arrayPrecio, List<decimal> arrayDescuento)
         {



             if (oPedidosVM.oPedidos.IdPedido == 0)
             {
                 _DBContext.Pedidos.Add(oPedidosVM.oPedidos);
             }

             _DBContext.SaveChanges();

             for (int i = 0; i <= arrayIdProducto.Count; i++)
              {
                  var idProducto = arrayIdProducto[i];
                  var cantidad = arrayCantidadProducto[i];
                  var precioVenta = arrayPrecio[i];
                  var descuento = arrayDescuento[i];

                  var producto = _DBContext.Productos.FirstOrDefault(p => p.IdProducto == idProducto);
                  if (producto != null)
                  {
                      producto.UnidadesEnBodega -= cantidad;
                      _DBContext.Productos.Update(producto);
                  }
                  else
                  {
                      Debug.WriteLine($"Producto con ID {idProducto} no encontrado.");
                  } 
              }

             return RedirectToAction("Index", "Home");
         } */

        public async Task<IActionResult> detalle_pedidos(PedidosVM oPedidosVM, List<int> arrayCantidadProducto, List<decimal> arrayPrecio, List<int> arrayDescuento, List<int> arrayIdProducto)
        {
            if (arrayCantidadProducto.Count != arrayPrecio.Count || arrayCantidadProducto.Count != arrayDescuento.Count || arrayCantidadProducto.Count != arrayIdProducto.Count)
            {
                return BadRequest("Los tamaños de los arreglos no coinciden.");
            }

            if (oPedidosVM.oPedidos.IdPedido == 0)
            {
                using (var transaction = await _DBContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _DBContext.Pedidos.Add(oPedidosVM.oPedidos);
                        await _DBContext.SaveChangesAsync();

                        var idPedido = oPedidosVM.oPedidos.IdPedido;

                        for (int i = 0; i < arrayCantidadProducto.Count; i++)
                        {
                            var productoDelPedido = new ProductosDelPedido
                            {
                                IdPedido = idPedido,
                                IdProducto = arrayIdProducto[i],
                                CantidadUnidadesRequeridas = arrayCantidadProducto[i],
                                CantidadUnidadesDespachadas = arrayCantidadProducto[i],
                                PrecioUnitarioAplicado = arrayPrecio[i],
                                PorcentajeDeDescuentoAplicado = arrayDescuento[i]
                            };

                            var producto = _DBContext.Productos.FirstOrDefault(p => p.IdProducto == arrayIdProducto[i]);

                            if (producto.UnidadesEnBodega >= arrayCantidadProducto[i])
                            {
                                producto.UnidadesEnBodega -= arrayCantidadProducto[i];
                            }
                            else
                            {
                                // Notifica o maneja la falta de existencias aquí
                                return StatusCode(400, "No hay suficientes existencias para el producto " + producto.Nombre);
                            }

                            _DBContext.ProductosDelPedidos.Add(productoDelPedido);
                        }

                        await _DBContext.SaveChangesAsync();
                        await transaction.CommitAsync();

                        return RedirectToAction("Pedidos", "Pedidos");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, "Ocurrió un error al guardar el pedido.");
                    }
                }
            }
            else
            {
                var pedidoExistente = await _DBContext.Pedidos.FindAsync(oPedidosVM.oPedidos.IdPedido);

                if (pedidoExistente != null)
                {
                    // Actualiza los campos del pedido
                    _DBContext.Entry(pedidoExistente).CurrentValues.SetValues(oPedidosVM.oPedidos);

                    // Obtén los productos del pedido existente
                    var productosDelPedidoExistentes = await _DBContext.ProductosDelPedidos
                        .Where(p => p.IdPedido == oPedidosVM.oPedidos.IdPedido)
                        .ToListAsync();

                    // Recorre los productos actuales y actualiza los valores
                    foreach (var productoDelPedido in productosDelPedidoExistentes)
                    {
                        var index = arrayIdProducto.IndexOf(productoDelPedido.IdProducto);
                        if (index >= 0)
                        {
                            // Actualiza los valores del producto
                            productoDelPedido.CantidadUnidadesRequeridas = arrayCantidadProducto[index];
                            productoDelPedido.CantidadUnidadesDespachadas = arrayCantidadProducto[index];
                            productoDelPedido.PrecioUnitarioAplicado = arrayPrecio[index];
                            productoDelPedido.PorcentajeDeDescuentoAplicado = arrayDescuento[index];
                        }
                    }

                    // Guarda los cambios
                    await _DBContext.SaveChangesAsync();

                    return RedirectToAction("Pedidos", "Pedidos");
                }
                else
                {
                    return StatusCode(404, "El pedido no existe.");
                }
            }

        }



    }
}
