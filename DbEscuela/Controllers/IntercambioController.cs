using DbEscuela.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DbEscuela.Controllers
{
    public class IntercambioController : Controller
    {
        //Almacena todos los participantes
        private static List<Participante> participantes = new List<Participante>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarParticipantes(List<Participante> nuevos)
        {
            //actualizamos lista participantes
            participantes = nuevos;
            return Json(new { success = true, message = $"Se guardaron {participantes.Count} participantes." });
        }

        [HttpPost]
        public JsonResult EnviarCorreos()
        {
            //Debe haber al menos 2 participantes para realizar el intercambio
            if (participantes == null || participantes.Count < 2)
                return Json(new { success = false, message = "Debe haber al menos 2 participantes." });

            var rnd = new Random();
            bool valido = false;
            List<Participante> asignados = null;

            // Intentar hasta obtener un sorteo válido
            while (!valido)
            {
                var mezclados = participantes.OrderBy(x => rnd.Next()).ToList();
                valido = true;

                // Verificar que nadie se asigne a sí mismo
                for (int i = 0; i < participantes.Count; i++)
                {
                    if (participantes[i].Nombre == mezclados[i].Nombre)
                    {
                        valido = false;
                        break;
                    }
                }

                if (valido)
                {
                    asignados = mezclados;
                }
            }

            // Asignar los resultados
            for (int i = 0; i < participantes.Count; i++)
            {
                participantes[i].AmigoSecreto = asignados[i].Nombre;

                // Enviar correo
                EnviarCorreo(
                    participantes[i].Email,
                    participantes[i].Nombre,
                    asignados[i].Nombre,
                    asignados[i].Regalo
                );
            }

            return Json(new { success = true, message = "Sorteo completado y correos enviados." });
        }

        private void EnviarCorreo(string destinatario, string nombre, string amigo, string regalo)
        {
            // Definimos la dirección de remitente y destinatario.
            string email = ConfigurationManager.AppSettings["email.user"];
            string pwd = ConfigurationManager.AppSettings["email.pwd"];

            var fromAddress = new MailAddress(email, "Intercambio Navideño 🎁");
            var toAddress = new MailAddress(destinatario);

            string subject = "Tu Amigo Secreto 🎉";
            string body = $@"
                <h2>Hola {nombre}!</h2>
                <p>Te tocó regalar a: <strong>{amigo}</strong></p>
                <p>Recuerda que su regalo deseado es: <em>{regalo}</em></p>
                <br/>
                <p>¡No se lo cuentes a nadie 😉!</p>
            ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, pwd)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }

    }
}
