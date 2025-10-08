﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.DTOs
{
    public class ArticuloAltaDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; } 
        public int IdMarca { get; set; }    
    }
}