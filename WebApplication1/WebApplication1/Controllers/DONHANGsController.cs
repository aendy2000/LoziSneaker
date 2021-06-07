using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;

namespace WebApplication1.Controllers
{
    public class DONHANGsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: DONHANGs
        [Authorize(Roles = "Nhân viên,Admin")]
        public ActionResult Index(DONHANG dh)
        {
            var donhang = db.DONHANGs.ToList();
            return View(donhang);
        }

        [AllowAnonymous]
        public ActionResult Index2()
        {
            string id = User.Identity.GetUserId();
            var donhang = db.DONHANGs.Where(c => c.TKKH == id).ToList();
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
