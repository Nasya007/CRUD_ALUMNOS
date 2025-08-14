using DbEscuela.Models;
using DbEscuela.Services;
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
            private readonly StoredProcedureService _spService;

            public AlumnosController()
            {
                _spService = new StoredProcedureService();
            }
            
            [HttpGet]
            public ActionResult Index()
            {
                // Usamos ExecuteReader para obtener una lista de alumnos
                var alumnos = _spService.ExecuteReader(
                    "sp_ObtenerAlumnos",
                    null,                
                    reader => new Alumno // Función que convierte cada fila del DataReader en un objeto Alumno
                {
                        ID_Alumno = Convert.ToInt32(reader["ID_Alumno"]),
                        NoControl = reader["NoControl"].ToString(),
                        Nombres = reader["Nombres"].ToString(),
                        ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                        ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                        Activo = Convert.ToBoolean(reader["Activo"]),
                        FechaDeNacimiento = Convert.ToDateTime(reader["FechaDeNacimiento"]),
                        Carrera = reader["Carrera"].ToString()
                    }
                );

                return View(alumnos);
            }

            public ActionResult Create()
            {
                    var carreras = _spService.ExecuteReader(
                        "sp_ObtenerCarreras",
                        null,
                        reader => new Carreras
                        {
                            ID_CARRERA = Convert.ToInt32(reader["ID_CARRERA"]),
                            Carrera = reader["Carrera"].ToString()
                        }
                    );

                    ViewBag.ID_CARRERA = new SelectList(carreras, "ID_CARRERA", "Carrera");

                return View();
            }

            [HttpPost]
            public ActionResult Create(Alumno alumno)
            {
                if (ModelState.IsValid)
                {
                    var parameters = new[]
                    {
                        new SqlParameter("@NoControl", alumno.NoControl),
                        new SqlParameter("@Nombres", alumno.Nombres),
                        new SqlParameter("@ApellidoPaterno", alumno.ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", alumno.ApellidoMaterno),
                        new SqlParameter("@Activo", alumno.Activo),
                        new SqlParameter("@FechaDeNacimiento", alumno.FechaDeNacimiento),
                        new SqlParameter("@ID_CARRERA", alumno.ID_CARRERA)
                    };

                    _spService.ExecuteNonQuery("sp_AgregarAlumno", parameters);

                    return RedirectToAction("Index");
                }

                return View(alumno);
            }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var parameters = new[]
            {
            new SqlParameter("@ID_Alumno", id)
            };

            var alumno = _spService.ExecuteReader(
                "sp_ObtenerAlumnoPorId",
                parameters,
                reader => new Alumno
                {
                    ID_Alumno = Convert.ToInt32(reader["ID_Alumno"]),
                    NoControl = reader["NoControl"].ToString(),
                    Nombres = reader["Nombres"].ToString(),
                    ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                    ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    FechaDeNacimiento = Convert.ToDateTime(reader["FechaDeNacimiento"]),
                    ID_CARRERA = Convert.ToInt32(reader["ID_CARRERA"]),
                    Carrera = reader["Carrera"].ToString()
                }
            ).FirstOrDefault();

            var carreras = _spService.ExecuteReader(
                        "sp_ObtenerCarreras",
                        null,
                        reader => new Carreras
                        {
                            ID_CARRERA = Convert.ToInt32(reader["ID_CARRERA"]),
                            Carrera = reader["Carrera"].ToString()
                        }
                    );

            ViewBag.ID_CARRERA = new SelectList(carreras, "ID_CARRERA", "Carrera", selectedValue: alumno.ID_CARRERA);

            return View(alumno);
        }

        [HttpPost]
        public ActionResult Edit(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]
                {
                new SqlParameter("@ID_Alumno", alumno.ID_Alumno),
                new SqlParameter("@NoControl", alumno.NoControl),
                new SqlParameter("@Nombres", alumno.Nombres),
                new SqlParameter("@ApellidoPaterno", alumno.ApellidoPaterno),
                new SqlParameter("@ApellidoMaterno", alumno.ApellidoMaterno),
                new SqlParameter("@Activo", alumno.Activo),
                new SqlParameter("@FechaDeNacimiento", alumno.FechaDeNacimiento),
                new SqlParameter("@ID_CARRERA", alumno.ID_CARRERA)
            };

                _spService.ExecuteNonQuery("sp_EditarAlumnos", parameters);

                return RedirectToAction("Index");
            }

            return View(alumno);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var parameters = new[]
            {
            new SqlParameter("@ID_Alumno", id)
            };

            var alumno = _spService.ExecuteReader(
                "sp_ObtenerAlumnoPorId",
                parameters,
                reader => new Alumno
                {
                    ID_Alumno = Convert.ToInt32(reader["ID_Alumno"]),
                    NoControl = reader["NoControl"].ToString(),
                    Nombres = reader["Nombres"].ToString(),
                    ApellidoPaterno = reader["ApellidoPaterno"].ToString(),
                    ApellidoMaterno = reader["ApellidoMaterno"].ToString(),
                    Activo = Convert.ToBoolean(reader["Activo"]),
                    FechaDeNacimiento = Convert.ToDateTime(reader["FechaDeNacimiento"]),
                    ID_CARRERA = Convert.ToInt32(reader["ID_CARRERA"]),
                    Carrera = reader["Carrera"].ToString()
                }
            );

            return View(alumno.First());
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var parameters = new[]
            {
                new SqlParameter("@ID_Alumno", id)
            };

            _spService.ExecuteNonQuery("sp_EliminarAlumno", parameters);

            return RedirectToAction("Index");
        }


    }
}
