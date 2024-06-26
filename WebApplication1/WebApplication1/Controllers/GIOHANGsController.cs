﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles ="Admin,Nhân viên")]
    public class GIOHANGsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: GIOHANGs
        public ActionResult Index()
        {
            var gIOHANGs = db.GIOHANGs.Include(g => g.AspNetUser);
            return View(gIOHANGs.ToList());
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
