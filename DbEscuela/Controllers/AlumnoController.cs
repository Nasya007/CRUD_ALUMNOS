using DbEscuela.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
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
            using (DbModel1 context = new DbModel1())
            {
                var alumnos = context.Tb_Alumno.Include(a =>a.Tb_Carrera).ToList();
                return View(alumnos);
            }
        }

        // GET: Alumno/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel1 context = new DbModel1())
            {
                var alumno = context.Tb_Alumno
                            .Include(a => a.Tb_Carrera)
                            .FirstOrDefault(x => x.ID_Alumno == id);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Details", alumno);
                }

                return View(alumno);
            }

        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            using (DbModel1 context = new DbModel1())
            {
                /*Viewbag pasar datos del controlador a la vista*/
                ViewBag.ID_CARRERA = new SelectList(context.Tb_Carrera.ToList(), "ID_Carrera", "Carrera");
                return View();
            }

        }

        // POST: Alumno/Create
        [HttpPost]
        public ActionResult Create(Tb_Alumno tb_Alumno)
        {
            try
            {
                using (DbModel1 context = new DbModel1())
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
            using (DbModel1 context = new DbModel1())
            {
                var alumno = context.Tb_Alumno.Include(a => a.Tb_Carrera).FirstOrDefault(x => x.ID_Alumno == id);
                ViewBag.ID_CARRERA = new SelectList(context.Tb_Carrera.ToList(), "ID_Carrera", "Carrera", alumno.ID_CARRERA);
                
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Edit", alumno);
                }
                return View(alumno);

            }
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tb_Alumno tb_Alumno)
        {
            try
            {
                using (DbModel1 context = new DbModel1())
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
            using (DbModel1 context = new DbModel1())
            {
                var alumno = context.Tb_Alumno
                            .Include(a => a.Tb_Carrera)
                            .FirstOrDefault(x => x.ID_Alumno == id);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Delete", alumno);
                }

                return View(alumno);
            }
        }

        // POST: Alumno/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel1 context = new DbModel1())
                {
                    Tb_Alumno tb_Alumno = context.Tb_Alumno.Include(a => a.Tb_Carrera).FirstOrDefault(x => x.ID_Alumno == id);
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