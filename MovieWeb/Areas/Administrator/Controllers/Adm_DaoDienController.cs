using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;
using PagedList;

namespace MovieWeb.Areas.Administrator.Controllers
{
    public class Adm_DaoDienController : Controller
    {
        private MovieWebContext db = new MovieWebContext();

        // GET: Administrator/Adm_DaoDien
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Adm_TrangChu");
            var daodien = db.DaoDiens.Include(p => p.Phims);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                daodien = daodien.Where(s => s.TenDaoDien.Contains(searchString));
            }
            int pageSize = 24;
            int pageNumber = (page ?? 1);
            return View(daodien.OrderBy(m => 1).ToPagedList(pageNumber, pageSize));
        }
        // GET: Administrator/Adm_DaoDien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoDien daoDien = db.DaoDiens.Find(id);
            if (daoDien == null)
            {
                return HttpNotFound();
            }
            return View(daoDien);
        }

        // GET: Administrator/Adm_DaoDien/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDaoDien,TenDaoDien,HinhAnhDaoDien")] DaoDien daoDien, HttpPostedFileBase fileupload)
        {
            if(fileupload != null)
            {
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/sample"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                daoDien.HinhAnhDaoDien = fileName;
            }
            if (ModelState.IsValid)
            {
                db.DaoDiens.Add(daoDien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daoDien);
        }

        // GET: Administrator/Adm_DaoDien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoDien daoDien = db.DaoDiens.Find(id);
            if (daoDien == null)
            {
                return HttpNotFound();
            }
            return View(daoDien);
        }

        // POST: Administrator/Adm_DaoDien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDaoDien,TenDaoDien,HinhAnhDaoDien")] DaoDien daoDien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(daoDien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(daoDien);
        }

        // GET: Administrator/Adm_DaoDien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DaoDien daoDien = db.DaoDiens.Find(id);
            if (daoDien == null)
            {
                return HttpNotFound();
            }
            return View(daoDien);
        }

        // POST: Administrator/Adm_DaoDien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DaoDien daoDien = db.DaoDiens.Find(id);
            db.DaoDiens.Remove(daoDien);
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
