using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using webApp.Models;

namespace webApp.Controllers
{
    public class ReservasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ReservasController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Reservas/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoReservas = JsonConvert.DeserializeObject<List<Reserva>>(content);
            return View(ListadoReservas);
        }
        public async Task<IActionResult> Eliminar(int idCotizacione)
        {
            Reserva ReservasEliminar = new Reserva()
            {
                Idreservas = idCotizacione
            };
            var contentSerialized = JsonConvert.SerializeObject(ReservasEliminar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Reservas/Eliminar", contentToSend);
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
            var response = await client.GetAsync("Reservas/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoReservas = JsonConvert.DeserializeObject<List<Reserva>>(content);
            ViewData["reservas"] = ListadoReservas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insertar(Reserva model)
        {
            model.Idreservas = 0;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Reserva CotizacioneInsertar = new Reserva()
            {
                Idreservas = model.Idreservas,
                FechaReserva = model.FechaReserva,
                Cliente = model.Cliente,    
                FechaIni = model.FechaIni,
                FechaFin = model.FechaFin,
                Estado = model.Estado,
                Idbanco = model.Idbanco,
                Idproducto = model.Idproducto
               
            };
            var contentSerialized = JsonConvert.SerializeObject(CotizacioneInsertar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Reservas/Insertar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Editar(int IdReserva)
        {
            Reserva ReservaEditar = new Reserva()
            {
                Idreservas = IdReserva

            };
            var contentSerialized = JsonConvert.SerializeObject(ReservaEditar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var clientReserva = _httpClientFactory.CreateClient("Base");
            var responseReserva = await clientReserva.PostAsync("Reservas/ObtenerReserva", contentToSend);
            if (!responseReserva.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var contentReserva = await responseReserva.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<Producto>(contentReserva);
            if (contentReserva == null)
            {
                return View("Error");
            }
            HttpContext.Session.SetInt32("IdReserva", ReservaEditar.Idreservas);
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Reservas/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoReservas= JsonConvert.DeserializeObject<List<Reserva>>(content);
            ViewData["Reservas"] = ListadoReservas;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Reserva model)
        {
            model.Idreservas = (int)HttpContext.Session.GetInt32("IdReserva");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Reserva cotizacioneModificar = new Reserva()
            {
                Idreservas = model.Idreservas,
                FechaReserva = model.FechaReserva,
                Cliente = model.Cliente,
                FechaIni = model.FechaIni,
                FechaFin = model.FechaFin,
                Estado = model.Estado,
                Idbanco = model.Idbanco,
                Idproducto = model.Idproducto
            };
            var contentSerialized = JsonConvert.SerializeObject(cotizacioneModificar);
            var contentToSend = new StringContent(contentSerialized, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.PostAsync("Reservas/Modificar", contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
                return View("Error");
            }
            return RedirectToAction("Index");

        }
    }
}
