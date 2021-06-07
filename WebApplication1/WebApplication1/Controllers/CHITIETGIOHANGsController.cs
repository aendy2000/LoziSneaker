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
    public class CHITIETGIOHANGsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: CHITIETGIOHANGs
        public ActionResult Index(string id)
        {
            var cHITIETGIOHANGs = db.CHITIETGIOHANGs.Where(c => c.MAGIOHG == id).ToList();
            return View(cHITIETGIOHANGs.ToList());
        }

        // GET: CHITIETGIOHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETGIOHANG cHITIETGIOHANG = db.CHITIETGIOHANGs.Find(id);
            if (cHITIETGIOHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETGIOHANG);
        }

        // POST: CHITIETGIOHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CHITIETGIOHANG cHITIETGIOHANG = db.CHITIETGIOHANGs.Find(id);
            string magh = cHITIETGIOHANG.MAGIOHG;
            int tonggia = cHITIETGIOHANG.SOLUONG.Value * cHITIETGIOHANG.GIA.Value;
            db.CHITIETGIOHANGs.Remove(cHITIETGIOHANG);
            var giohg = db.GIOHANGs.Where(c => (c.MAGIOHANG == magh)).First();
            int solg = int.Parse(giohg.SOLUONG);
            int solgnew = solg - 1;
            giohg.SOLUONG = solgnew.ToString();
            giohg.TONGTIEN = (giohg.TONGTIEN.Value - tonggia);
            db.SaveChanges();
            return RedirectToAction("Index", "GIOHANGs");
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
