using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApi.Models;
using webApi.Models.DTO;
using webApi.Repositories.Interfaces;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private IRepositoryAsync<Producto> productosRepository;
        private IRepositoryAsync<Categoria> categoriasRepository;
        
        public ProductosController(IRepositoryAsync<Producto> repositoryAsync, IRepositoryAsync<Categoria> categoriasRepository)
        {
            this.productosRepository = repositoryAsync;
            this.categoriasRepository = categoriasRepository;
        }

        [HttpGet]
        [Route("Listado")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Producto> listadoProductos=await productosRepository.GetAll();
                List<Categoria> listadoCategorias=await categoriasRepository.GetAll();
                List<ProductoVM> listadoProductosVM = listadoProductos.Join(
                    listadoCategorias,
                    producto=>producto.Idcategoria,
                    categoria=>categoria.Idcategoria,
                    (producto,categoria)=>new ProductoVM
                    {
                        IdProducto=producto.IdProducto,
                        Producto1=producto.Producto1,
                        Precio=producto.Precio,
                        Idcategoria = producto.Idcategoria,
                        Existencia=producto.Existencia,
                        FechaIng = producto.FechaIng,
                        Descripcion = producto.Descripcion,
                        Categoria = categoria.Categoria1
                    }).ToList();
                return Ok(listadoProductosVM);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        //endpoints
        [HttpPost]
        [Route("ObtenerProducto")]
        public async Task<IActionResult> ObtenerProducto(Producto model)
        {
            try
            {
                return Ok(await productosRepository.GetByID(model.IdProducto));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Insertar")]
        public async Task<IActionResult> InsertarProducto(Producto model)
        {
            try
            {
                return Ok(await productosRepository.Insert(model));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Modificar")]
        public async Task<IActionResult> ModificarProducto(Producto model)
        {
            try
            {
                await productosRepository.Update(model);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
        [HttpPost]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarProducto(Producto model)
        {
            try
            {
                return Ok(await productosRepository.Delete(model.IdProducto));

            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
        }
    }
}
