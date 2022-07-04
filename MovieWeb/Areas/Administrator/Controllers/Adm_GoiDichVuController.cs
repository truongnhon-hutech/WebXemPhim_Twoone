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
    public class Adm_GoiDichVuController : Controller
    {
        private MovieWebContext db = new MovieWebContext();

        // GET: Administrator/Adm_GoiDichVu
        public ActionResult Index()
        {
            return View(db.GoiDichVus.ToList());
        }

        // GET: Administrator/Adm_GoiDichVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiDichVu goiDichVu = db.GoiDichVus.Find(id);
            if (goiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(goiDichVu);
        }

        // GET: Administrator/Adm_GoiDichVu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Adm_GoiDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGoiDV,TenGoiDV,GiaGoiDV,MoTaGoiDichVu")] GoiDichVu goiDichVu)
        {
            if (ModelState.IsValid)
            {
                db.GoiDichVus.Add(goiDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goiDichVu);
        }

        // GET: Administrator/Adm_GoiDichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiDichVu goiDichVu = db.GoiDichVus.Find(id);
            if (goiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(goiDichVu);
        }

        // POST: Administrator/Adm_GoiDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGoiDV,TenGoiDV,GiaGoiDV,MoTaGoiDichVu")] GoiDichVu goiDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goiDichVu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goiDichVu);
        }

        // GET: Administrator/Adm_GoiDichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GoiDichVu goiDichVu = db.GoiDichVus.Find(id);
            if (goiDichVu == null)
            {
                return HttpNotFound();
            }
            return View(goiDichVu);
        }

        // POST: Administrator/Adm_GoiDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GoiDichVu goiDichVu = db.GoiDichVus.Find(id);
            db.GoiDichVus.Remove(goiDichVu);
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
