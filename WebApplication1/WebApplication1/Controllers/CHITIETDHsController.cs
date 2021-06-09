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
    public class CHITIETDHsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: CHITIETDHs
        public ActionResult Index(string id)
        {
            var cHITIETDH = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            return View(cHITIETDH.ToList());
        }

        public ActionResult Index2(string id)
        {
            var cHITIETDH = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            return View(cHITIETDH.ToList());
        }

        public ActionResult DeleteDonHangUser(string id)
        {
            var DeleteDetailsDonHang = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            var DeleteDonHang = db.DONHANGs.FirstOrDefault(c => c.MADH == id);
            db.CHITIETDHs.RemoveRange(DeleteDetailsDonHang);
            db.DONHANGs.Remove(DeleteDonHang);
            db.SaveChanges();
            return RedirectToAction("Index", "DONHANGs");
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
