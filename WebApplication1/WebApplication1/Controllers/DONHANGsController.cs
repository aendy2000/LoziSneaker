using System;
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
    public class DONHANGsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: DONHANGs
        public ActionResult Index()
        {
            var donhang = db.DONHANGs.ToList();
            return View(donhang);
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
