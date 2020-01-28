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
using CemexControlIngreso_V2.Models;

namespace CemexControlIngreso_V2.Controllers
{
    public class SALIDAController : Controller
    {
        private CONTROLINGRESOEntities db = new CONTROLINGRESOEntities();

        // GET: CONDUCTOR
        public ActionResult Index()
        {
            return View();
        }

        // GET: CONDUCTOR/Create
        public ActionResult Create()
        {
            var cmdText = "[DoStuff] @Cedula = @Cedula";
            var @params = new[]{
            new SqlParameter("Cedula", "123456"),
            };

            //ObjectContext.ExecuteStoreQuery<DataTable>(cmdText, @params);

            //ObjectParameter[] parameters = new ObjectParameter[2];
            //parameters[0] = new ObjectParameter("Cedula", "123456");
            //var informacion = ObjectContext.ExecuteStoreQuery("dbo_TraerConductor", parameters);
            
            //ViewBag.Nombre = new SelectList(db.INSTRUCTOR.
            //       Where(o => o.Estado == true), "IdInstructor", "Nombre");

            return View();
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