﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CemexControlIngreso_V2.Controllers
{
    public class HomeConductorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checklist()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}