using API.DTOs;
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
    public class ArticuloController : ApiController
    {
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

        // GET: api/Articulo/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Articulo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Articulo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulo/5
        public void Delete(int id)
        {
        }
    }
}
