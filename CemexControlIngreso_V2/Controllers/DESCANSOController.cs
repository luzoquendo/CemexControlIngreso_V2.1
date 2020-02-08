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
            ViewBag.IdCorredor = new SelectList(db.CORREDOR.
                Where(o => o.Estado == true), "IdCorredor", "Corredor1");
            ViewBag.IdProducto = new SelectList(db.PRODUCTO.
                Where(o => o.Estado == true), "idProducto", "Producto1");
            ViewBag.IdTrailer = new SelectList(db.TRAILER.
                Where(o => o.Estado == true), "IdTrailer", "PlacaTrailer");
            ViewBag.IdPlaca = new SelectList(db.PLACAS.
                Where(o => o.Estado == true), "IdPlaca", "Placa");
            return View();
        }
    }
}