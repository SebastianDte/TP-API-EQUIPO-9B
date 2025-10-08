using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DTOs
{
    public class ArticuloDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string PrecioFormateado { get; set; }
        public string PrimeraImagenUrl { get; set; }
        public MarcaDto Marca { get; set; }
        public CategoriaDto Categoria { get; set; }
        public List<ImagenDto> Imagenes { get; set; }
    }
}