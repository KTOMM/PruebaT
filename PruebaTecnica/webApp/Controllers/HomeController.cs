using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using webApp.Models;

namespace webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Categorias/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoCategorias = JsonConvert.DeserializeObject<List<Categoria>>(content);
            return View(ListadoCategorias);
        }

        public async Task<IActionResult> Eliminar(int idCategoria)
        {
            Categoria categoriaEliminar = new Categoria()
            {
                Idcategoria = idCategoria
            };
            var contentSerialized = JsonConvert.SerializeObject(categoriaEliminar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Categorias/Eliminar",contentToSend);
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

        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insertar(Categoria model)
        {
            model.Idcategoria = 0;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Categoria categoriaInsertar = new Categoria()
            {
                Categoria1 = model.Categoria1
            };
            var contentSerialized = JsonConvert.SerializeObject(categoriaInsertar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Categorias/Insertar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Editar(int idCategoria)
        {
            Categoria categoriaEditar = new Categoria()
            {
                Idcategoria = idCategoria
            };
            var contentSerialized = JsonConvert.SerializeObject(categoriaEditar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Categorias/ObtenerCategoria", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var model=JsonConvert.DeserializeObject<Categoria>(content);
            if (content == null)
            {
                return View("Error");
            }
            HttpContext.Session.SetInt32("IdCategoria",idCategoria);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria model)
        {
            model.Idcategoria = (int)HttpContext.Session.GetInt32("IdCategoria");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Categoria categoriaModificar = new Categoria()
            {
                Idcategoria = model.Idcategoria,
                Categoria1 = model.Categoria1
            };
            var contentSerialized = JsonConvert.SerializeObject(categoriaModificar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Categorias/Modificar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}