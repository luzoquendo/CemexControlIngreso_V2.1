using CemexControlIngreso_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemexControlIngreso_V2.Controllers
{
    public class ACCESSController : Controller
    {
        // GET: ACCESS
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enter(int user, string password)
        {
            try
            {
                using (CONTROLINGRESOEntities2 db = new CONTROLINGRESOEntities2())
                {
                    var lst = from d in db.User
                              where d.Codigo == user
                              select d;

                    if (lst.Count() > 0)
                    {
                        //User oUser = lst.First();
                        //Session["User"] = oUser;
                    }

                }
                return Content("1");
            }
            catch (Exception ex)
            {

                return Content("Ocurrio un error : " + ex.Message);

            }
        }
    }
}