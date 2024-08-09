using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
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
            //var client = _httpClientFactory.CreateClient("Base");
            //var response = await client.GetAsync("Categorias/Listado");
            //if (!response.IsSuccessStatusCode)
            //{
            //    ViewBag.Data = "Error en la solicitud";
            //}
            //var content = await response.Content.ReadAsStringAsync();
            ////var ListadoCategorias = JsonConvert.DeserializeObject<List<Categoria>>(content);
            //return View(ListadoCategorias);
            return View();
        }
    }
}