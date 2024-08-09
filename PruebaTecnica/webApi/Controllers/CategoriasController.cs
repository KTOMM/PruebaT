using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApi.Models;
using webApi.Repositories.Interfaces;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private IRepositoryAsync<Categoria> categoriasRepository;
        public CategoriasController(IRepositoryAsync<Categoria> repositoryAsync) { 

            this.categoriasRepository = repositoryAsync;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await categoriasRepository.GetAll());
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        //endpoints
        [HttpPost]
        [Route("ObtenerCategoria")]
        public async Task<IActionResult> ObtenerCategoria(Categoria model)
        {
            try
            {
                return Ok(await categoriasRepository.GetByID(model.Idcategoria));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> InsertarCategoria(Categoria model)
        {
            try
            {
                return Ok(await categoriasRepository.Insert(model));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> ModificarCategoria(Categoria model)
        {
            
            try
            {
                await categoriasRepository.Update(model);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarCategoria(Categoria model)
        {
            try
            {
                return Ok(await categoriasRepository.Delete(model.Idcategoria));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
    }

}
