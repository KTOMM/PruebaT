using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories.Interfaces;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionesController : ControllerBase
    {
        private IRepositoryAsync<Cotizacione> cotizacionesRepository;

        public CotizacionesController(IRepositoryAsync<Cotizacione> repositoryAsync)
        {
            this.cotizacionesRepository = repositoryAsync;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await cotizacionesRepository.GetAll());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

        //endpoints
        [HttpPost]
        [Route("ObtenerProducto")]
        public async Task<IActionResult> ObtenerCotizacion(Cotizacione model)
        {
            try
            {
                return Ok(await cotizacionesRepository.GetByID(model.Idcotizaciones));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> InsertarCotizacion(Cotizacione model)
        {
            try
            {
                return Ok(await cotizacionesRepository.Insert(model));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> ModificarCotizacion(Cotizacione model)
        {
            try
            {
                await cotizacionesRepository.Update(model);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarCotizacion(Cotizacione model)
        {
            try
            {
                return Ok(await cotizacionesRepository.Delete(model.Idcotizaciones));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
    }
}
