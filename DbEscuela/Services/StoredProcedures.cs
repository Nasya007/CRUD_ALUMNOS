using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DbEscuela.Services
{
    public class StoredProcedureService
    {
        private readonly string _connectionString; // Almacena la cadena de conexión

        // Constructor
        public StoredProcedureService()
        {
            // Obtener cadena de conexión desde Web.config
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // T: Tipo genérico que representa el modelo de datos a devolver
        // parameters:null
        // mapFunction: Función que convierte un SqlDataReader a un objeto T
        public List<T> ExecuteReader<T>(string storedProcedureName, SqlParameter[] parameters, Func<SqlDataReader, T> mapFunction)
        {
            var result = new List<T>(); // Lista para almacenar los resultados

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agrega parámetros si existen
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Usa la función mapFunction para convertir la fila a objeto T
                            result.Add(mapFunction(reader));
                        }
                    }
                }
            }

            return result; // Devuelve la lista de resultados
        }

        // Método para ejecutar stored procedures que no devuelven datos (INSERT, UPDATE, DELETE)
        // parameters: Parámetros para el SP (puede ser null)
        // Retorna: Número de filas afectadas
        public int ExecuteNonQuery(string storedProcedureName, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Agrega parámetros si existen
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    if (parameters != null && parameters.Length > 0)
                    {
                        Debug.WriteLine("Parámetros:");
                        foreach (var param in parameters)
                        {
                            string value = param.Value != null ? param.Value.ToString() : "NULL";
                            Debug.WriteLine($"  {param.ParameterName}: {value} (Tipo: {param.SqlDbType}, Tamaño: {param.Size})");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("No se recibieron parámetros");
                    }

                    conn.Open();

                    // Ejecuta el comando y devuelve el número de filas afectadas
                    //int result = command.ExecuteNonQuery();
                    //Debug.WriteLine($"SP {storedProcedureName} ejecutado. Filas afectadas: {result}");
                    //return result;

                    try
                    {
                        int result = command.ExecuteNonQuery();
                        Debug.WriteLine($"SP {storedProcedureName} ejecutado. Filas afectadas: {result}");
                        return result;
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine($"Error ejecutando {storedProcedureName}: {ex.Message}");
                        throw; // Re-lanzar la excepción para manejarla en otro lugar si es necesario
                    }

                }
            }
        }

    }
}