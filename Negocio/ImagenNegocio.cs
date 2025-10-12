using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listarImagenes(int idArticulo)
        {
            AccesoDato datos = new AccesoDato();
            List<Imagen> imagenes = new List<Imagen>();
            try
            {
                datos.setearConsulta("SELECT Id, IdArticulo, imagenUrl FROM IMAGENES WHERE IdArticulo = " + idArticulo);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Imagen img = new Imagen();
                    img.id = (int)datos.Lector["Id"];
                    img.idArticulo = (int)datos.Lector["IdArticulo"];
                    img.imageUrl = (string)datos.Lector["imagenUrl"];
                    imagenes.Add(img);
                }
                return imagenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void AgregarImagen(Imagen img)
        {
            AccesoDato datos = new AccesoDato();
            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES(IdArticulo, imagenUrl) VALUES(@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", img.idArticulo);
                datos.setearParametro("@ImagenUrl", img.imageUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void EliminarImagenes(int idArticulo)
        {
            AccesoDato datos = new AccesoDato();
            try
            {
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarImagen(int id)
        {
            AccesoDato datos = new AccesoDato();
            try
            {
                datos.setearConsulta("DELETE FROM IMAGENES WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarImagenes(List<Imagen> imagenes)
        {
            foreach (var img in imagenes)
            {
                AgregarImagen(img);
            }
        }

        public void ModificarImagen(Imagen img)
        {
            AccesoDato datos = new AccesoDato();
            try
            {
                datos.setearConsulta("UPDATE IMAGENES SET ImagenUrl=@ImagenUrl WHERE Id=@Id");
                datos.setearParametro("@ImagenUrl", img.imageUrl);
                datos.setearParametro("@Id", img.id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
