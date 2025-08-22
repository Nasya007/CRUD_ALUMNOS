using DbEscuela.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbEscuela.Controllers
{
    public class IntercambioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarParticipantes(List<Participante> participantes)
        {
            return Json(new
            {
                success = true,
                cantidad = participantes.Count,
                data = participantes
            });
        }
    }
}
