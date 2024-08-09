using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using webApp.Models;

namespace webApp.Controllers
{
    public class BancosController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BancosController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("Base");
            var response = await client.GetAsync("Bancos/Listado");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Data = "Error en la solicitud";
            }
            var content = await response.Content.ReadAsStringAsync();
            var ListadoBancos = JsonConvert.DeserializeObject<List<Banco>>(content);
            return View(ListadoBancos);
        }
    }
}
