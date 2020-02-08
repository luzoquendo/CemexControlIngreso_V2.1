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
    public class INSTRUCTORController : Controller
    {

        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: INSTRUCTOR
        public ActionResult Index()
        {
            return View(db.INSTRUCTOR.ToList());
        }

        // GET: INSTRUCTOR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSTRUCTOR iNSTRUCTOR = db.INSTRUCTOR.Find(id);
            if (iNSTRUCTOR == null)
            {
                return HttpNotFound();
            }
            return View(iNSTRUCTOR);
        }

        // GET: INSTRUCTOR/Create
        public ActionResult Create()
        {
            ViewBag.showSuccessAlert = false;
            return View();
        }

        // POST: INSTRUCTOR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdInstructor,Nombre,Celular1,Celular2,Estado")] INSTRUCTOR iNSTRUCTOR)
        {
            if (ModelState.IsValid)
            {
                

                bool existeUsuario = db.INSTRUCTOR.Any(x => x.Nombre == iNSTRUCTOR.Nombre);
                if (!existeUsuario)
                {
                    iNSTRUCTOR.Nombre = iNSTRUCTOR.Nombre.ToUpper();
                    db.INSTRUCTOR.Add(iNSTRUCTOR);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Script = "<script type='text/javascript'>alert('Ya existe un registro con este nombre, por favor revise.');</script>";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(iNSTRUCTOR);
        }

        // GET: INSTRUCTOR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSTRUCTOR iNSTRUCTOR = db.INSTRUCTOR.Find(id);
            if (iNSTRUCTOR == null)
            {
                return HttpNotFound();
            }
            return View(iNSTRUCTOR);
        }

        // POST: INSTRUCTOR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdInstructor,Nombre,Celular1,Celular2,Estado")] INSTRUCTOR iNSTRUCTOR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNSTRUCTOR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iNSTRUCTOR);
        }

        // GET: INSTRUCTOR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INSTRUCTOR iNSTRUCTOR = db.INSTRUCTOR.Find(id);
            if (iNSTRUCTOR == null)
            {
                return HttpNotFound();
            }
            return View(iNSTRUCTOR);
        }

        // POST: INSTRUCTOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INSTRUCTOR iNSTRUCTOR = db.INSTRUCTOR.Find(id);
            //db.INSTRUCTOR.Remove(iNSTRUCTOR);
            iNSTRUCTOR.Estado = false;
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
