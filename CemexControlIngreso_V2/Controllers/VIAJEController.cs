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
using CemexControlIngreso_V2.Models;

namespace CemexControlIngreso_V2.Controllers
{
    public class VIAJEController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: VIAJE
        public ActionResult Index()
        {
            var vIAJE = db.VIAJE.Include(v => v.CONDUCTOR).Include(v => v.CORREDOR).Include(v => v.PRODUCTO).Include(v => v.PLACAS).Include(v => v.TRAILER);
            return View(vIAJE.ToList());
        }

        // GET: VIAJE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAJE vIAJE = db.VIAJE.Find(id);
            if (vIAJE == null)
            {
                return HttpNotFound();
            }
            return View(vIAJE);
        }

        // GET: VIAJE/Create
        public ActionResult Create()
        {
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR.
                Where(o => o.Estado == true),"IdConductor", "Cedula");

            ViewBag.IdInstructor = new SelectList(db.INSTRUCTOR.
                   Where(o => o.Estado == true), "IdInstructor", "Nombre");
            ViewBag.IdCorredor = new SelectList(db.CORREDOR.
                Where(o => o.Estado == true),"IdCorredor", "IdCorredor");
            ViewBag.IdProducto = new SelectList(db.PRODUCTO.
                Where(o => o.Estado == true),"IdProducto", "Nombre");
            ViewBag.IdTrailer = new SelectList(db.TRAILER.
                Where(o => o.Estado == true), "IdTrailer", "PlacaTrailer");
            ViewBag.IdPlaca = new SelectList(db.PLACAS.
                Where(o => o.Estado == true), "IdPlaca", "Placa");
            return View();
        }

        // POST: VIAJE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViaje,IdConductor,IdCorredor,IdProducto,Estado")] VIAJE vIAJE)
        {
            if (ModelState.IsValid)
            {
                db.VIAJE.Add(vIAJE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Conductor1",vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            return View(vIAJE);
        }

        // GET: VIAJE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAJE vIAJE = db.VIAJE.Find(id);
            if (vIAJE == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR,"IdConductor", "Nombre", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            return View(vIAJE);
        }

        // POST: VIAJE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViaje,IdConductor,IdCorredor,IdProducto,Estado")] VIAJE vIAJE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vIAJE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Nombre", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            return View(vIAJE);
        }

        // GET: VIAJE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAJE vIAJE = db.VIAJE.Find(id);
            if (vIAJE == null)
            {
                return HttpNotFound();
            }
            return View(vIAJE);
        }

        // POST: VIAJE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VIAJE vIAJE = db.VIAJE.Find(id);
            //db.VIAJE.Remove(vIAJE);
            vIAJE.Estado = false;
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
