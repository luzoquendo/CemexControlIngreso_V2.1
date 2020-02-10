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
    public class CHECKLISTController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: CHECKLIST
        public ActionResult Index()
        {
            var cHECKLIST = db.CHECKLIST.Include(c => c.VIAJE).Include(c => c.VIAJECTRL);
            return View(cHECKLIST.ToList());
        }

        // GET: CHECKLIST/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHECKLIST cHECKLIST = db.CHECKLIST.Find(id);
            if (cHECKLIST == null)
            {
                return HttpNotFound();
            }
            return View(cHECKLIST);
        }

        // GET: CHECKLIST/Create
        public ActionResult Create()
        {
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Alcohotest");
            ViewBag.IdViaje = new SelectList(db.VIAJECTRL, "IdViaje", "Alcohotest");
            return View();
        }

        // POST: CHECKLIST/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Llantas,Motor,Aceite,IdViaje,Fecha")] CHECKLIST cHECKLIST)
        {
            if (ModelState.IsValid)
            {
                db.CHECKLIST.Add(cHECKLIST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Alcohotest", cHECKLIST.IdViaje);
            ViewBag.IdViaje = new SelectList(db.VIAJECTRL, "IdViaje", "Alcohotest", cHECKLIST.IdViaje);
            return View(cHECKLIST);
        }

        // GET: CHECKLIST/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHECKLIST cHECKLIST = db.CHECKLIST.Find(id);
            if (cHECKLIST == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Alcohotest", cHECKLIST.IdViaje);
            ViewBag.IdViaje = new SelectList(db.VIAJECTRL, "IdViaje", "Alcohotest", cHECKLIST.IdViaje);
            return View(cHECKLIST);
        }

        // POST: CHECKLIST/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Llantas,Motor,Aceite,IdViaje,Fecha")] CHECKLIST cHECKLIST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHECKLIST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Alcohotest", cHECKLIST.IdViaje);
            ViewBag.IdViaje = new SelectList(db.VIAJECTRL, "IdViaje", "Alcohotest", cHECKLIST.IdViaje);
            return View(cHECKLIST);
        }

        // GET: CHECKLIST/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHECKLIST cHECKLIST = db.CHECKLIST.Find(id);
            if (cHECKLIST == null)
            {
                return HttpNotFound();
            }
            return View(cHECKLIST);
        }

        // POST: CHECKLIST/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHECKLIST cHECKLIST = db.CHECKLIST.Find(id);
            db.CHECKLIST.Remove(cHECKLIST);
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
