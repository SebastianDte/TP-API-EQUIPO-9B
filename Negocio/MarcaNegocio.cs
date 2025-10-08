using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()

        {
            List<Marca> lista = new List<Marca>();
            AccesoDato datos = new AccesoDato();

            try
            {
                datos.setearConsulta("Select Id, Descripcion From MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.id = (int)datos.Lector["Id"];
                    aux.descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                return lista;
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
    
        public void agregar( Marca nueva)
        {
            AccesoDato datos = new AccesoDato();

            try
            {
                datos.setearConsulta("INSERT INTO MARCAS(Descripcion)VALUES(@Descripcion);");
                datos.setearParametro("@Descripcion", nueva.descripcion);
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
    
        public void modificar(Marca marcaMod)
        {
            AccesoDato datos = new AccesoDato();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @descrip WHERE Id = @id");
                datos.setearParametro("@id", marcaMod.id);
                datos.setearParametro("@descrip", marcaMod.descripcion);

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
    
        public void eliminar(int id)
        {
            AccesoDato datos = new AccesoDato();

            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex )
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
