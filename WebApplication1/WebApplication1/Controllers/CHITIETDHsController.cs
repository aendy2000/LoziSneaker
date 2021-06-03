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
        public ActionResult Index()
        {
            var cHITIETDHs = db.CHITIETDHs.Include(c => c.DONHANG).Include(c => c.SANPHAM);
            return View(cHITIETDHs.ToList());
        }

        // GET: CHITIETDHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETDH cHITIETDH = db.CHITIETDHs.Find(id);
            if (cHITIETDH == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDH);
        }

        // POST: CHITIETDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIETDH cHITIETDH = db.CHITIETDHs.Find(id);
            db.CHITIETDHs.Remove(cHITIETDH);
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
