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
        public ActionResult Edit(AspNetUser aspNetUser, string id)
        {
            if (ModelState.IsValid)
            {
                var user = db.AspNetUsers.Find(id);
                user.PhoneNumber = aspNetUser.PhoneNumber;
                user.UserName = aspNetUser.UserName;
                user.LockoutEnabled = aspNetUser.LockoutEnabled;
                user.LockoutEndDateUtc = aspNetUser.LockoutEndDateUtc;
                user.Email = aspNetUser.Email;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
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
            var usergh = db.GIOHANGs.Find(id);
            var count = db.CHITIETGIOHANGs.Where(m => m.MAGIOHG == usergh.MAGIOHANG);
            var iddh = db.DONHANGs.FirstOrDefault(m => m.TKKH == id);
            var dh = db.DONHANGs.Where(m => m.TKKH == id);
            if (idKH != null)
            {
                if (iddh != null)
                {
                    db.AspNetUsers.Remove(aspNetUser);
                    db.TTCANHANs.Remove(idKH);
                    db.CHITIETGIOHANGs.RemoveRange(count);
                    db.GIOHANGs.Remove(usergh);
                    db.GIOHANGs.Remove(usergh);
                    foreach (var dhid in dh)
                    {
                        var ctdh = db.CHITIETDHs.Where(m => m.MADH == dhid.MADH);
                        db.CHITIETDHs.RemoveRange(ctdh);
                    }
                    db.DONHANGs.RemoveRange(dh);
                    db.SaveChanges();
                }
                else
                {
                    db.AspNetUsers.Remove(aspNetUser);
                    db.TTCANHANs.Remove(idKH);
                    db.CHITIETGIOHANGs.RemoveRange(count);
                    db.GIOHANGs.Remove(usergh);
                    db.GIOHANGs.Remove(usergh);
                    db.SaveChanges();
                }
            }
            else
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
