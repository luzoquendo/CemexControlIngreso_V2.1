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
    public class VIAJECTRLController : Controller
    {
        private int conductorid = 0;
        private CONTROLINGRESOEntities3 db = new CONTROLINGRESOEntities3();
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

        // POST: VIAJE/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdViaje,IdConductor,IdCorredor,IdProducto,Estado,IdPlaca,IdTrailer,Alcohotest, NumeroViaje")] VIAJECTRL vIAJE)
        {
            //bool ok = autorizado();
            if (ModelState.IsValid)
            {
                db.VIAJECTRL.Add(vIAJE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Conductor1", vIAJE.IdConductor);
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

            return JsonConvert.SerializeObject(db.TraerConductorDisponible(id.ToString()));
        }

        public String ConsultaId(int? IdConductor)
        {

            // var conductorarr = db.TraerConductor(id.ToString());

            return JsonConvert.SerializeObject(db.TraerConductorId(IdConductor));
        }
        public class Inform
        {
            public string Nombre { get; set; }
            public string Celular1 { get; set; }
            public string Celular2 { get; set; }
            public string Cedula { get; set; }
        }

        // GET: VIAJE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string[] informV;
            informV = new string[13];

            string[] informC;
            informC = new string[5];

            string CedCond = "";
            string NombInst = "";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CONTROL"].ConnectionString))
            { // Create the command and set its properties. 
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "TraerViajeId";
                command.CommandType = CommandType.StoredProcedure;
                // Add the input parameter and set its properties. 
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@Id";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = id;
                // Add the parameter to the Parameters collection. 
                command.Parameters.Add(parameter);
                // Open the connection and execute the reader. 
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            informV[0] = reader[0].ToString();
                            informV[1] = reader[1].ToString();
                            informV[2] = reader[2].ToString();
                            informV[3] = reader[3].ToString();
                            informV[4] = reader[4].ToString();
                            informV[5] = reader[5].ToString();
                            informV[6] = reader[6].ToString();
                            informV[7] = reader[7].ToString();
                            informV[8] = reader[8].ToString();
                            informV[9] = reader[9].ToString();
                            informV[10] = reader[10].ToString();
                            informV[11] = reader[11].ToString();
                            informV[12] = reader[12].ToString();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();
                }


                string IdViaj = "";
                IdViaj = informV[4].ToString();

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
                }

                string IdInst = "";
                IdInst = informC[4].ToString();

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

                connection.Close();
            }

            VIAJECTRL vIAJE = db.VIAJECTRL.Find(id);

            bool ok = Autorizado(CedCond);
            if (vIAJE == null)
            {
                return HttpNotFound();
            }
            ViewBag.NombreCond = informC[0];
            ViewBag.Celular = informC[1];
            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Cedula");
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", vIAJE.IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "IdProducto", "Producto1", vIAJE.IdProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", vIAJE.idTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", vIAJE.idPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", vIAJE.IdViaje);
            ViewBag.Fecha = vIAJE.Fecha.ToString().Replace(" a. m.", "");
            ViewBag.Alcohotest = vIAJE.Alcohotest;
            ViewBag.Checklist = vIAJE.Checklist;
            ViewBag.Instructor = NombInst;
            ViewBag.FechaCtrl = DateTime.Now;
            return View(vIAJE);
        }

        // POST: VIAJE/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IdViaje,IdConductor,IdCorredor,idProducto,Estado,IdPlaca,IdTrailer,Alcohotest,NumeroViaje, Aceite, LLantas, Motor")] VIAJECTRL vIAJE)
        public ActionResult Edit(string IdViaje, string IdConductor, string IdCorredor, string idProducto, string Estado, string IdPlaca, string IdTrailer, string Alcohotest, string NumeroViaje, string Checklist)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            VIAJECTRL vIAJE1 = new VIAJECTRL();
            Descanso Descanso1 = new Descanso();
            VIAJECTRL vIAJE = db.VIAJECTRL.Find(Convert.ToInt32(IdViaje));
            conductorid = vIAJE.IdConductor;

            string[] informC;
            informC = new string[5];
            string IdInst = "";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CONTROL"].ConnectionString))
            { // Create the command and set its properties. 
                string IdViaj = "";
                IdViaj = IdConductor;

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
                connection.Open();
                // Open the connection and execute the reader. 
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
                    reader1.Close();
                }
                IdInst = informC[4].ToString();
                connection.Close();
            }


            DateTime Fecha = vIAJE.Fecha;
            var inform = JsonConvert.SerializeObject(db.TraerConductorId(vIAJE.IdConductor));
            Inform[] inform1 = js.Deserialize<Inform[]>(inform);
            string cc = inform1[0].Cedula;
            bool ok = Autorizado(cc);

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                //El conductor no puede estar asignado a otro viaje activo
                bool estadoViaje = false;
                int cond = Convert.ToInt32(IdConductor);
                int viaj = Convert.ToInt32(IdViaje);
                bool existeViaje = db.VIAJECTRL.Any(x => x.IdConductor == cond && x.IdViaje != viaj);
                estadoViaje = db.VIAJECTRL.Any(x => x.Estado == true);
                //Si cambia conductor el conductor entra a descanso
                if (!existeViaje)
                {
                    if (vIAJE.IdConductor != Convert.ToInt32(IdConductor))
                    {
                        Descanso1.IdConductor = vIAJE.IdConductor;
                        Descanso1.IdViaje = vIAJE.IdViaje;
                        Descanso1.FechaDescanso = DateTime.Now;
                        Descanso1.Estado = "1";
                        db.Descanso.Add(Descanso1);
                        db.SaveChanges();

                        DateTime FechaRegreso = DateTime.Now.AddHours(9);
                        //Tiempos de descanso

                        ViewBag.Message = "Por favor recuerde que la fecha estimada de llegada del descanso es: ..."+ FechaRegreso.ToShortDateString();
                    }

                    if (!vIAJE.Estado)
                    {
                        Descanso1.IdConductor = vIAJE.IdConductor;
                        Descanso1.IdViaje = vIAJE.IdViaje;
                        Descanso1.FechaDescanso = DateTime.Now;
                        Descanso1.Estado = "1";
                        db.Descanso.Add(Descanso1);
                        db.SaveChanges();

                        DateTime FechaRegreso = DateTime.Now.AddHours(9);

                        ActualizarViaje(Convert.ToInt32(vIAJE.NumeroViaje));
                        //Tiempos de descanso
                        ViewBag.Message = "Por favor recuerde que la fecha estimada de llegada del descanso es: ..." + FechaRegreso.ToShortDateString();
                    }

                    vIAJE1.IdViaje = Convert.ToInt32(IdViaje);
                    vIAJE1.IdConductor = Convert.ToInt32(IdConductor);
                    vIAJE1.idPlaca = Convert.ToInt32(IdPlaca);
                    vIAJE1.idTrailer = Convert.ToInt32(IdTrailer);
                    vIAJE1.IdProducto = Convert.ToInt32(idProducto);
                    vIAJE1.IdCorredor = Convert.ToInt32(IdCorredor);
                    vIAJE1.Fecha = Convert.ToDateTime(Fecha);
                    vIAJE1.NumeroViaje = NumeroViaje;
                    vIAJE1.Alcohotest = Alcohotest;
                    vIAJE1.FechaCtrl = System.DateTime.Now;
                    vIAJE1.Checklist = Checklist;
                    vIAJE1.IdInstructor = Convert.ToInt32(IdInst);
                    vIAJE1.Estado = true;
                    db.VIAJECTRL.Add(vIAJE1);
                    //db.Entry(vIAJE).State = EntityState.Modified;
                    db.SaveChanges();

                    vIAJE.Estado = false;
                    db.Entry(vIAJE).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "El conductor " + IdConductor + " se encuentra activo en otro viaje por favor revise...";
                }
            }

            ViewBag.IdConductor = new SelectList(db.CONDUCTOR, "IdConductor", "Cedula", IdConductor);
            ViewBag.IdCorredor = new SelectList(db.CORREDOR, "IdCorredor", "Corredor1", IdCorredor);
            ViewBag.IdProducto = new SelectList(db.PRODUCTO, "idProducto", "Producto1", idProducto);
            ViewBag.IdTrailer = new SelectList(db.TRAILER, "IdTrailer", "PlacaTrailer", IdTrailer);
            ViewBag.IdPlaca = new SelectList(db.PLACAS, "IdPlaca", "Placa", IdPlaca);
            ViewBag.IdViaje = new SelectList(db.VIAJE, "IdViaje", "Viaje", IdViaje);
            ViewBag.Alcohotest = Alcohotest;
            ViewBag.Checklist = Checklist;
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

        public bool Autorizado(string Cedula)
        {
            var tbl = new DataTable();

            using (var conn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = C:\\ZKTeco\\ZKAccess3.5\\Access.mdb"))
            {
                DateTime autenticacion = new DateTime();
                DateTime date2 = new DateTime();
                string connetionString = null;
                OleDbConnection oledbCnn;
                OleDbDataAdapter oledbAdapter;
                DataSet ds = new DataSet();
                string sql = null;
                int i = 0;

                connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\\ZKTeco\\ZKAccess3.5\\Access.mdb";
                sql = "SELECT top 1 * FROM acc_monitor_log WHERE acc_monitor_log.[pin] = '" + Cedula.ToString().Trim() + "' order by time desc";

                oledbCnn = new OleDbConnection(connetionString);
                try
                {
                    oledbCnn.Open();
                    oledbAdapter = new OleDbDataAdapter(sql, oledbCnn);
                    oledbAdapter.Fill(ds);
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        autenticacion = (DateTime)ds.Tables[0].Rows[i].ItemArray[9];
                    }
                    oledbAdapter.Dispose();
                    oledbCnn.Close();
                    date2 = DateTime.Now;
                    TimeSpan result = date2.Subtract(autenticacion);

                    if (result.Minutes > 5)
                    {
                        ViewBag.Message = "El conductor se registro en el biometrico hace mas de cinco minutos por favor revise...";
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public String ActualizarViaje(int? NumeroViaje)
        {
            return JsonConvert.SerializeObject(db.ActualizarViaje(NumeroViaje));
        }

        public bool perfil(string cedula)
        {
            string connetionString = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter oledbAdapter;
            DataSet ds = new DataSet();
            string sql = null;
            int i = 0;

            connetionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= C:\\ZKTeco\\ZKAccess3.5\\Access.mdb";
            sql = "SELECT * FROM acc_monitor_log WHERE acc_monitor_log.[pin] = '" + cedula.ToString().Trim();

            oledbCnn = new OleDbConnection(connetionString);
            try
            {
                oledbCnn.Open();
                oledbAdapter = new OleDbDataAdapter(sql, oledbCnn);
                oledbAdapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
