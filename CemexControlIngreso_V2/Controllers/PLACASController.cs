using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CemexControlIngreso_V2.Models;

namespace CemexControlIngreso_V2.Controllers
{
    public class PLACASController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: PLACAS
        public ActionResult Index()
        {
            return View(db.PLACAS.ToList());
        }

        // GET: PLACAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACAS pLACAS = db.PLACAS.Find(id);
            if (pLACAS == null)
            {
                return HttpNotFound();
            }
            return View(pLACAS);
        }

        // GET: PLACAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PLACAS/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPlaca,Placa,Estado")] PLACAS pLACAS)
        {
            if (ModelState.IsValid)
            {
                db.PLACAS.Add(pLACAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pLACAS);
        }

        // GET: PLACAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACAS pLACAS = db.PLACAS.Find(id);
            if (pLACAS == null)
            {
                return HttpNotFound();
            }
            return View(pLACAS);
        }

        // POST: PLACAS/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPlaca,Placa,Estado")] PLACAS pLACAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLACAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pLACAS);
        }

        // GET: PLACAS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLACAS pLACAS = db.PLACAS.Find(id);
            if (pLACAS == null)
            {
                return HttpNotFound();
            }
            return View(pLACAS);
        }

        // POST: PLACAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PLACAS pLACAS = db.PLACAS.Find(id);
            //db.PLACAS.Remove(pLACAS);
            pLACAS.Estado = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
