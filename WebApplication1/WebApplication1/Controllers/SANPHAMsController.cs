﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SANPHAMsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: SANPHAMs
        [Authorize(Roles = "Admin,Nhân viên")]
        public ActionResult Index()
        {
            return View(db.SANPHAMs.ToList());
        }
        [AllowAnonymous]
        public ActionResult Index2()
        {
            var model = db.SANPHAMs.ToList();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Search(string keyword)
        {
            var model = db.SANPHAMs.ToList();
            model = model.Where(S => S.TENSP.ToLower().Contains(keyword)).ToList();
            ViewBag.Keywork = keyword;
            return View("Index2", model);

        }

        [AllowAnonymous]
        public ActionResult Productdetails(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index2");
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return RedirectToAction("Index2");
            }
            return View(sANPHAM);
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
            SANPHAM sp = new SANPHAM();
            return View(sp);
        }

        // POST: SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SANPHAM sANPHAM)
        {
            if (sANPHAM.ImageUpload != null)
            {
                string filename = Path.GetFileNameWithoutExtension(sANPHAM.ImageUpload.FileName).ToString();
                string extension = Path.GetExtension(sANPHAM.ImageUpload.FileName);
                filename = filename + extension;
                sANPHAM.HINHANH = "~/img/imgProduct/" + filename;
                sANPHAM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename));
            }

            KiemtraTruongNhạp(sANPHAM);

            if (ModelState.IsValid)
            {
                var findid = db.SANPHAMs.FirstOrDefault(m => m.MASP == sANPHAM.MASP);
                var sp = db.SANPHAMs.Find(sANPHAM.MASP);
                if (findid !=null) 
                {
                    ModelState.AddModelError("","Mã sản phẩm đã tồn tại");
                }
                else
                {
                    db.SANPHAMs.Add(sANPHAM);
                    db.SaveChanges();

                    CHITIETSP addMaSP = new CHITIETSP();
                    addMaSP.MASANPHAM = sANPHAM.MASP.ToString();
                    addMaSP.SL_TONG = 0;
                    addMaSP.SL_SIZE36 = 0;
                    addMaSP.SL_SIZE37 = 0;
                    addMaSP.SL_SIZE38 = 0;
                    addMaSP.SL_SIZE39 = 0;
                    addMaSP.SL_SIZE40 = 0;
                    addMaSP.SL_SIZE41 = 0;
                    addMaSP.SL_SIZE42 = 0;
                    addMaSP.SL_SIZE43 = 0;

                    db.CHITIETSPs.Add(addMaSP);
                    db.SaveChanges();
                return RedirectToAction("Index");
                }
            }
            return View(sANPHAM);
        }
        private void KiemtraTruongNhạp(SANPHAM sanpham)
        {
            var massp = new Regex("^[a-zA-Z0-9]*$");
            if (sanpham.MASP== null)
            {
                ModelState.AddModelError("MASP","Mã sản phẩm không được bỏ trống !!");
            }
            else if (massp.IsMatch(sanpham.MASP) == false)
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm sai định dạng");
            }
            else if (sanpham.MASP.Length < 5 || sanpham.MASP.Length > 20)
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm không hợp lệ !!");
            }
            else if (massp.IsMatch(sanpham.MASP) == false)
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm sai định dạng");
            }
            else if (sanpham.MASP.Trim().Length == 0)
            {
                ModelState.AddModelError("MASP", "Mã sản phẩm không hợp lệ !!");
            }
            else if (sanpham.TENSP == null)
            {
                ModelState.AddModelError("TENSP", "Tên sản phẩm không được bỏ trống !!");
            }
            else if (sanpham.TENSP.Trim().Length == 0)
            {
                ModelState.AddModelError("TENSP", "Tên sản phẩm không hợp lệ!!");
            }
            else if (sanpham.THUONGHIEU == null)
            {
                ModelState.AddModelError("THUONGHIEU", "Thương hiệu sản phẩm không được bỏ trống !!");
            }
            else if (sanpham.THUONGHIEU.Trim().Length == 0)
            {
                ModelState.AddModelError("THUONGHIEU", "THương hiệu sản phẩm không hợp lệ!! ");
            }
            else if (sanpham.GIA == null)
            {
                ModelState.AddModelError("GIA", "Giá sản phẩm không được bỏ trống !!");
            }
            else if (sanpham.GIA < 1)
            {
                ModelState.AddModelError("Gia", "Giá không thể dưới 1 VNĐ !!");
            }
            else if (sanpham.HINHANH == null)
            {
                ModelState.AddModelError("HINHANH", "Hình ảnh sản phẩm không được bỏ trống !!");
            }
            else if (sanpham.MAUSAC == null)
            {
                ModelState.AddModelError("MAUSAC", "Màu sản phẩm không được bỏ trống !!");
            }
            else if (sanpham.MAUSAC.Trim().Length == 0)
            {
                ModelState.AddModelError("MAUSAC", "Màu sản phẩm không được bỏ trống !!");
            }
            else if (sanpham.NGAYTHEM == null)
            {
                ModelState.AddModelError("NGAYTHEM", "Ngày thêm sản phẩm không được bỏ trống !!");
            }

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
        public ActionResult Edit(SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                if (sANPHAM.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(sANPHAM.ImageUpload.FileName).ToString();
                    string extension = Path.GetExtension(sANPHAM.ImageUpload.FileName);
                    filename = filename + extension;
                    sANPHAM.HINHANH = "~/img/imgProduct/" + filename;
                    sANPHAM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/img/imgProduct/"), filename));
                }
                KiemtraTruongNhạp(sANPHAM);
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
            SANPHAM sANPHAM = db.SANPHAMs.Find(id); ;
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
            var XoaSP = db.SANPHAMs.Find(id);
            var XoaTSSP = db.CHITIETSPs.Find(id);
            db.CHITIETSPs.Remove(XoaTSSP);
            db.SANPHAMs.Remove(XoaSP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void Size(string size)
        {
            ViewBag.Size = size;
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
