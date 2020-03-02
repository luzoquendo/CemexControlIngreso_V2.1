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
        private CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();

        // GET: TRAILER
        public ActionResult Index()
        {
            var tRAILER = db.TRAILER.Include(c => c.TIPOTRAILER);
            return View(tRAILER.ToList());
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
            ViewBag.IdTipoTrailer = new SelectList(db.TIPOTRAILER, "Id", "IdTipoTrailer");
            return View();
        }

        // POST: TRAILER/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTrailer,PlacaTrailer,Estado,IdTipoTrailer")] TRAILER tRAILER)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.TRAILER.Any(x => x.PlacaTrailer.ToUpper() == tRAILER.PlacaTrailer.ToUpper());
                if (!existeUsuario)
                {
                    tRAILER.PlacaTrailer = tRAILER.PlacaTrailer.ToUpper();
                    tRAILER.Estado = true;
                    db.TRAILER.Add(tRAILER);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este numero de trailer, " + tRAILER.PlacaTrailer + " por favor revise...";
                }
            }
            ViewBag.IdTipoTrailer = new SelectList(db.TIPOTRAILER, "Id", "TipoTrailer1", tRAILER.IdTipoTrailer);
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
            ViewBag.IdTipoTrailer = new SelectList(db.TIPOTRAILER, "Id", "TipoTrailer1", tRAILER.IdTipoTrailer);
            return View(tRAILER);
        }

        // POST: TRAILER/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTrailer,PlacaTrailer,Estado,IdTipoTrailer")] TRAILER tRAILER)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.TRAILER.Any(x => x.PlacaTrailer.ToUpper() == tRAILER.PlacaTrailer.ToUpper());

                    db.Entry(tRAILER).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
  
            }
            ViewBag.IdTipoTrailer = new SelectList(db.TIPOTRAILER, "Id", "TipoTrailer1", tRAILER.IdTipoTrailer);
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
