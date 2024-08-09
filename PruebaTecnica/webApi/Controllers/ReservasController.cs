using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories.Interfaces;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private IRepositoryAsync<Reserva> reservasRepository;

        public ReservasController(IRepositoryAsync<Reserva> repositoryAsync)
        {
            this.reservasRepository = repositoryAsync;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await reservasRepository.GetAll());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }

        //endpoints
        [HttpPost]
        [Route("ObtenerProducto")]
        public async Task<IActionResult> ObtenerReservas(Reserva model)
        {
            try
            {
                return Ok(await reservasRepository.GetByID(model.Idreservas));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> InsertarReserva(Reserva model)
        {
            try
            {
                return Ok(await reservasRepository.Insert(model));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> ModificarReserva(Reserva model)
        {
            try
            {
                await reservasRepository.Update(model);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarReserva(Reserva model)
        {
            try
            {
                return Ok(await reservasRepository.Delete(model.Idreservas));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
    }
}
