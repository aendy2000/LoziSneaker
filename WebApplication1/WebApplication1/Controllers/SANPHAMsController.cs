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
    public class SANPHAMsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: SANPHAMs
        public ActionResult Index()
        {
            return View(db.SANPHAMs.ToList());
        }

        // GET: SANPHAMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return RedirectToAction("Index");
            }
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP");
            return View();
        }

        // POST: SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASP,TENSP,THUONGHIEU,GIA,MOTA,NGAYTHEM")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                CHITIETSP addMaSP = new CHITIETSP();
                addMaSP.MASANPHAM = sANPHAM.MASP.ToString();
                db.CHITIETSPs.Add(addMaSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", sANPHAM.MASP);
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", sANPHAM.MASP);
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASP,TENSP,THUONGHIEU,GIA,MOTA,NGAYTHEM")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MASP = new SelectList(db.SANPHAMs, "MASP", sANPHAM.MASP);
            return View(sANPHAM);
        }

        // GET: SANPHAMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            SANPHAM sANPHAM = db.SANPHAMs.Where(c => c.MASP == id).First();
            if (sANPHAM == null)
            {
                return RedirectToAction("Index");
            }
            return View(sANPHAM);
        }

        // POST: SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            string ma = id;
            var XoaSP = db.SANPHAMs.Where(c => c.MASP == ma).First();
            var XoaTSSP = db.CHITIETSPs.Where(c => c.MASANPHAM == ma).First();
            db.CHITIETSPs.Remove(XoaTSSP);
            db.SANPHAMs.Remove(XoaSP);
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
    }
}
