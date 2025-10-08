using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VoucherNegocio
    {
        private AccesoDato acceso;

        public VoucherNegocio()
        {
            acceso = new AccesoDato();
        }

        // Método para validar si un voucher existe y si se puede canjear
        public Vouncher ValidarVoucher(string codigo)
        {
            Vouncher voucher = null;

            try
            {
                acceso.setearConsulta("SELECT v.CodigoVoucher, v.FechaCanje, v.IdCliente, c.Documento, c.Nombre, c.Apellido, c.Email, c.Direccion, c.Ciudad, c.CP " +
                                      "FROM Vouchers v " +
                                      "LEFT JOIN Clientes c ON v.IdCliente = c.Id " +
                                      "WHERE v.CodigoVoucher = @codigo");
                acceso.setearParametro("@codigo", codigo);
                acceso.ejecutarLectura();

                if (acceso.Lector.Read())
                {
                    voucher = new Vouncher
                    {
                        CodigoVoucher = acceso.Lector["CodigoVoucher"].ToString(),
                        FechaCanje = acceso.Lector["FechaCanje"] != DBNull.Value ? (DateTime?)acceso.Lector["FechaCanje"] : null,
                        Cliente = acceso.Lector["IdCliente"] != DBNull.Value ? new Cliente
                        {
                            Id = Convert.ToInt32(acceso.Lector["IdCliente"]),
                            Documento = acceso.Lector["Documento"].ToString(),
                            Nombre = acceso.Lector["Nombre"].ToString(),
                            Apellido = acceso.Lector["Apellido"].ToString(),
                            Email = acceso.Lector["Email"].ToString(),
                            Direccion = acceso.Lector["Direccion"].ToString(),
                            Ciudad = acceso.Lector["Ciudad"].ToString(),
                            CP = Convert.ToInt32(acceso.Lector["CP"])
                        } : null
                    };
                }

                return voucher;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }

        // Método para canjear un voucher
        public void CanjearVoucher(string codigoVoucher, int idCliente, int idArticulo)
        {
            AccesoDato datos = new AccesoDato();
            try
            {
                // Intento actualizar el voucher
                datos.setearConsulta(@"
            UPDATE Vouchers 
            SET IdCliente = @IdCliente, IdArticulo = @IdArticulo, FechaCanje = GETDATE()
            WHERE CodigoVoucher = @CodigoVoucher AND FechaCanje IS NULL
        ");
                datos.setearParametro("@IdCliente", idCliente);
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.setearParametro("@CodigoVoucher", codigoVoucher);
                datos.ejecutarAccion(); 

                // Verifico si se aplicó
                datos.setearConsulta("SELECT COUNT(1) FROM Vouchers WHERE CodigoVoucher = @CodigoVoucher AND FechaCanje IS NOT NULL");
                datos.setearParametro("@CodigoVoucher", codigoVoucher);
                object resultado = datos.ejecutarScalar();
                int filas = Convert.ToInt32(resultado);

                if (filas == 0)
                    throw new Exception("El voucher ya fue usado o no existe.");
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


