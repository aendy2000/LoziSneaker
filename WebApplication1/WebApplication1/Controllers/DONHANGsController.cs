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

        public ActionResult Duyet(string id)
        {
            var donhang = db.DONHANGs.FirstOrDefault(c => c.MADH == id);
            donhang.TRANGTHAI = "Đã duyệt";
            db.SaveChanges();
            return RedirectToAction("Index","DONHANGs");
        }
        public ActionResult Huy(string id)
        {
            var donhang = db.DONHANGs.FirstOrDefault(c => c.MADH == id);
            donhang.TRANGTHAI = "Đã hủy";
            db.SaveChanges();

            var huydonhang = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            foreach(var item in huydonhang)
            {
                string size = item.SIZE;
                var pro = db.CHITIETSPs.FirstOrDefault(c => c.MASANPHAM == item.MASP);

                pro.SL_TONG = pro.SL_TONG.Value + item.SOLUONG.Value;
                if (size.Equals("36"))
                    pro.SL_SIZE36 = pro.SL_SIZE36.Value + item.SOLUONG.Value;
                else if (size.Equals("37"))
                    pro.SL_SIZE37 = pro.SL_SIZE37.Value + item.SOLUONG.Value;
                else if (size.Equals("38"))
                    pro.SL_SIZE38 = pro.SL_SIZE38.Value + item.SOLUONG.Value;
                else if (size.Equals("39"))
                    pro.SL_SIZE39 = pro.SL_SIZE39.Value + item.SOLUONG.Value;
                else if (size.Equals("40"))
                    pro.SL_SIZE40 = pro.SL_SIZE40.Value + item.SOLUONG.Value;
                else if (size.Equals("41"))
                    pro.SL_SIZE41 = pro.SL_SIZE41.Value + item.SOLUONG.Value;
                else if (size.Equals("42"))
                    pro.SL_SIZE42 = pro.SL_SIZE42.Value + item.SOLUONG.Value;
                else if (size.Equals("43"))
                    pro.SL_SIZE43 = pro.SL_SIZE43.Value + item.SOLUONG.Value;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "DONHANGs");
        }

        [AllowAnonymous]
        public ActionResult Huy2(string id)
        {
            var donhang = db.DONHANGs.FirstOrDefault(c => c.MADH == id);
            donhang.TRANGTHAI = "Đã hủy";
            db.SaveChanges();

            var huydonhang = db.CHITIETDHs.Where(c => c.MADH == id).ToList();
            foreach (var item in huydonhang)
            {
                string size = item.SIZE;
                var pro = db.CHITIETSPs.FirstOrDefault(c => c.MASANPHAM == item.MASP);

                pro.SL_TONG = pro.SL_TONG.Value + item.SOLUONG.Value;
                if (size.Equals("36"))
                    pro.SL_SIZE36 = pro.SL_SIZE36.Value + item.SOLUONG.Value;
                else if (size.Equals("37"))
                    pro.SL_SIZE37 = pro.SL_SIZE37.Value + item.SOLUONG.Value;
                else if (size.Equals("38"))
                    pro.SL_SIZE38 = pro.SL_SIZE38.Value + item.SOLUONG.Value;
                else if (size.Equals("39"))
                    pro.SL_SIZE39 = pro.SL_SIZE39.Value + item.SOLUONG.Value;
                else if (size.Equals("40"))
                    pro.SL_SIZE40 = pro.SL_SIZE40.Value + item.SOLUONG.Value;
                else if (size.Equals("41"))
                    pro.SL_SIZE41 = pro.SL_SIZE41.Value + item.SOLUONG.Value;
                else if (size.Equals("42"))
                    pro.SL_SIZE42 = pro.SL_SIZE42.Value + item.SOLUONG.Value;
                else if (size.Equals("43"))
                    pro.SL_SIZE43 = pro.SL_SIZE43.Value + item.SOLUONG.Value;
                db.SaveChanges();
            }

            return RedirectToAction("Index2", "DONHANGs");
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
