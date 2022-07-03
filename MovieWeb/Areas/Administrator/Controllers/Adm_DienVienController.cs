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
    public class Adm_DienVienController : Controller
    {
        private MovieWebContext db = new MovieWebContext();

        // GET: Administrator/Adm_DienVien
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Adm_TrangChu");
            var dienvien = db.DienViens.Include(p => p.DienVienDongPhims);
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
                dienvien = dienvien.Where(s => s.TenDienVien.Contains(searchString));
            }
            int pageSize = 24;
            int pageNumber = (page ?? 1);
            return View(dienvien.OrderBy(s=>s.TenDienVien).ToPagedList(pageNumber, pageSize));
        }

        // GET: Administrator/Adm_DienVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DienVien dienVien = db.DienViens.Find(id);
            if (dienVien == null)
            {
                return HttpNotFound();
            }
            return View(dienVien);
        }

        // GET: Administrator/Adm_DienVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Adm_DienVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDienVien,TenDienVien,HinhAnhDienVien")] DienVien dienVien, HttpPostedFileBase fileupload)
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
                dienVien.HinhAnhDienVien = fileName;
            }
            if (ModelState.IsValid)
            {
                db.DienViens.Add(dienVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dienVien);
        }

        // GET: Administrator/Adm_DienVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DienVien dienVien = db.DienViens.Find(id);
            if (dienVien == null)
            {
                return HttpNotFound();
            }
            return View(dienVien);
        }

        // POST: Administrator/Adm_DienVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDienVien,TenDienVien,HinhAnhDienVien")] DienVien dienVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dienVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dienVien);
        }

        // GET: Administrator/Adm_DienVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DienVien dienVien = db.DienViens.Find(id);
            if (dienVien == null)
            {
                return HttpNotFound();
            }
            return View(dienVien);
        }

        // POST: Administrator/Adm_DienVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DienVien dienVien = db.DienViens.Find(id);
            db.DienViens.Remove(dienVien);
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
