using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Text.RegularExpressions;

namespace WebApplication1.Controllers
{ 
    [Authorize (Roles ="Admin")]
    public class AspNetUsersController : Controller
    {
        
        private CT25Team13Entities db = new CT25Team13Entities();

        
        // GET: AspNetUsers
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AspNetUser aspNetUser, string id, TTCANHAN tTCANHAN)
        {
            checktextbox(aspNetUser);
            if (ModelState.IsValid)
            {
                var idKH = db.TTCANHANs.Find(id);
                if (idKH != null)
                {
                    var user = db.AspNetUsers.Find(id);
                    user.PhoneNumber = aspNetUser.PhoneNumber;
                    user.UserName = aspNetUser.UserName;
                    user.LockoutEnabled = aspNetUser.LockoutEnabled;
                    user.LockoutEndDateUtc = aspNetUser.LockoutEndDateUtc;
                    user.Email = aspNetUser.Email;
                    var updateprofile = db.TTCANHANs.Find(id);
                    updateprofile.EMAIL = aspNetUser.Email;
                    updateprofile.SDT = aspNetUser.PhoneNumber;
                    db.SaveChanges();
                }
                else
                {
                    var user = db.AspNetUsers.Find(id);
                    user.PhoneNumber = aspNetUser.PhoneNumber;
                    user.UserName = aspNetUser.UserName;
                    user.LockoutEnabled = aspNetUser.LockoutEnabled;
                    user.LockoutEndDateUtc = aspNetUser.LockoutEndDateUtc;
                    user.Email = aspNetUser.Email;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }
        public void checktextbox(AspNetUser aspNetUser)
        {
            Regex mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var Phone = new Regex("^[0-9]*$");
            if ( aspNetUser.PhoneNumber == null || aspNetUser.Email == null || aspNetUser.UserName == null)
            {
                ModelState.AddModelError("", "Thông tin chưa nhập đầy đủ");
            }
            else if (aspNetUser.Email.Trim().Length == 0)
            {
                ModelState.AddModelError("","Email chỉ chứa kí tự khoảng trắng");
            }
            else if (mail.IsMatch(aspNetUser.Email)== false)
            {
                ModelState.AddModelError("","Email không hợp lệ");
            }
            else if (aspNetUser.UserName.Trim().Length == 0)
            {
                ModelState.AddModelError("","Tên người dùng chỉ chứa kí tự khoảng trắng");
            }
            else if (aspNetUser.PhoneNumber.Trim().Length == 0)
            {
                ModelState.AddModelError("", "Số điện thoại chỉ chứa khoảng trắng");
            }
            else if (Phone.IsMatch(aspNetUser.PhoneNumber) == false)
            {
                ModelState.AddModelError("", "Số diện thoại có chứa chữ và ký tự đặc biệt");
            }
            else if (aspNetUser.PhoneNumber.Length < 10 || aspNetUser.PhoneNumber.Length > 10)
            {
                ModelState.AddModelError("", "Số điện thoại không phù hợp");
            }
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            var idKH = db.TTCANHANs.Find(id);
            if (idKH != null)
            {
                db.AspNetUsers.Remove(aspNetUser);
                db.TTCANHANs.Remove(idKH);
                db.SaveChanges();
            }else
            {
                db.AspNetUsers.Remove(aspNetUser);
                db.SaveChanges();
            }
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
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
