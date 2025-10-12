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
    /// <summary>
    /// Controlador para agregar Lista de Imagenes.
    /// </summary>
    public class ImagenController : ApiController
    {
        /// <summary>
        /// Da de alta una lista de imagenes en la Data Base.
        /// </summary>
        // POST: api/Imagen/3
        public IHttpActionResult Post(int id, [FromBody] List<ImagenDto> imagenesNuevas)
        {
            ImagenNegocio imagenes = new ImagenNegocio();
            int idArticulo = id;

            foreach (var imgDto in imagenesNuevas)
            {
                Imagen imagen = new Imagen();
                imagen.idArticulo = idArticulo;
                imagen.imageUrl = imgDto.ImageUrl;

                imagenes.AgregarImagen(imagen);
            }
            return Ok(new { mensaje = "Producto agregado correctamente.", id = idArticulo});
        }
    }
}
