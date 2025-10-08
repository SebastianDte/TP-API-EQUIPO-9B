using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Vouncher
    {
        [DisplayName("Código")]
        public string CodigoVoucher { get; set; }

        [DisplayName("Cliente")]
        public Cliente Cliente { get; set; }

        [DisplayName("Fecha de Canje")]
        public DateTime? FechaCanje { get; set; }

        [DisplayName("Artículo")]
        public Articulo Articulo { get; set; }

        // Propiedad para saberi si el vouncher ya fue usado.
        public bool SePuedeCanjear => FechaCanje == null;
    }
}

