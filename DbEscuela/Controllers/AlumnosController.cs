using DbEscuela.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbEscuela.Controllers
{
    public class AlumnosController : Controller
    {
        public ActionResult Index()
        {
            var alumnos = new List<Alumno>();

            // Obtener cadena de conexión desde Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_ObtenerAlumnos", conn);
                command.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    alumnos.Add(new Alumno
                    {
                        ID_Alumno = Convert.ToInt32(reader["ID_Alumno"]),
                        NoControl = reader["NoControl"].ToString(),
                        Nombres = reader["Nombres"].ToString(),
                        ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                        ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                        Activo = Convert.ToBoolean(reader["Activo"]),
                        FechaDeNacimiento = Convert.ToDateTime(reader["FechaDeNacimiento"]),
                        Carrera = reader["Carrera"].ToString()
                    });
                }
            }

            return View(alumnos);
        }
    }
}