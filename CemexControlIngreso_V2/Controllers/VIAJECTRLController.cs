using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.OleDb;
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
    public class VIAJECTRLController : Controller
    {
        private int conductorid = 0;
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();
        [Authorize]
        // GET: VIAJE
        public ActionResult Index()
        {
            var vIAJE = db.VIAJECTRL.Include(v => v.CONDUCTOR).Include(v => v.CORREDOR).Include(v => v.PRODUCTO).Include(v => v.PLACAS).Include(v => v.TRAILER);
            return View(vIAJE.ToList());
        }

        // GET: VIAJE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAJECTRL vIAJE = db.VIAJECTRL.Find(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViaje,IdConductor,IdCorredor,IdProducto,Estado,IdPlaca,IdTrailer,Alcohotest, NumeroViaje")] VIAJECTRL vIAJE)
        {
            if (ModelState.IsValid)
            {
                db.VIAJECTRL.Add(vIAJE);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            VIAJECTRL vIAJE = db.VIAJECTRL.Find(id);
            conductorid = vIAJE.IdConductor;
            var inform = JsonConvert.SerializeObject(db.TraerConductorId(vIAJE.IdConductor));
            Inform[] inform1 = js.Deserialize<Inform[]>(inform);

            if (vIAJE == null)
            {
                return HttpNotFound();
            }

            ViewBag.NombreCond = inform1[0].Nombre;
            ViewBag.Celular = inform1[0].Celular1;
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR,"idConductor", "Nombre", conductorid);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.Fecha = vIAJE.Fecha.ToString().Replace(" a. m.", "");
            ViewBag.Alcohotest = vIAJE.Alcohotest;
            return View(vIAJE);
        }

        // POST: VIAJE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IdViaje,IdConductor,IdCorredor,idProducto,Estado,IdPlaca,IdTrailer,Alcohotest,NumeroViaje, Aceite, LLantas, Motor")] VIAJECTRL vIAJE)
        public ActionResult Edit(string IdViaje, string IdConductor, string IdCorredor, string idProducto, string Estado, string IdPlaca, string IdTrailer, string Alcohotest, string NumeroViaje, DateTime Fecha)
        {
            bool ok = autorizado();
            VIAJECTRL vIAJE1 = new VIAJECTRL();
            VIAJECTRL vIAJE = db.VIAJECTRL.Find(Convert.ToInt32(IdViaje));
            conductorid = vIAJE.IdConductor;
            if (ModelState.IsValid)
            {
                vIAJE1.IdViaje = Convert.ToInt32(IdViaje);
                vIAJE1.IdConductor = conductorid;
                vIAJE1.idPlaca = Convert.ToInt32(IdPlaca);
                vIAJE1.idTrailer = Convert.ToInt32(IdTrailer);
                vIAJE1.IdProducto = Convert.ToInt32(idProducto);
                vIAJE1.IdCorredor = Convert.ToInt32(IdCorredor);
                vIAJE1.Fecha = Convert.ToDateTime(Fecha);
                vIAJE1.NumeroViaje = NumeroViaje;
                vIAJE1.Alcohotest = Alcohotest;
                vIAJE1.FechaCtrl = System.DateTime.Now;
                vIAJE1.Estado = true;
                db.VIAJECTRL.Add(vIAJE1);
                //db.Entry(vIAJE).State = EntityState.Modified;
                db.SaveChanges();

                vIAJE.Estado = false;
                db.Entry(vIAJE).State = EntityState.Modified;
                db.SaveChanges();

                //cHECKLIST.Aceite = vIAJE.a;
                return RedirectToAction("Index");
                //return new RedirectResult(DESCANSOController.Url.RouteUrl(new { action = "Edit", p = 1}));
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Nombre", IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "idProducto", "Producto1", idProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", IdTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", IdPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", IdViaje);
            ViewBag.Alcohotest = Alcohotest;
            return View(vIAJE);
        }

        // GET: VIAJE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VIAJECTRL vIAJE = db.VIAJECTRL.Find(id);
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
            VIAJECTRL vIAJE = db.VIAJECTRL.Find(id);
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

        public ActionResult _Empresas()
        {
            try
            {
                return PartialView();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ChildActionOnly]
        public ActionResult _CHECKLIST([Bind(Include = "LLantas,Motor,Aceite,IdViaje")] CHECKLIST cHECKLIST)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cHECKLIST.Fecha = DateTime.Now;
                    db.CHECKLIST.Add(cHECKLIST);

                    db.SaveChanges();

                }

                return PartialView();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool autorizado()
        {
            var tbl = new DataTable();

            using (var conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\ZKTeco\\ZKAccess3.5\\Access.accdb"))
            {
                string sql = "SELECT * FROM acc_monitor_log";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                conn.Open();

                OleDbDataReader reader;

                reader = cmd.ExecuteReader();

                while (reader.Read())

                {

                    Console.Write(reader.GetString(0).ToString() + "\t \t");

                    Console.Write(reader.GetString(1).ToString() + "\t \t ");

                    Console.WriteLine(reader.GetDecimal(2));

                }
                reader.Close();
                conn.Close();
            }
            return true;
        }
    }
}
