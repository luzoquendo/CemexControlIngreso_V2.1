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
    public class CORREDORController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: CORREDOR
        public ActionResult Index()
        {
            return View(db.CORREDOR.ToList());
        }

        // GET: CORREDOR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CORREDOR cORREDOR = db.CORREDOR.Find(id);
            if (cORREDOR == null)
            {
                return HttpNotFound();
            }
            return View(cORREDOR);
        }

        // GET: CORREDOR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CORREDOR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCorredor,Corredor1,Estado")] CORREDOR cORREDOR)
        {
            if (ModelState.IsValid)
            {
                db.CORREDOR.Add(cORREDOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cORREDOR);
        }

        // GET: CORREDOR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CORREDOR cORREDOR = db.CORREDOR.Find(id);
            if (cORREDOR == null)
            {
                return HttpNotFound();
            }
            return View(cORREDOR);
        }

        // POST: CORREDOR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCorredor,Corredor1,Estado")] CORREDOR cORREDOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cORREDOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cORREDOR);
        }

        // GET: CORREDOR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CORREDOR cORREDOR = db.CORREDOR.Find(id);
            if (cORREDOR == null)
            {
                return HttpNotFound();
            }
            return View(cORREDOR);
        }

        // POST: CORREDOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CORREDOR cORREDOR = db.CORREDOR.Find(id);
            //db.CORREDOR.Remove(cORREDOR);
            cORREDOR.Estado = false;
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
