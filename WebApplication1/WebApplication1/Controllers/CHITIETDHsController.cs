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


        // GET: CHITIETDHs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var editCHITIETDHs = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            if (editCHITIETDHs == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.MADH = new SelectList(db.DONHANGs, "MADH", editCHITIETDHs.ToList());
            return View(editCHITIETDHs.ToList());
        }

        // POST: CHITIETDHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CHITIETDH editCHITIETDHs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editCHITIETDHs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADH = new SelectList(db.DONHANGs, "MADH", "TKKH", editCHITIETDHs.MADH);
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", "TENSP", editCHITIETDHs.MASP);
            return View(editCHITIETDHs);
        }

        // GET: CHITIETDHs/Delete/5
        public ActionResult Delete(string id)
        {
            var cHITIETDH = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            if (cHITIETDH == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETDH.ToList());
        }

        // POST: CHITIETDHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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
