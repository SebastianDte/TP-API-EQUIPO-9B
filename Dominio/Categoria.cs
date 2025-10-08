using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Dominio
{
    public class Categoria
    {
        public int id { get; set; }
        [DisplayName("Descripción")]
        public string descripcion { get; set; }

        public override string ToString()
        {
            return this.descripcion;
        }
    }
}
