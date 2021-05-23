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
    public class TTCANHANsController : Controller
    {
        private CT25Team13Entities db = new CT25Team13Entities();

        // GET: TTCANHANs
        public ActionResult Index()
        {
            return View(db.TTCANHANs.ToList());
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

        // POST: TTCANHANs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMAIL,TEN,NGAYSINH,DIACHI,GIOITINH,SDT,id")] TTCANHAN tTCANHAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tTCANHAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tTCANHAN);
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
