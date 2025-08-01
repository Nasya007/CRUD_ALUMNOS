using DbEscuela.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbEscuela.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Tb_Alumno.ToList());
            }
        }

        // GET: Alumno/Details/5
        public ActionResult Details(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Tb_Alumno.Where(x => x.ID_Alumno == id).FirstOrDefault());
            }

        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            return View();

        }

        // POST: Alumno/Create
        [HttpPost]
        public ActionResult Create(Tb_Alumno tb_Alumno)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Tb_Alumno.Add(tb_Alumno);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumno/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Tb_Alumno.Where(x => x.ID_Alumno == id).FirstOrDefault());
            }
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tb_Alumno tb_Alumno)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    context.Entry(tb_Alumno).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Alumno/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModels context = new DbModels())
            {
                return View(context.Tb_Alumno.Where(x => x.ID_Alumno == id).FirstOrDefault());
            }
        }

        // POST: Alumno/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModels context = new DbModels())
                {
                    Tb_Alumno tb_Alumno = context.Tb_Alumno.Where(x => x.ID_Alumno == id).FirstOrDefault();
                    context.Tb_Alumno.Remove(tb_Alumno);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
