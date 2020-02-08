using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using CemexControlIngreso_V2.Models;
using Newtonsoft.Json;

namespace CemexControlIngreso_V2.Controllers
{
    public class VIAJEController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();
        [Authorize]
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
            ViewBag.IdCorredor = new SelectList(db.CORREDOR.
                Where(o => o.Estado == true),"IdCorredor", "Corredor1");
            ViewBag.IdProducto = new SelectList(db.PRODUCTO.
                Where(o => o.Estado == true),"idProducto", "Producto1");
            ViewBag.IdTrailer = new SelectList(db.TRAILER.
                Where(o => o.Estado == true), "IdTrailer", "PlacaTrailer");
            ViewBag.IdPlaca = new SelectList(db.PLACAS.
                Where(o => o.Estado == true), "IdPlaca", "Placa");
            return View();
        }

        // POST: VIAJE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViaje,IdConductor,IdCorredor,IdProducto,Estado,IdPlaca,IdTrailer,Alcohotest,NumeroViaje")] VIAJE vIAJE)
        {
            if (ModelState.IsValid)
            {
                bool existeUsuario = db.VIAJE.Any(x => x.IdConductor == vIAJE.IdConductor && x.IdCorredor == vIAJE.IdCorredor && x.IdInstructor == vIAJE.IdInstructor && x.idPlaca == vIAJE.idPlaca && x.IdProducto == vIAJE.IdProducto && x.idTrailer == vIAJE.idTrailer && x.Fecha == vIAJE.Fecha);
                if (!existeUsuario)
                {
                    VIAJECTRL vIAJECTRL = new VIAJECTRL();
                    vIAJE.Fecha = DateTime.Now;
                    vIAJE.Estado = true;
                    db.VIAJE.Add(vIAJE);
                    db.SaveChanges();

                    vIAJECTRL.Alcohotest = vIAJE.Alcohotest;
                    vIAJECTRL.Estado = vIAJE.Estado.Value;
                    vIAJECTRL.Fecha = vIAJE.Fecha.Value;
                    vIAJECTRL.FechaCtrl = DateTime.Now;
                    vIAJECTRL.IdConductor = vIAJE.IdConductor.Value;
                    vIAJECTRL.IdCorredor = vIAJE.IdCorredor;
                    vIAJECTRL.IdInstructor = vIAJE.IdInstructor;
                    vIAJECTRL.idPlaca = vIAJE.idPlaca.Value;
                    vIAJECTRL.IdProducto = vIAJE.IdProducto.Value;
                    vIAJECTRL.idTrailer = vIAJE.idTrailer.Value;
                    vIAJECTRL.IdViaje = vIAJE.IdViaje;
                    vIAJECTRL.NumeroViaje = vIAJE.NumeroViaje;

                    db.VIAJECTRL.Add(vIAJECTRL);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Script = "<script type='text/javascript'>alert('Ya existe un registro con este nombre, por favor revise.');</script>";
                    return RedirectToAction("Index");
                }
            }

            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Conductor1",vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "idProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.alcohotest = new SelectList(db.VIAJE, "Alcohotest", "Alcohotest", vIAJE.Alcohotest);
            return View(vIAJE);
        }

        public String Consulta(int? id)
        {

           // var conductorarr = db.TraerConductor(id.ToString());

            return JsonConvert.SerializeObject(db.TraerConductor(id.ToString()));
        }

        public String ConsultaId(int? IdConductor)
        {

            // var conductorarr = db.TraerConductor(id.ToString());

            return JsonConvert.SerializeObject(db.TraerConductorId(IdConductor));
        }
        public class Inform
        {
            public string Nombre{ get; set; }
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
            VIAJE vIAJE = db.VIAJE.Find(id);
            var inform = JsonConvert.SerializeObject(db.TraerConductorId(vIAJE.IdConductor));
            Inform[] inform1 = js.Deserialize<Inform[]>(inform);

            if (vIAJE == null)
            {
                return HttpNotFound();
            }

            ViewBag.NombreCond = inform1[0].Nombre;
            ViewBag.Celular = inform1[0].Celular1;
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR,"idConductor", "Nombre", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.Alcohotest = "";
            ViewBag.Fecha = vIAJE.Fecha;
            return View(vIAJE);
        }

        // POST: VIAJE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViaje,IdConductor,IdCorredor,idProducto,Estado,IdPlaca,IdTrailer,alcohotest")] VIAJE vIAJE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vIAJE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Nombre", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "idProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.alcohotest = vIAJE.Alcohotest;
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
