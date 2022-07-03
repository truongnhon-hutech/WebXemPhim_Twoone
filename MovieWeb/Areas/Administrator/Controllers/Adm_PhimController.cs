using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using MovieWeb.Models;
using PagedList;


namespace MovieWeb.Areas.Administrator.Controllers
{
    public class Adm_PhimController : Controller
    {
        private MovieWebContext db = new MovieWebContext();

        // GET: Phims
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Adm_TrangChu");
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.IMDBSortParm = sortOrder == "IMDB" ? "IMDB_desc" : "IMDB";
            ViewBag.ViewSortParm = sortOrder == "View" ? "View_desc" : "View";
            ViewBag.TimeSortParm = sortOrder == "Time" ? "Time_desc" : "Time";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var phims = db.Phims.Include(p => p.DatNuoc);
            if (!String.IsNullOrEmpty(searchString))
            {
                phims = phims.Where(s => s.TenPhim.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    phims = phims.OrderByDescending(s => s.TenPhim);
                    break;
                case "Date":
                    phims = phims.OrderBy(s => s.NgaySanXuat);
                    break;
                case "date_desc":
                    phims = phims.OrderByDescending(s => s.NgaySanXuat);
                    break;
                case "IMDB":
                    phims = phims.OrderBy(s => s.DiemIMDB);
                    break;
                case "IMDB_desc":
                    phims = phims.OrderByDescending(s => s.DiemIMDB);
                    break;
                case "View":
                    phims = phims.OrderBy(s => s.LuotXem);
                    break;
                case "View_desc":
                    phims = phims.OrderByDescending(s => s.LuotXem);
                    break;
                case "Time":
                    phims = phims.OrderBy(s => s.ThoiLuong);
                    break;
                case "Time_desc":
                    phims = phims.OrderByDescending(s => s.ThoiLuong);
                    break;
                default:
                    phims = phims.OrderBy(s => s.TenPhim);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phims.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ExportToExcel()
        {
            ViewBag.K = "tai";
            var products = new System.Data.DataTable();
            var phims = db.Phims.ToList();
            products.Columns.Add("Phim");
            products.Columns.Add("Ngày sản xuất");
            products.Columns.Add("Tóm tắt nội dung");
            products.Columns.Add("Điểm IMDB");
            products.Columns.Add("Lượt xem");
            products.Columns.Add("Thời lượng");
            products.Columns.Add("Banner");

            foreach (var item in phims)
            {
                products.Rows.Add(item.TenPhim,item.NgaySanXuat,item.NoiDungPhimTomTat,item.DiemIMDB,item.LuotXem,item.ThoiLuong,item.HinhBanner);
            }
         

            var grid = new GridView();
            grid.DataSource = products;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Phim.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write('\uFEFF'+sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Index", "Adm_Phim");
        }
        // GET: Phims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // GET: Phims/Create
        public ActionResult Create()
        {
            ViewBag.MaNuoc = new SelectList(db.DatNuocs, "MaNuoc", "TenNuoc");
            ViewBag.MaNgonNgu = new SelectList(db.NgonNgus, "MaNgonNgu", "TenNgonNgu");
            return View();
        }

        // POST: Phims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhim,TenPhim,NgaySanXuat,NoiDungPhim,NoiDungPhimTomTat,DiemIMDB,LuotXem,ThoiLuong,HinhBanner,LinkPhim,LinkPhimVip,TrailerPhim,MaNuoc,MaNgonNgu")] Phim phim, HttpPostedFileBase fileupload)
        {
            if (fileupload != null)
            {
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/MoviePosters"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileupload.SaveAs(path);
                }
                phim.HinhBanner = fileName;
            }
            if (ModelState.IsValid)
            {
                db.Phims.Add(phim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNuoc = new SelectList(db.DatNuocs, "MaNuoc", "TenNuoc", phim.MaNuoc);
            ViewBag.MaNgonNgu = new SelectList(db.NgonNgus, "MaNgonNgu", "TenNgonNgu", phim.MaNgonNgu);
            return View(phim);
        }

        // GET: Phims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.DienViens = db.DienViens.ToList();
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTheLoai = new SelectList(db.TheLoais, "MaTheLoai", "TenTheLoai", phim.TheLoais);
            ViewBag.MaNuoc = new SelectList(db.DatNuocs, "MaNuoc", "TenNuoc", phim.MaNuoc);
            ViewBag.MaNgonNgu = new SelectList(db.NgonNgus, "MaNgonNgu", "TenNgonNgu", phim.MaNgonNgu);
            ViewBag.MaDienVien = new SelectList(db.DienViens, "MaDienVien", "TenDienVien", phim.DienVienDongPhims);
            ViewBag.MaDaoDien = new SelectList(db.DaoDiens,"MaDaoDien","TenDaoDien",phim.DaoDiens);
            ViewBag.MaHashTag = new SelectList(db.HashTags, "MaHashTag", "TenTag", phim.HashTags);
          /*  ViewBag.MaDienVien = db.DienViens.ToList();*/
            return View(phim);
        }

        // POST: Phims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaPhim,TenPhim,NgaySanXuat,NoiDungPhim,NoiDungPhimTomTat,DiemIMDB,LuotXem,ThoiLuong,HinhBanner,LinkPhim,LinkPhimVip,TrailerPhim,MaNuoc,MaNgonNgu")] Phim phim,int? MaDienVien,int? MaDaoDien,int? MaHashTag,int? MaTheLoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phim).State = EntityState.Modified;
                if(MaDienVien!= null)
                {
                    int maDienVien = (int)MaDienVien;
                    var k = phim.DienVienDongPhims.Any(x => x.MaDienVien == maDienVien);
                    if (!k)
                    {
                        DienVienDongPhim dienVien = new DienVienDongPhim();
                        dienVien.MaDienVien = maDienVien;
                        dienVien.MaPhim = phim.MaPhim;
                        db.DienVienDongPhims.Add(dienVien);
                    }
                }
                if(MaDaoDien!= null)
                {
                    int maDaoDien = (int)MaDaoDien;
                    DaoDien daoDien = db.DaoDiens.FirstOrDefault(x => x.MaDaoDien == maDaoDien);
                    phim.DaoDiens.Add(daoDien);
                    daoDien.Phims.Add(phim);
                }
                if(MaHashTag != null)
                {
                    int maHashTag = (int)MaHashTag;
                    HashTag hashTag = db.HashTags.FirstOrDefault(x=> x.MaHashTag == maHashTag);
                    phim.HashTags.Add(hashTag);
                    hashTag.Phims.Add(phim);
                }
                if(MaTheLoai != null)
                {
                    int maTheLoai = (int)MaTheLoai;
                    TheLoai theLoai = db.TheLoais.FirstOrDefault(x => x.MaTheLoai == maTheLoai);
                    phim.TheLoais.Add(theLoai);
                    theLoai.Phims.Add(phim);
                }
                db.SaveChanges();
                return RedirectToAction("Edit");

            }
            ViewBag.MaNuoc = new SelectList(db.DatNuocs, "MaNuoc", "TenNuoc", phim.MaNuoc);
            ViewBag.MaNgonNgu = new SelectList(db.NgonNgus, "MaNgonNgu", "TenNgonNgu", phim.MaNgonNgu);
            return View(phim);
        }

        // GET: Phims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = db.Phims.Find(id);
            if (phim == null)
            {
                return HttpNotFound();
            }
            return View(phim);
        }

        // POST: Phims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phim phim = db.Phims.Find(id);
            db.Phims.Remove(phim);
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