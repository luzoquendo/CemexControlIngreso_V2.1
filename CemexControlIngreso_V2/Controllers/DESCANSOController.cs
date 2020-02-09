using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CemexControlIngreso_V2.Models;
using Newtonsoft.Json;

namespace CemexControlIngreso_V2.Controllers
{
    public class DESCANSOController : Controller
    {
        // GET: DESCANSO
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();
        [Authorize]
        // GET: VIAJE
        public ActionResult Index()
        {
            var dESCANSO = db.Descanso.Include(v => v.CONDUCTOR).Include(v => v.VIAJE);
            return View(dESCANSO.ToList());
        }

        // GET: VIAJE/Details/5
        public ActionResult Details(int? IdConductor)
        {
            if (IdConductor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descanso dESCANSO = db.Descanso.Find(IdConductor);
            if (dESCANSO == null)
            {
                return HttpNotFound();
            }
            return View(dESCANSO);
        }

        // GET: VIAJE/Create
        public ActionResult Create()
        {
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR.
                Where(o => o.Estado == true), "IdConductor", "Cedula");
            ViewBag.IdViaje = new SelectList(db.VIAJE.
                Where(o => o.Estado == true), "IdViaje", "NumeroViaje");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConductor,IdViaje")] Descanso descanso)
        {
            if (ModelState.IsValid)
            {
                db.Descanso.Add(descanso);
                descanso.FechaDescanso = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Conductor1", descanso.IdConductor);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", descanso.IdViaje);
            return View(descanso);
        }

        public class Inform
        {
            public string Nombre { get; set; }
            public string Celular1 { get; set; }
            public string Celular2 { get; set; }
        }

        // GET: VIAJE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Descanso descanso = db.Descanso.Find(id);
            var inform = JsonConvert.SerializeObject(db.TraerConductorId(descanso.IdConductor));
            Inform[] inform1 = js.Deserialize<Inform[]>(inform);

            if (descanso == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "idConductor", "Nombre", descanso.IdConductor);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", descanso.IdViaje);
            return View(descanso);
        }

        // POST: VIAJE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViaje,IdConductor,FechaDescanso")] Descanso descanso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descanso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Nombre", descanso.IdConductor);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", descanso.IdViaje);
            ViewBag.Fecha = descanso.FechaDescanso;
            return View(descanso);
        }

        // GET: VIAJE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descanso descanso = db.Descanso.Find(id);
            if (descanso == null)
            {
                return HttpNotFound();
            }
            return View(descanso);
        }

        // POST: VIAJE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Descanso descanso = db.Descanso.Find(id);
            db.Descanso.Remove(descanso);
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