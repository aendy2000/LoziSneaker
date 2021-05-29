using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TTCANHANsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: TTCANHANs
        public ActionResult Index()
        {
            return View(db.TTCANHANs.ToList());
        }

        public ActionResult Index2(string id)
        {
            var user = db.TTCANHANs.Find(id);

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index2([Bind(Include = "EMAIL,TEN,NGAYSINH,DIACHI,GIOITINH,SDT,id")] TTCANHAN tTCANHAN, AspNetUser aspNetUser)
        {
            checktextbox(tTCANHAN);
            if (ModelState.IsValid)
            {
                db.Entry(tTCANHAN).State = EntityState.Modified;
                var user = db.AspNetUsers.Find(tTCANHAN.id);
                user.Email = tTCANHAN.EMAIL;
                user.PhoneNumber = tTCANHAN.SDT;
                db.SaveChanges();
                return RedirectToAction("Index2");
            }
            return View(tTCANHAN);
        }
            // GET: TTCANHANs/Details/5
            public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TTCANHAN tTCANHAN = db.TTCANHANs.Find(id);
            if (tTCANHAN == null)
            {
                return HttpNotFound();
            }
            return View(tTCANHAN);
        }
        // GET: TTCANHANs/Edit/5
        public ActionResult Edit(string id)
        {
            var ttcanhan = db.TTCANHANs.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(ttcanhan);
        }

        // POST: TTCANHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMAIL,TEN,NGAYSINH,DIACHI,GIOITINH,SDT,id")] TTCANHAN tTCANHAN, AspNetUser aspNetUser)
        {
            checktextbox(tTCANHAN);
            if (ModelState.IsValid)
            {
                var user= db.AspNetUsers.Find(tTCANHAN.id);
                user.Email = tTCANHAN.EMAIL;
                user.PhoneNumber = tTCANHAN.SDT;
                db.Entry(tTCANHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tTCANHAN);
        }
        public void checktextbox(TTCANHAN tTCANHAN)
        {
            var regexItem = new Regex("^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ ]*$");
            var diachi = new Regex("^[aAàÀảẢãÃáÁạẠăĂằẰẳẲẵẴắẮặẶâÂầẦẩẨẫẪấẤậẬbBcCdDđĐeEèÈẻẺẽẼéÉẹẸêÊềỀểỂễỄếẾệỆfFgGhHiIìÌỉỈĩĨíÍịỊjJkKlLmMnNoOòÒỏỎõÕóÓọỌôÔồỒổỔỗỖốỐộỘơƠờỜởỞỡỠớỚợỢpPqQrRsStTuUùÙủỦũŨúÚụỤưƯừỪửỬữỮứỨựỰvVwWxXyYỳỲỷỶỹỸýÝỵỴzZ0-9 ]*$");
            var sdt = new Regex("^[0-9]*$");
            if (tTCANHAN.TEN == null || tTCANHAN.SDT == null || tTCANHAN.DIACHI == null || tTCANHAN.SDT == null || tTCANHAN.GIOITINH == null || tTCANHAN.NGAYSINH == null || tTCANHAN.EMAIL== null)
            {
                ModelState.AddModelError("", "Thông tin chưa nhập đầy đủ");
            }
            else if (regexItem.IsMatch(tTCANHAN.TEN) == false)
            {
                ModelState.AddModelError("","Tên có chứa số và kí tự đặc biệt");
            }
            else if(tTCANHAN.TEN.Trim().Length ==0)
            {
                ModelState.AddModelError("", "Tên chỉ chứa khoảng trắng");
            }else if (regexItem.IsMatch(tTCANHAN.GIOITINH) == false)
            {
                ModelState.AddModelError("", "Giới tính có chứa số và kí tự đặc biệt");
            }else if (tTCANHAN.GIOITINH.Trim().Length==0)
            {
                ModelState.AddModelError("","Giới tính chỉ chứa khoảng trắng");
            }
            else if (diachi.IsMatch(tTCANHAN.DIACHI)== false)
            {
                ModelState.AddModelError("","Địa chỉ có chứa ký tụ đặc biệt");
            }else if (tTCANHAN.DIACHI.Trim().Length == 0)
            {
                ModelState.AddModelError("","Địa chỉ chứa khoảng trắng");
            }
            else if (tTCANHAN.SDT.Trim().Length == 0)
            {
                ModelState.AddModelError("","Số điện thoại chỉ chứa khoảng trắng");
            }
            else if (sdt.IsMatch(tTCANHAN.SDT) == false)
            {
                ModelState.AddModelError("","Số diện thoại có chứa chữ và ký tự đặc biệt");
            }
            else if(tTCANHAN.SDT.Length <10 || tTCANHAN.SDT.Length > 10)
            {
                ModelState.AddModelError("","Số điện thoại không phù hợp");
            }
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