using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Datos
{
    internal class Conexion
    {
        SqlConnection conn;

        public Conexion()
        {
            conn = new SqlConnection("Server = DESKTOP-4M4TPA7\\SQLEXPRESS; Database = empleado_db; integrated security = true");
        }

        public SqlConnection conectar()
        {
            try {
                conn.Open();
                return conn;
            } catch (Exception e) {
                throw e.GetBaseException();
            }
        }

        public bool desconectar()
        {
            try {
                conn.Close();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

    }
}
