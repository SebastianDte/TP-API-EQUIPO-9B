using API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace API.Validaciones
{
    public class ValidadorArticulo
    {
        public static (bool EsValido, string Error) ValidarFormato(ArticuloAltaDto articulo)
        {
            if (articulo == null)
                return (false, "El producto no puede ser nulo.");

            // Código
            if (string.IsNullOrWhiteSpace(articulo.Codigo))
                return (false, "El código es obligatorio.");
            if (articulo.Codigo.Length < 3 || articulo.Codigo.Length > 200)
                return (false, "El código debe tener entre 3 y 200 caracteres.");
            if (!Regex.IsMatch(articulo.Codigo, @"^[a-zA-Z0-9]+$"))
                return (false, "El código solo puede contener letras y números.");

            // Nombre
            if (string.IsNullOrWhiteSpace(articulo.Nombre))
                return (false, "El nombre es obligatorio.");
            if (articulo.Nombre.Length < 3 || articulo.Nombre.Length > 200)
                return (false, "El nombre debe tener entre 3 y 200 caracteres.");

            // Descripción
            if (string.IsNullOrWhiteSpace(articulo.Descripcion))
                return (false, "La descripción es obligatoria.");
            if (articulo.Descripcion.Length < 3 || articulo.Descripcion.Length > 200)
                return (false, "La descripción debe tener entre 3 y 200 caracteres.");

            // Precio
            if (articulo.Precio <= 0 || articulo.Precio > 1000000)
                return (false, "El precio debe ser mayor a 0 y menor a 1.000.000.");

            // IDs
            if (articulo.IdMarca <= 0)
                return (false, "El ID de la marca no puede estar vacío ni ser cero.");
            if (articulo.IdCategoria <= 0)
                return (false, "El ID de la categoría no puede estar vacío ni ser cero.");


            return (true, string.Empty); 
        }

        
    }
}