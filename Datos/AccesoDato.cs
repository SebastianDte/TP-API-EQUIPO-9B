using System;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDato : IDisposable
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector => lector;

        public AccesoDato()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS;database=PROMOS_DB;integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
            comando.Parameters.Clear();
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor ?? DBNull.Value);
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            conexion.Open();
            lector = comando.ExecuteReader();
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }
        }

        public object ejecutarScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return comando.ExecuteScalar();
            }
            finally
            {
                conexion.Close();
            }
        }

        public void cerrarConexion()
        {
            if (lector != null && !lector.IsClosed)
                lector.Close();
            if (conexion.State != System.Data.ConnectionState.Closed)
                conexion.Close();
        }

        public void Dispose()
        {
            cerrarConexion();
            comando.Dispose();
            conexion.Dispose();
        }
    }
}
