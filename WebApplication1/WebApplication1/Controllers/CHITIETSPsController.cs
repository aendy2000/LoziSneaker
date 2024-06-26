﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        // GET: CHITIETSPs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Details");
            }
            CHITIETSP cHITIETSP = dbo.CHITIETSPs.Find(id);
            if (cHITIETSP == null)
            {
                return RedirectToAction("Details");
            }
            ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", cHITIETSP.MASANPHAM);
            return View(cHITIETSP);
        }

        // POST: CHITIETSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CHITIETSP cHITIETSP)
        {
            kiemtratruongnhap(cHITIETSP);
            if (ModelState.IsValid)
            {
                dbo.Entry(cHITIETSP).State = EntityState.Modified;
                dbo.SaveChanges();
                return RedirectToAction("Details");
            }
                    
            ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP", cHITIETSP.MASANPHAM);
            return View(cHITIETSP);

        }
        public void kiemtratruongnhap(CHITIETSP cHITIETSP)
        {
            if (cHITIETSP.SL_TONG < 0 || cHITIETSP.SL_SIZE36 < 0 || cHITIETSP.SL_SIZE37 < 0 || cHITIETSP.SL_SIZE38 < 0 || cHITIETSP.SL_SIZE39 < 0 || 
                cHITIETSP.SL_SIZE40 <0 || cHITIETSP.SL_SIZE41 <0|| cHITIETSP.SL_SIZE42 < 0 || cHITIETSP.SL_SIZE43 <0)
            {
                ModelState.AddModelError("","Tổng số lượng và các size nhập sai định dạng");
            } 
            else if (cHITIETSP.SL_TONG != (cHITIETSP.SL_SIZE36 + cHITIETSP.SL_SIZE37 + cHITIETSP.SL_SIZE38 + cHITIETSP.SL_SIZE39 + cHITIETSP.SL_SIZE40 + cHITIETSP.SL_SIZE41 + cHITIETSP.SL_SIZE42 + cHITIETSP.SL_SIZE43))
            {
                ModelState.AddModelError("", "Tổng số lượng với các size không trùng khớp");
            }
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
