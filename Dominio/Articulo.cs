using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int id { get; set; }

        [DisplayName("Código")]
        public string codigo { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [DisplayName("Descripción")]
        public string descripcion { get; set; }
        [DisplayName("Precio_")]
        public decimal precio { get; set; }
        [DisplayName("Categoria")]
        public Categoria categoria { get; set; }
        [DisplayName("Marca")]
        public Marca marca { get; set; }
        public List<Imagen> Imagenes { get; set; } = new List<Imagen>();

        [DisplayName("Precio")]
        public string PrecioFormateado
        {
            get
            {
                return precio.ToString("C", new CultureInfo("es-AR"));
            }
        }
        public string PrimeraImagenUrl
        {
            get
            {
                return Imagenes != null && Imagenes.Count > 0
                    ? Imagenes[0].imageUrl
                    : "img/default.png";
            }
        }



    }
}
