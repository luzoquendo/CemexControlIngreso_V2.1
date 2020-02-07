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
    public class TRAILERController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: TRAILER
        public ActionResult Index()
        {
            return View(db.TRAILER.ToList());
        }

        // GET: TRAILER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAILER tRAILER = db.TRAILER.Find(id);
            if (tRAILER == null)
            {
                return HttpNotFound();
            }
            return View(tRAILER);
        }

        // GET: TRAILER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TRAILER/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTrailer,PlacaTrailer,Estado")] TRAILER tRAILER)
        {
            if (ModelState.IsValid)
            {
                tRAILER.PlacaTrailer = tRAILER.PlacaTrailer.ToUpper();
                db.TRAILER.Add(tRAILER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRAILER);
        }

        // GET: TRAILER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAILER tRAILER = db.TRAILER.Find(id);
            if (tRAILER == null)
            {
                return HttpNotFound();
            }
            return View(tRAILER);
        }

        // POST: TRAILER/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTrailer,PlacaTrailer,Estado")] TRAILER tRAILER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRAILER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRAILER);
        }

        // GET: TRAILER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAILER tRAILER = db.TRAILER.Find(id);
            if (tRAILER == null)
            {
                return HttpNotFound();
            }
            return View(tRAILER);
        }

        // POST: TRAILER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRAILER tRAILER = db.TRAILER.Find(id);
            //db.TRAILER.Remove(tRAILER);
            tRAILER.Estado = false;
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
