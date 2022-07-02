using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Areas.Administrator.Controllers
{
    public class Adm_NgonNguController : Controller
    {
        private MovieWebContext db = new MovieWebContext();

        // GET: Administrator/Adm_NgonNgu
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Adm_TrangChu");
            return View(db.NgonNgus.ToList());
        }

        // GET: Administrator/Adm_NgonNgu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NgonNgu ngonNgu = db.NgonNgus.Find(id);
            if (ngonNgu == null)
            {
                return HttpNotFound();
            }
            return View(ngonNgu);
        }

        // GET: Administrator/Adm_NgonNgu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Adm_NgonNgu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNgonNgu,TenNgonNgu")] NgonNgu ngonNgu)
        {
            if (ModelState.IsValid)
            {
                db.NgonNgus.Add(ngonNgu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ngonNgu);
        }

        // GET: Administrator/Adm_NgonNgu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NgonNgu ngonNgu = db.NgonNgus.Find(id);
            if (ngonNgu == null)
            {
                return HttpNotFound();
            }
            return View(ngonNgu);
        }

        // POST: Administrator/Adm_NgonNgu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNgonNgu,TenNgonNgu")] NgonNgu ngonNgu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ngonNgu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ngonNgu);
        }

        // GET: Administrator/Adm_NgonNgu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NgonNgu ngonNgu = db.NgonNgus.Find(id);
            if (ngonNgu == null)
            {
                return HttpNotFound();
            }
            return View(ngonNgu);
        }

        // POST: Administrator/Adm_NgonNgu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NgonNgu ngonNgu = db.NgonNgus.Find(id);
            db.NgonNgus.Remove(ngonNgu);
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
