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


    }
}
