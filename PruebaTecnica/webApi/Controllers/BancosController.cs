using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Repositories.Interfaces;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancosController : ControllerBase
    {
        private IRepositoryAsync<Banco> bancosRepository;
        public BancosController(IRepositoryAsync<Banco> repositoryAsync)
        {

            this.bancosRepository = repositoryAsync;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await bancosRepository.GetAll());

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        //endpoints
        [HttpPost]
        [Route("ObtenerBanco")]
        public async Task<IActionResult> ObtenerCategoria(Banco model)
        {
            try
            {
                return Ok(await bancosRepository.GetByID(model.Idbancos));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
    }
}
