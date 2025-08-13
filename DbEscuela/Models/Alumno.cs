using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DbEscuela.Models
{
    public class Alumno
    {
        public int ID_Alumno { get; set; }
        public string NoControl { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public string Carrera { get; set; }
    }
}