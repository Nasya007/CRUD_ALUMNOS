using DbEscuela.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbEscuela.Controllers
{
    public class CarreraController : Controller
    {
        // GET: Carrera
        public ActionResult Index()
        {
            using (DbModel1 context = new DbModel1())
            {
                return View(context.Tb_Carrera.ToList());
            }
        }

        // GET: Carrera/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel1 context = new DbModel1())
            {
                var carrera = context.Tb_Carrera.FirstOrDefault(x=>x.ID_Carrera == id);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Details", carrera);
                }

                return View(carrera);
            }
        }

        // GET: Carrera/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrera/Create
        [HttpPost]
        public ActionResult Create(Tb_Carrera tb_Carrera)
        {
            try
            {
                using (DbModel1 context = new DbModel1())
                {
                    context.Tb_Carrera.Add(tb_Carrera);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrera/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel1 context = new DbModel1())
            {
                var carrera = context.Tb_Carrera.FirstOrDefault(x => x.ID_Carrera == id);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Edit", carrera);
                }

                return View(carrera);
            }
        }

        // POST: Carrera/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tb_Carrera tb_Carrera)
        {
            try
            {
                using (DbModel1 context = new DbModel1())
                {
                    context.Entry(tb_Carrera).State = EntityState.Modified;
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrera/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModel1 context = new DbModel1())
            {
                var carrera = context.Tb_Carrera.FirstOrDefault(x => x.ID_Carrera == id);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("Delete", carrera);
                }

                return View(carrera);
            }
        }

        // POST: Carrera/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel1 context = new DbModel1())
                {
                    Tb_Carrera tb_Carrera = context.Tb_Carrera.Where(x => x.ID_Carrera == id).FirstOrDefault();
                    context.Tb_Carrera.Remove(tb_Carrera);
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
