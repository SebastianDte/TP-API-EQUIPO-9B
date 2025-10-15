using API.DTOs;
using API.Validaciones;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    /// <summary>
    /// Controlador para manejar las operaciones de artículos.
    /// </summary>
    public class ArticuloController : ApiController
    {

        /// <summary>
        /// Devuelve la lista de artículos.
        /// </summary>
        // GET: api/Articulo
        [HttpGet]
        public IEnumerable<ArticuloDto> Get()
        {
            ArticuloNegocio articuloNeg = new ArticuloNegocio();
            var articulos = articuloNeg.lista();


            var articulosDto = articulos.Select(a => new ArticuloDto
            {
                Codigo = a.codigo,
                Nombre = a.nombre,
                Descripcion = a.descripcion,
                Precio = a.precio,
                PrecioFormateado = a.PrecioFormateado,
                PrimeraImagenUrl = a.PrimeraImagenUrl,
                Marca = new MarcaDto { Descripcion = a.marca.descripcion },
                Categoria = new CategoriaDto { Descripcion = a.categoria.descripcion },
                Imagenes = a.Imagenes.Select(i => new ImagenDto { ImageUrl = i.imageUrl }).ToList()
            }).ToList();

            return articulosDto;
        }

        /// <summary>
        /// Da de alta un nuevo artículo en la base de datos.
        /// </summary>
        // POST:api/Articulo
        [HttpPost]
        public IHttpActionResult Post([FromBody] ArticuloAltaDto nuevoArticulo)
        {
            var validacion = ValidadorArticulo.ValidarFormato(nuevoArticulo);
            if (!validacion.EsValido)
                return BadRequest(validacion.Error);

            ArticuloNegocio negocio = new ArticuloNegocio();

            if (!negocio.MarcaExiste(nuevoArticulo.IdMarca))
                return BadRequest("El ID de la marca no existe.");
            if (!negocio.CategoriaExiste(nuevoArticulo.IdCategoria))
                return BadRequest("El ID de la categoría no existe.");

            Articulo articulo = new Articulo
            {
                codigo = nuevoArticulo.Codigo,
                nombre = nuevoArticulo.Nombre,
                descripcion = nuevoArticulo.Descripcion,
                precio = nuevoArticulo.Precio,
                categoria = new Categoria { id = nuevoArticulo.IdCategoria },
                marca = new Marca { id = nuevoArticulo.IdMarca },
                Imagenes = new List<Imagen>()
            };

            int idGenerado = negocio.agregar(articulo);

            return Ok(new { mensaje = "Producto agregado correctamente.", id = idGenerado });
        }

        public IHttpActionResult Put(int id, [FromBody] ArticuloAltaDto Articulo)
        {
            var validacion = ValidadorArticulo.ValidarFormato(Articulo);
            if (!validacion.EsValido)
                return BadRequest(validacion.Error);

            ArticuloNegocio negocio = new ArticuloNegocio();

            if (!negocio.MarcaExiste(Articulo.IdMarca))
                return BadRequest("El ID de la marca no existe.");
            if (!negocio.CategoriaExiste(Articulo.IdCategoria))
                return BadRequest("El ID de la categoría no existe.");

            Articulo articulo = new Articulo
            {
                id = id,
                codigo = Articulo.Codigo,
                nombre = Articulo.Nombre,
                descripcion = Articulo.Descripcion,
                precio = Articulo.Precio,
                categoria = new Categoria { id = Articulo.IdCategoria },
                marca = new Marca { id = Articulo.IdMarca },
                Imagenes = new List<Imagen>()
            };


            negocio.Modificar(articulo);
            int idArt = articulo.id;

            return Ok(new { mensaje = "Producto modificado correctamente.", id = idArt });
        }

        /// <summary>
        /// Elimina de manera fisica un artículo en la base de datos.
        /// </summary>
        // DELETE:api/Articulo
        [HttpDelete]
        public HttpResponseMessage Delete(int Id)
        {
            var negocio = new ArticuloNegocio();
            var lista = negocio.lista();

            Articulo artEliminar = negocio.lista().Find(x => x.id == Id);

            if( artEliminar == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "El articulo no existe.");

            negocio.eliminar(Id);

            return Request.CreateResponse(HttpStatusCode.OK, "Articulo eliminado correctamente.");
        }

        /// <summary>
        /// Busca el artículo mediante su ID en la base de datos.
        /// </summary>
        // GET:api/Articulo
        //[HttpGET]
        public HttpResponseMessage Get(int Id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.lista();
            Articulo articulo = lista.Find(x => x.id == Id);

            if (articulo == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "El articulo no existe.");

            ArticuloDto articuloDto = new ArticuloDto();
            articuloDto.Codigo = articulo.codigo;
            articuloDto.Nombre = articulo.nombre;
            articuloDto.Descripcion = articulo.descripcion;
            articuloDto.Precio = articulo.precio;
            articuloDto.PrecioFormateado = articulo.PrecioFormateado;
            articuloDto.PrimeraImagenUrl = articulo.PrimeraImagenUrl;
            articuloDto.Marca = new MarcaDto { Descripcion = articulo.marca.descripcion };
            articuloDto.Categoria = new CategoriaDto { Descripcion = articulo.categoria.descripcion };
            articuloDto.Imagenes = articulo.Imagenes.Select(i => new ImagenDto { ImageUrl = i.imageUrl })
            .ToList();
            
            return Request.CreateResponse(HttpStatusCode.OK, articuloDto);

        }


    }

}


