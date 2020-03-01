using CemexControlIngreso_V2.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CemexControlIngreso_V2.Controllers
{
    public class VIAJEController : Controller
    {
        private CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();
        private CONTROLINGRESOEntities3 db1 = new CONTROLINGRESOEntities3();
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
            ViewBag.Message = "Recuerde tener a la mano su nUmero de checklist...";

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR.
                Where(o => o.Estado == true), "idConductor", "Cedula");
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

        // POST: VIAJE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViaje,IdConductor,IdCorredor,IdProducto,Estado,IdPlaca,IdTrailer,Alcohotest,NumeroViaje,Checklist,IdInstructor")] VIAJE vIAJE)
        {
            vIAJE.Fecha = DateTime.Now;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                string idCond = vIAJE.IdConductor.ToString();
                bool existeViaje = db.VIAJE.Any(x => x.NumeroViaje == vIAJE.NumeroViaje);
                bool existePlaca = db.VIAJE.Any(x => x.idPlaca == vIAJE.idPlaca);
                bool existeTrailer = db.VIAJE.Any(x => x.idTrailer == vIAJE.idTrailer);
                bool existeUsuario = db.VIAJE.Any(x => x.IdConductor == vIAJE.IdConductor && x.Estado == true); //db.VIAJE.Any(x => x.IdConductor == vIAJE.IdConductor && x.IdCorredor == vIAJE.IdCorredor && x.IdInstructor == vIAJE.IdInstructor && x.idPlaca == vIAJE.idPlaca && x.IdProducto == vIAJE.IdProducto && x.idTrailer == vIAJE.idTrailer && x.Estado == vIAJE.Estado);
                string idInst = JsonConvert.SerializeObject(db.TraerInstructorIdCond(idCond));
                if (!existeViaje)
                {
                    if (!existePlaca)
                    {
                        if (!existeTrailer)
                        {
                            if (!existeUsuario)
                            {
                                vIAJE.Estado = true;
                                vIAJE.IdInstructor = Convert.ToInt32(idInst);
                                db.VIAJE.Add(vIAJE);
                                db.SaveChanges();

                                VIAJECTRL vIAJECTRL = new VIAJECTRL();
                                vIAJECTRL.Alcohotest = vIAJE.Alcohotest;
                                vIAJECTRL.Estado = vIAJE.Estado;
                                vIAJECTRL.Fecha = DateTime.Now;
                                vIAJECTRL.FechaCtrl = DateTime.Now;
                                vIAJECTRL.IdConductor = vIAJE.IdConductor.Value;
                                vIAJECTRL.IdCorredor = vIAJE.IdCorredor;
                                vIAJECTRL.IdInstructor = vIAJE.IdInstructor;
                                vIAJECTRL.idPlaca = vIAJE.idPlaca;
                                vIAJECTRL.IdProducto = vIAJE.IdProducto;
                                vIAJECTRL.idTrailer = vIAJE.idTrailer;
                                vIAJECTRL.IdViaje = vIAJE.IdViaje;
                                vIAJECTRL.NumeroViaje = vIAJE.NumeroViaje;
                                vIAJECTRL.Checklist = vIAJE.Checklist;

                                db1.VIAJECTRL.Add(vIAJECTRL);
                                db1.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.Message = "Ya existe un registro con este conductor," + vIAJE.IdConductor + " por favor revise...";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Ya existe un registro con este numero de trailer," + vIAJE.idTrailer + " por favor revise...";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Ya existe un registro con este numero de placa," + vIAJE.idPlaca + " por favor revise...";
                    }
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este numero de viaje," + vIAJE.NumeroViaje + " por favor revise...";
                }
            }

            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Cedula", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "idProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.alcohotest = new SelectList(db.VIAJE, "Alcohotest", "Alcohotest", vIAJE.Alcohotest);


            return View(vIAJE);
        }

        public String Consulta(int id)
        {
            return JsonConvert.SerializeObject(db.TraerConductor(id.ToString()));
        }

        public String ConsultaId(int? IdConductor)
        {
            return JsonConvert.SerializeObject(db.TraerConductorId(IdConductor));
        }

        public String ConsultaViajePlaca(string Placa)
        {
            return JsonConvert.SerializeObject(db.TraerViajePlaca(Placa));
        }

        public String TraerInstructor(string id)
        {
            return JsonConvert.SerializeObject(db.TraerInstructor(id));
        }

        public class Inform
        {
            public string Nombre { get; set; }
            public string Celular1 { get; set; }
            public string Celular2 { get; set; }
            public string Cedula { get; set; }
            public string IdInst { get; set; }
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
            //string IdInst = inform1[4].ToString();

            string[] informC;
            informC = new string[5];
            string IdViaj = "";
            IdViaj = vIAJE.IdConductor.ToString();

            string CedCond = "";
            string NombInst = "";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CONTROL"].ConnectionString))
            {
                SqlCommand command1 = new SqlCommand();
                command1.Connection = connection;
                command1.CommandText = "TraerConductorId";
                command1.CommandType = CommandType.StoredProcedure;
                // Add the input parameter and set its properties. 
                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@Id";
                parameter1.SqlDbType = SqlDbType.Int;
                parameter1.Direction = ParameterDirection.Input;
                parameter1.Value = Convert.ToInt32(IdViaj);
                // Add the parameter to the Parameters collection. 
                command1.Parameters.Add(parameter1);
                // Open the connection and execute the reader. 
                connection.Open();
                using (SqlDataReader reader1 = command1.ExecuteReader())
                {
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            informC[0] = reader1[0].ToString();
                            informC[1] = reader1[1].ToString();
                            informC[2] = reader1[2].ToString();
                            informC[3] = reader1[3].ToString();
                            informC[4] = reader1[4].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    CedCond = informC[3];
                    reader1.Close();

                    string IdInst = informC[4].ToString();

                    SqlCommand command2 = new SqlCommand();
                    command2.Connection = connection;
                    command2.CommandText = "[TraerInstructor]";
                    command2.CommandType = CommandType.StoredProcedure;
                    // Add the input parameter and set its properties. 
                    SqlParameter parameter2 = new SqlParameter();
                    parameter2.ParameterName = "@Id";
                    parameter2.SqlDbType = SqlDbType.Int;
                    parameter2.Direction = ParameterDirection.Input;
                    parameter2.Value = Convert.ToInt32(IdInst);
                    // Add the parameter to the Parameters collection. 
                    command2.Parameters.Add(parameter2);
                    // Open the connection and execute the reader. 
                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                NombInst = reader2[0].ToString();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No rows found.");
                        }
                        reader2.Close();
                    }
                }
                connection.Close();
            }

            if (vIAJE == null)
            {
                return HttpNotFound();
            }

            ViewBag.NombreCond = informC[0];// inform1[0].Nombre;
            ViewBag.Celular = informC[1];//.Celular1;
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "idConductor", "Cedula", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.Alcohotest = "";
            ViewBag.Fecha = vIAJE.Fecha;
            ViewBag.Checklist = vIAJE.Checklist;
            ViewBag.NombreInst = NombInst;

            //ViewBag.Instructor = NombInst;
            return View(vIAJE);
        }

        // POST: VIAJE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdViaje,IdConductor,IdCorredor,idProducto,Estado,IdPlaca,IdTrailer,alcohotest,Fecha,Checklist")] VIAJE vIAJE)
        {
            if (ModelState.IsValid)
            {
                bool existeViaje = db.VIAJE.Any(x => x.NumeroViaje == vIAJE.NumeroViaje && x.Estado == true); //db.VIAJE.Any(x => x.IdConductor == vIAJE.IdConductor && x.IdCorredor == vIAJE.IdCorredor && x.IdInstructor == vIAJE.IdInstructor && x.idPlaca == vIAJE.idPlaca && x.IdProducto == vIAJE.IdProducto && x.idTrailer == vIAJE.idTrailer && x.Estado == vIAJE.Estado);
                if (!existeViaje)
                {
                    db.Entry(vIAJE).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Ya existe un registro con este numero de viaje," + vIAJE.NumeroViaje + " por favor revise...";
                }
            }
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Nombre", vIAJE.IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "idProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.alcohotest = vIAJE.Alcohotest;
            ViewBag.Fecha = vIAJE.Fecha;
            ViewBag.Checklist = vIAJE.Checklist;
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

        public ActionResult Importar_Click()
        {
            //Microsoft.Office.Interop.Excel.Application ExcelObj = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook theWorkbook = ExcelObj.Workbooks.Open("C:/ Proyecto Cemex/conductores.xls", 0, true, 5,"", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false,0, true);

            //Microsoft.Office.Interop.Excel.Sheets sheets = theWorkbook.Worksheets;
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1); 




            string conexion = "Provider = Microsoft.Jet.OleDb.4.0; Data Source = C:/Proyecto Cemex/conductores.xls; Extended Propierties = Excel 8.0;HDR=YES;'";
            OleDbConnection conector = default(OleDbConnection);
            conector = new OleDbConnection(conexion);
            conector.Open();

            OleDbCommand consulta = default(OleDbCommand);
            consulta = new OleDbCommand("select * from [Hoja1$]", conector);

            OleDbDataAdapter adaptador = new OleDbDataAdapter();
            adaptador.SelectCommand = consulta;

            DataSet ds = new DataSet();
            adaptador.Fill(ds);

            //dataGridView.DataSource = ds.Tables[0];

            conector.Close();
            return RedirectToAction("Index");
        }
    }
}
