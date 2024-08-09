using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using webApp.Models;

namespace webApp.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductosController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Productos/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoProductos = JsonConvert.DeserializeObject<List<ProductoVM >>(content);
            return View(ListadoProductos);
        }
        public async Task<IActionResult> Eliminar(int idProducto)
        {
            Producto productoEliminar = new Producto()
            {
                IdProducto = idProducto
            };
            var contentSerialized = JsonConvert.SerializeObject(productoEliminar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Productos/Eliminar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                return View("Error");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Insertar()
        {
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Categorias/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoCategorias = JsonConvert.DeserializeObject<List<Categoria>>(content);
            ViewData["Categorias"] = ListadoCategorias;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insertar(Producto model)
        {
            model.IdProducto = 0;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Producto ProductoInsertar = new Producto()
            {
                Producto1 = model.Producto1,
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Existencia = model.Existencia,    
                FechaIng = model.FechaIng,
                Idcategoria = model.Idcategoria
            };
            var contentSerialized = JsonConvert.SerializeObject(ProductoInsertar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Productos/Insertar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Editar(int IdProducto)
        {
            Producto ProductoEditar = new Producto()
            {
                IdProducto = IdProducto
            };
            var contentSerialized = JsonConvert.SerializeObject(ProductoEditar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var clientProducto = _httpClientFactory.CreateClient("Base");
            var responseProducto = await clientProducto.PostAsync("Productos/ObtenerProducto", contentToSend);
            if (!responseProducto.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var contentProducto = await responseProducto.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Producto>(contentProducto);
            if (contentProducto == null)
            {
                return View("Error");
            }
            HttpContext.Session.SetInt32("IdProducto", ProductoEditar.IdProducto);
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Categorias/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoCategorias = JsonConvert.DeserializeObject<List<Categoria>>(content);
            ViewData["Categorias"] = ListadoCategorias;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Producto model)
        {
            model.IdProducto = (int)HttpContext.Session.GetInt32("IdProducto");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Producto productoModificar = new Producto()
            {
                IdProducto = model.IdProducto,
                Producto1 = model.Producto1,    
                Descripcion = model.Descripcion,
                Precio = model.Precio,
                Existencia = model.Existencia,
                FechaIng = model.FechaIng,
                Idcategoria = model.Idcategoria
            };
            var contentSerialized = JsonConvert.SerializeObject(productoModificar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Productos/Modificar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }
        
    }
}
