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
    public class CHITIETSPsController : Controller
    {
        private CT25Team13Entities dbo = new CT25Team13Entities();

        // GET: CHITIETSPs
        public ActionResult Index()
        {
            return RedirectToAction("Index", "SANPHAMs");
        }

        // GET: CHITIETSPs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "SANPHAMs");
            }
            CHITIETSP cHITIETSP = dbo.CHITIETSPs.Find(id);
            if (cHITIETSP == null)
            {
                return RedirectToAction("Index", "SANPHAMs");
            }
            return View(cHITIETSP);
        }

        // GET: CHITIETSPs/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP");
        //    return View();
        //}

        //// POST: CHITIETSPs/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MASANPHAM,SL_TONG,SL_SIZE36,SL_SIZE37,SL_SIZE38,SL_SIZE39,SL_SIZE40,SL_SIZE41,SL_SIZE42,SL_SIZE43,MAUSAC1,MAUSAC2,MAUSAC3,MAUSAC4,MAUSAC5,MAUSAC6,MAUSAC7,MAUSAC8,MAUSAC9,MAUSAC10,HINHANH1,HINHANH2,HINHANH3,HINHANH4,HINHANH5")] CHITIETSP cHITIETSP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        dbo.CHITIETSPs.Add(cHITIETSP);
        //        dbo.SaveChanges();
        //        return RedirectToAction("Index", "SANPHAMs");
        //    }

        //    ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP", cHITIETSP.MASANPHAM);
        //    return View(cHITIETSP);
        //}

        // GET: CHITIETSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "SANPHAMs");
            }
            CHITIETSP cHITIETSP = dbo.CHITIETSPs.Find(id);
            if (cHITIETSP == null)
            {
                return RedirectToAction("Index", "SANPHAMs");
            }
            ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP", cHITIETSP.MASANPHAM);
            return View(cHITIETSP);
        }

        // POST: CHITIETSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASANPHAM,SL_TONG,SL_SIZE36,SL_SIZE37,SL_SIZE38,SL_SIZE39,SL_SIZE40,SL_SIZE41,SL_SIZE42,SL_SIZE43,MAUSAC1,MAUSAC2,MAUSAC3,MAUSAC4,MAUSAC5,MAUSAC6,MAUSAC7,MAUSAC8,MAUSAC9,MAUSAC10,HINHANH1,HINHANH2,HINHANH3,HINHANH4,HINHANH5")] CHITIETSP cHITIETSP)
        {
            if (ModelState.IsValid)
            {
                dbo.Entry(cHITIETSP).State = EntityState.Modified;
                dbo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP", cHITIETSP.MASANPHAM);
            return View(cHITIETSP);
        }

        // GET: CHITIETSPs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "SANPHAMs");
            }
            CHITIETSP cHITIETSP = dbo.CHITIETSPs.Find(id);
            if (cHITIETSP == null)
            {
                return RedirectToAction("Index", "SANPHAMs");
            }
            return View(cHITIETSP);
        }

        // POST: CHITIETSPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETSP cHITIETSP = dbo.CHITIETSPs.Find(id);
            dbo.CHITIETSPs.Remove(cHITIETSP);
            dbo.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
