using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using webApp.Models;

namespace webApp.Controllers
{
    public class CotizacionesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CotizacionesController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Cotizaciones/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoCotizaciones = JsonConvert.DeserializeObject<List<Cotizacione>>(content);
            return View(ListadoCotizaciones);
        }
        public async Task<IActionResult> Eliminar(int idCotizacione)
        {
            Cotizacione cotizacioneEliminar = new Cotizacione()
            {
                Idcotizaciones = idCotizacione
            };
            var contentSerialized = JsonConvert.SerializeObject(cotizacioneEliminar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Cotizciones/Eliminar", contentToSend);
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
        public async Task<IActionResult> Insertar(Cotizacione model)
        {
            model.Idcotizaciones = 0;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Cotizacione cotizacioneInsertar = new Cotizacione()
            {
                FechaCotizacion = model.FechaCotizacion,
                Total = model.Total,
                Cliente = model.Cliente,
                Estado  = model.Estado,
                Idproducto = model.Idproducto
            };
            var contentSerialized = JsonConvert.SerializeObject(cotizacioneInsertar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Cotizaciones/Insertar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Editar(int idCotizacione)
        {
            Cotizacione cotizacioneEditar = new Cotizacione()
            {
                Idcotizaciones = idCotizacione
            };
            var contentSerialized = JsonConvert.SerializeObject(cotizacioneEditar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Cotizaciones/ObtenerCategoria", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Cotizacione>(content);
            if (content == null)
            {
                return View("Error");
            }
            HttpContext.Session.SetInt32("IdCotizacione", idCotizacione);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Cotizacione model)
        {
            model.Idcotizaciones = (int)HttpContext.Session.GetInt32("IdCotizaciones");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Cotizacione cotizacioneModificar = new Cotizacione()
            {
                Idcotizaciones = model.Idcotizaciones,
                FechaCotizacion = model.FechaCotizacion,
                Total = model.Total,
                Cliente = model.Cliente,
                Estado = model.Estado,
                Idproducto = model.Idproducto
            };
            var contentSerialized = JsonConvert.SerializeObject(cotizacioneModificar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Cotizaciones/Modificar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}
