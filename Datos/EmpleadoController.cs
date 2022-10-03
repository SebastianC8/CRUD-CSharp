using Empleados.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empleados.Datos
{
    internal class EmpleadoController
    {

        public static bool guardar(Empleado empleado)
        {
            try {
                Conexion conexion = new Conexion();

                string sql = "INSERT INTO tb_empleados VALUES ('" + empleado.Documento + "', '" + empleado.Nombres + "', '" + empleado.Apellidos + "', " + empleado.Edad + ", '" + empleado.Direccion + "', '" + empleado.Fecha_nacimiento + "')";
                SqlCommand cmd = new SqlCommand(sql, conexion.conectar());
                int filasAfectadas = cmd.ExecuteNonQuery();

                conexion.desconectar();

                return filasAfectadas > 0;
            } catch (Exception) {
                return false;
            }
        }

        public static DataTable? listar()
        {
            try {
                Conexion conexion = new Conexion();

                string sql = "SELECT * FROM tb_empleados";
                SqlCommand cmd = new SqlCommand(sql, conexion.conectar());
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);

                conexion.desconectar();

                return dataTable;
            } catch (Exception) {
                return null;
            }
        }

        public static Empleado? getEmpleado(string documento)
        {
            try {
                Conexion conexion = new Conexion();

                string sql = "SELECT * FROM tb_empleados WHERE documento = '" + documento + "'";
                SqlCommand cmd = new SqlCommand(sql, conexion.conectar());
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (dataReader.Read()) {
                    
                    Empleado empleado = new Empleado();

                    empleado.Documento = dataReader["documento"].ToString();
                    empleado.Nombres = dataReader["nombres"].ToString();
                    empleado.Apellidos = dataReader["apellidos"].ToString();
                    empleado.Edad = Convert.ToInt32(dataReader["edad"]);
                    empleado.Direccion = dataReader["direccion"].ToString();
                    empleado.Fecha_nacimiento = dataReader["fecha_nacimiento"].ToString();

                    conexion.desconectar();
                    return empleado;
                } else {
                    conexion.desconectar();
                    return null;
                }

            } catch (Exception) {
                return null;
            }

        }

        public static bool actualizar(Empleado empleado)
        {
            try {
                Conexion conexion = new Conexion();

                string sql = "UPDATE tb_empleados SET nombres = '" + empleado.Nombres + "', apellidos = '" + empleado.Apellidos + "', edad = " + empleado.Edad + ", direccion = '" + empleado.Direccion + "', fecha_nacimiento = '" + empleado.Fecha_nacimiento + "' WHERE documento = " + empleado.Documento;
                SqlCommand cmd = new SqlCommand(sql, conexion.conectar());
                int filasAfectadas = cmd.ExecuteNonQuery();

                conexion.desconectar();

                return filasAfectadas > 0;
            }
            catch (Exception) {
                return false;
            }
        }

        public static bool eliminar(string documento)
        {
            try {
                Conexion conexion = new Conexion();

                string sql = "DELETE FROM tb_empleados WHERE documento = " + documento;
                SqlCommand cmd = new SqlCommand(sql, conexion.conectar());
                int filasAfectadas = cmd.ExecuteNonQuery();

                conexion.desconectar();

                return filasAfectadas > 0;
            }
            catch (Exception) {
                return false;
            }
        }

    }
}
