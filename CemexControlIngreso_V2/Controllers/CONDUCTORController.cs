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
    public class CONDUCTORController : Controller
    {
        private CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();

        // GET: CONDUCTOR
        public ActionResult Index()
        {
            var cONDUCTOR = db.CONDUCTOR.Include(c => c.INSTRUCTOR);
            return View(cONDUCTOR.ToList());

        }

        // GET: CONDUCTOR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONDUCTOR cONDUCTOR = db.CONDUCTOR.Find(id);
            if (cONDUCTOR == null)
            {
                return HttpNotFound();
            }
            return View(cONDUCTOR);
        }

        // GET: CONDUCTOR/Create
        public ActionResult Create()
        {
            ViewBag.IdInstructor = new SelectList(db.INSTRUCTOR.
                   Where(o => o.Estado == true), "IdInstructor", "Nombre");

            return View();
        }

        private static List<INSTRUCTOR> CargarInstructor()
        {
            List<INSTRUCTOR> instructorl = new List<INSTRUCTOR>();
            string constr = ConfigurationManager.ConnectionStrings["CONTROLINGRESOEntities2"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "select i.IdInstructor, i.Nombre from db.INSTRUCTOR i where i.Estado = 1";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            instructorl.Add(new INSTRUCTOR
                            {
                                Nombre = sdr["Nombre"].ToString(),
                                IdInstructor = Convert.ToInt32(sdr["IdInstructor"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return instructorl;
        }
        // POST: CONDUCTOR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdConductor,Nombre,IdInstructor,Celular1,Celular2,Estado,Cedula")] CONDUCTOR cONDUCTOR)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.CONDUCTOR.Any(x => x.Cedula == cONDUCTOR.Cedula);
                if (!existeUsuario)
                {
                    cONDUCTOR.Estado = true;
                    cONDUCTOR.Nombre = cONDUCTOR.Nombre.ToUpper();
                    db.CONDUCTOR.Add(cONDUCTOR);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este numero de Cedula, "+ cONDUCTOR.Cedula +" por favor revise...";
                }
 
                //return RedirectToAction("Index");
            }

            ViewBag.IdInstructor = new SelectList(db.INSTRUCTOR, "IdInstructor", "Nombre", cONDUCTOR.IdInstructor);
            return View(cONDUCTOR);
        }

        // GET: CONDUCTOR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONDUCTOR cONDUCTOR = db.CONDUCTOR.Find(id);
            if (cONDUCTOR == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdInstructor = new SelectList(db.INSTRUCTOR, "IdInstructor", "Nombre", cONDUCTOR.IdInstructor);
            return View(cONDUCTOR);
        }

        // POST: CONDUCTOR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdConductor,Nombre,IdInstructor,Celular1,Celular2,Estado")] CONDUCTOR cONDUCTOR)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.CONDUCTOR.Any(x => x.Cedula == cONDUCTOR.Cedula);
                if (!existeUsuario)
                {
                    db.Entry(cONDUCTOR).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este numero de Cedula, " + cONDUCTOR.Cedula + " por favor revise...";
                }
            }
            ViewBag.IdInstructor = new SelectList(db.INSTRUCTOR, "IdInstructor", "Nombre", cONDUCTOR.IdInstructor);
            return View(cONDUCTOR);
        }

        // GET: CONDUCTOR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONDUCTOR cONDUCTOR = db.CONDUCTOR.Find(id);
            if (cONDUCTOR == null)
            {
                return HttpNotFound();
            }
            return View(cONDUCTOR);
        }

        // POST: CONDUCTOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONDUCTOR cONDUCTOR = db.CONDUCTOR.Find(id);
            //db.CONDUCTOR.Remove(cONDUCTOR);
            cONDUCTOR.Estado = false;
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
