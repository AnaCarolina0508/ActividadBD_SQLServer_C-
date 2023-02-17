using System;
using System.Data.SqlClient;

namespace PracticaEmpresaBD
{
    internal class Program
    {
        static string cadenaDeConexion = string.Empty;
        static SqlConnection conexion = null; 
        static SqlCommand mySqlCommand = null; 
        static SqlDataReader mySqlDataReader = null;    



        static void Main(string[] args)
        {
            Console.WriteLine("Trabajando con base de datos en C#");
            ConectarSqlServer();
            MostrarDatosEmpleado();
            //InsertarNuevoEmpleado("David", "Espinosa", 37, 45566.98M);
            //ActaulizarEmpleado("Jessica", 1500);
            EliminarEmpleado();
            CerrarConexion();
        }

        private static void ConectarSqlServer()
        {
            try
            {
                cadenaDeConexion = "Server=.\\SQLEXPRESS;Database=Empresa;Trusted_Connection=True";
                conexion = new SqlConnection(cadenaDeConexion);
                conexion.Open();
                Console.WriteLine("Conexión Exitosa a SLQ Server");
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Problemas al tratar de conectar la BD. Detalles:  ");
                Console.WriteLine(ex.Message);
            }
        }

        private static void MostrarDatosEmpleado()
        {
            try
            {
                string sqlQuery = "SELECT * FROM Empleado";
                mySqlCommand= new SqlCommand(sqlQuery,conexion);
                mySqlDataReader= mySqlCommand.ExecuteReader();
                Console.WriteLine("Empleado Id\t\t Nombres\t\t Apellidos\t\t Edad\t\t Salario");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                while(mySqlDataReader.Read())
                {
                    Console.WriteLine($"{mySqlDataReader["Id"]}\t\t {mySqlDataReader["Nombres"]}\t\t {mySqlDataReader["Apellidos"]}\t\t {mySqlDataReader["Edad"]}\t\t {mySqlDataReader["Salario"]}");
                }
                mySqlDataReader.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Problemas al tratar de conectar la BD. Detalles:  ");
                Console.WriteLine(ex.Message);
            }
        }

        private static void InsertarNuevoEmpleado(string nombres, string apellidos, byte edad, decimal salario)
        {
            try
            {
                string sqlQuery = "INSERT INTO Empleado (Nombres, Apellidos, Edad, Salario) VALUES (@nombres, @apellidos, @edad, @salario)";
                mySqlCommand= new SqlCommand(sqlQuery,conexion);
                mySqlCommand.Parameters.AddWithValue("nombres", nombres);
                mySqlCommand.Parameters.AddWithValue("apellidos", apellidos);
                mySqlCommand.Parameters.AddWithValue("edad", edad);
                mySqlCommand.Parameters.AddWithValue("salario", salario);
                int resultado= mySqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{resultado} Registro insertado correctamente");
                Console.WriteLine("Datos actaules de la tabla");
                MostrarDatosEmpleado();
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Problemas al tratar de conectar la BD. Detalles:  ");
                Console.WriteLine(ex.Message);
            }
        }
       
        private static void ActaulizarEmpleado(string nombres, decimal aumento)
        {
            try
            {
                Console.WriteLine($"Actualizar el salario del empleado {nombres}");
                string sqlQuery = "UPDATE Empleado SET Salario = @salario WHERE Nombres = @nombres";
                mySqlCommand= new SqlCommand(sqlQuery,conexion);
                mySqlCommand.Parameters.AddWithValue("salario", aumento);
                mySqlCommand.Parameters.AddWithValue("nombres", nombres);
                int resultado = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{resultado} Registro se actualizó en la base de datos");
                Console.WriteLine($"{nombres} AHora tiene un aumento de {aumento}");
                Console.WriteLine("Datos actuales de la base de datos");
                MostrarDatosEmpleado();
            }
            catch (SqlException ex)
            {

                Console.WriteLine("Problemas al tratar de conectar la BD. Detalles:  ");
                Console.WriteLine(ex.Message);
            }
        }
        
        private static void EliminarEmpleado()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre del empleado a eliminar ");
                string empleadoEliminar = Console.ReadLine();
                string sqlQuery = "DELETE FROM Empleado WHERE Nombres = @nombres";
                mySqlCommand= new SqlCommand(sqlQuery,conexion);
                mySqlCommand.Parameters.AddWithValue("nombres", empleadoEliminar);
                int resultado = mySqlCommand.ExecuteNonQuery();
                Console.WriteLine($"{resultado} Registro eliminado correctamente");
                Console.WriteLine("Datos actuales de la base de datos");
                MostrarDatosEmpleado();


            }
            catch (SqlException ex)
            {

                Console.WriteLine("Problemas al tratar de conectar la BD. Detalles:  ");
                Console.WriteLine(ex.Message);
            }
        }
        private static void CerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Problemas al tratar de conectar la BD. Detalles:  ");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
