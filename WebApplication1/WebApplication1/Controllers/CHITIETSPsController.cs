using System;
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
            ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP", cHITIETSP.MASANPHAM);
            return View(cHITIETSP);
        }

        // POST: CHITIETSPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CHITIETSP cHITIETSP)
        {
            try
            {
                if(cHITIETSP.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(cHITIETSP.ImageUpload.FileName).ToString();
                    string extension = Path.GetExtension(cHITIETSP.ImageUpload.FileName);
                    filename = filename + extension;
                    cHITIETSP.HINHANH1 = "~/img/imgProduct/" + filename;
                    cHITIETSP.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename));
                }
                if (cHITIETSP.ImageUpload2 != null)
                {
                    string filename2 = Path.GetFileNameWithoutExtension(cHITIETSP.ImageUpload2.FileName).ToString();
                    string extension2 = Path.GetExtension(cHITIETSP.ImageUpload2.FileName);
                    filename2 = filename2 + extension2;
                    cHITIETSP.HINHANH2 = "~/img/imgProduct/" + filename2;
                    cHITIETSP.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename2));
                }
                if (cHITIETSP.ImageUpload3 != null)
                    {
                        string filename3 = Path.GetFileNameWithoutExtension(cHITIETSP.ImageUpload3.FileName).ToString();
                        string extension3 = Path.GetExtension(cHITIETSP.ImageUpload3.FileName);
                        filename3 = filename3 + extension3;
                        cHITIETSP.HINHANH3 = "~/img/imgProduct/" + filename3;
                        cHITIETSP.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename3));
                    }
                    if (cHITIETSP.ImageUpload4 != null)
                    {
                        string filename4 = Path.GetFileNameWithoutExtension(cHITIETSP.ImageUpload4.FileName).ToString();
                        string extension4 = Path.GetExtension(cHITIETSP.ImageUpload4.FileName);
                        filename4 = filename4 + extension4;
                        cHITIETSP.HINHANH4 = "~/img/imgProduct/" + filename4;
                        cHITIETSP.ImageUpload4.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename4));
                    }
                    if (cHITIETSP.ImageUpload5 != null)
                    {
                        string filename5 = Path.GetFileNameWithoutExtension(cHITIETSP.ImageUpload5.FileName).ToString();
                        string extension5 = Path.GetExtension(cHITIETSP.ImageUpload5.FileName);
                        filename5 = filename5 + extension5;
                        cHITIETSP.HINHANH5 = "~/img/imgProduct/" + filename5;
                        cHITIETSP.ImageUpload5.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename5));
                    }
                if (ModelState.IsValid)
                    {
                        dbo.Entry(cHITIETSP).State = EntityState.Modified;
                        dbo.SaveChanges();
                        return RedirectToAction("Details");
                    }
                    ViewBag.MASANPHAM = new SelectList(dbo.SANPHAMs, "MASP", "TENSP", cHITIETSP.MASANPHAM);
            }
            catch
            {
                return RedirectToAction("Details");
            }
            return View(cHITIETSP);

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
