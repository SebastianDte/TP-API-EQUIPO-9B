using Dominio;
using System;

namespace Negocio
{
    public class ClienteNegocio
    {
        public void agregar(Cliente nuevo)
        {
            using (AccesoDato datos = new AccesoDato())
            {
                try
                {
                    datos.setearConsulta(@"
                        INSERT INTO Clientes(Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) 
                        OUTPUT INSERTED.Id 
                        VALUES(@Documento, @Nombre, @Apellido, @Email, @Direccion, @Ciudad, @CP);
                    ");
                    datos.setearParametro("@Documento", nuevo.Documento);
                    datos.setearParametro("@Nombre", nuevo.Nombre);
                    datos.setearParametro("@Apellido", nuevo.Apellido);
                    datos.setearParametro("@Email", nuevo.Email);
                    datos.setearParametro("@Direccion", nuevo.Direccion);
                    datos.setearParametro("@Ciudad", nuevo.Ciudad);
                    datos.setearParametro("@CP", nuevo.CP);

                    datos.ejecutarLectura();

                    try
                    {
                        if (datos.Lector.Read())
                            nuevo.Id = (int)datos.Lector["Id"];
                    }
                    finally
                    {
                        if (datos.Lector != null && !datos.Lector.IsClosed)
                            datos.Lector.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Cliente obtenerPorDni(string dni)
        {
            using (AccesoDato datos = new AccesoDato())
            {
                Cliente cliente = null;
                try
                {
                    datos.setearConsulta("SELECT Id, Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP FROM Clientes WHERE Documento = @Documento");
                    datos.setearParametro("@Documento", dni);
                    datos.ejecutarLectura();

                    try
                    {
                        if (datos.Lector.Read())
                        {
                            cliente = new Cliente()
                            {
                                Id = (int)datos.Lector["Id"],
                                Documento = (string)datos.Lector["Documento"],
                                Nombre = (string)datos.Lector["Nombre"],
                                Apellido = (string)datos.Lector["Apellido"],
                                Email = (string)datos.Lector["Email"],
                                Direccion = (string)datos.Lector["Direccion"],
                                Ciudad = (string)datos.Lector["Ciudad"],
                                CP = (int)datos.Lector["CP"]
                            };
                        }
                    }
                    finally
                    {
                        if (datos.Lector != null && !datos.Lector.IsClosed)
                            datos.Lector.Close();
                    }

                    return cliente;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
