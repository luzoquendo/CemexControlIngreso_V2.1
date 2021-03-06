﻿using System;
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
    public class PRODUCTOController : Controller
    {
        private CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();

        // GET: PRODUCTO
        public ActionResult Index()
        {
            return View(db.PRODUCTO.ToList());
        }

        // GET: PRODUCTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PRODUCTO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,Producto1,Estado")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.PRODUCTO.Any(x => x.Producto1.ToUpper() == pRODUCTO.Producto1.ToUpper());
                if (!existeUsuario)
                {
                    pRODUCTO.Producto1 = pRODUCTO.Producto1.ToUpper();
                    db.PRODUCTO.Add(pRODUCTO);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este nombre, " + pRODUCTO.Producto1 + " por favor revise...";
                }
            }
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTO);
        }

        // POST: PRODUCTO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,Producto1,Estado")] PRODUCTO pRODUCTO)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.PRODUCTO.Any(x => x.Producto1.ToUpper() == pRODUCTO.Producto1.ToUpper());
                if (!existeUsuario)
                {
                    db.Entry(pRODUCTO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este nombre, " + pRODUCTO.Producto1 + " por favor revise...";
                }
            }
            return View(pRODUCTO);
        }

        // GET: PRODUCTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            if (pRODUCTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCTO);
        }

        // POST: PRODUCTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCTO pRODUCTO = db.PRODUCTO.Find(id);
            //db.PRODUCTO.Remove(pRODUCTO);
            pRODUCTO.Estado = false;
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
