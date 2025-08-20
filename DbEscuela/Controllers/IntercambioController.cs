using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DbEscuela.Controllers
{
    public class IntercambioController : Controller
    {
        // GET: Intercambio
        public ActionResult Index()
        {
            return View();
        }

        // GET: Intercambio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Intercambio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Intercambio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Intercambio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Intercambio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Intercambio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Intercambio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
