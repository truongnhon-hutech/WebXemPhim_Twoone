using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;
using PagedList;

namespace MovieWeb.Areas.Administrator.Controllers
{
    public class Adm_DatNuocController : Controller
    {
        private MovieWebContext db = new MovieWebContext();

        // GET: Administrator/Adm_DatNuoc
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Adm_TrangChu");
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return View(db.DatNuocs.OrderBy(dn => dn.MaNuoc).ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrator/Adm_DatNuoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatNuoc datNuoc = db.DatNuocs.Find(id);
            if (datNuoc == null)
            {
                return HttpNotFound();
            }
            return View(datNuoc);
        }

        // GET: Administrator/Adm_DatNuoc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Adm_DatNuoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNuoc,TenNuoc")] DatNuoc datNuoc)
        {
            if (ModelState.IsValid)
            {
                db.DatNuocs.Add(datNuoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datNuoc);
        }

        // GET: Administrator/Adm_DatNuoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatNuoc datNuoc = db.DatNuocs.Find(id);
            if (datNuoc == null)
            {
                return HttpNotFound();
            }
            return View(datNuoc);
        }

        // POST: Administrator/Adm_DatNuoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNuoc,TenNuoc")] DatNuoc datNuoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datNuoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(datNuoc);
        }

        // GET: Administrator/Adm_DatNuoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatNuoc datNuoc = db.DatNuocs.Find(id);
            if (datNuoc == null)
            {
                return HttpNotFound();
            }
            return View(datNuoc);
        }

        // POST: Administrator/Adm_DatNuoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DatNuoc datNuoc = db.DatNuocs.Find(id);
            db.DatNuocs.Remove(datNuoc);
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
