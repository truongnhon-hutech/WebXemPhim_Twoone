using MovieWeb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MovieWeb.Controllers
{
    [HandleError]
    public class MovieController : Controller
    {
        // san pham

        // Trang chu
        public ActionResult Home()
        {
            MovieWebContext data = new MovieWebContext();
            return View(data.Phims);
        }
        private List<Phim> TopPhimNhieuView(int count)
        {
            MovieWebContext data = new MovieWebContext();
            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Phims.OrderByDescending(a => a.LuotXem).Take(count).ToList();
        }
        public ActionResult SlideTopView()
        {
            var phim = TopPhimNhieuView(10);
            return PartialView(phim);
        }
        //Phuong thức Index: Không có tham số (null) hoặc có tham số là số nguyên (biến page)
        private List<Phim> PhimMoi(int count)
        {
            MovieWebContext data = new MovieWebContext();

            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Phims.OrderByDescending(a => a.NgaySanXuat).Take(count).ToList();
        }
        public ActionResult SlideMovieNew()
        {
            var phim = PhimMoi(10);
            return PartialView(phim);
        }
        private List<Phim> PhimMoiSlide(int count)
        {
            MovieWebContext data = new MovieWebContext();

            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Phims.OrderByDescending(a => a.NgaySanXuat).Take(count).ToList();
        }
        public ActionResult TC_TTPhimTDV(int? id)
        {
            MovieWebContext db = new MovieWebContext();
            var phim = db.Phims.FirstOrDefault(x => x.MaPhim == id);
            return PartialView(phim.DienVienDongPhims);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
            return View();
        }
        // Menu
        public ActionResult Genre()
        {
            MovieWebContext data = new MovieWebContext();
            var theloai = data.TheLoais.Take(7).ToList();
            return PartialView(theloai);
        }
        public ActionResult Country()
        {
            MovieWebContext data = new MovieWebContext();
            var quocgia = data.DatNuocs.Take(7).ToList();
            return PartialView(quocgia);
        }
        public ActionResult Language()
        {
            MovieWebContext data = new MovieWebContext();
            var ngonngu = data.NgonNgus.Take(7).ToList();
            return PartialView(ngonngu);
        }
        //Sortphim



        //Chi tiết phim
        public ActionResult MovieDetail(int id)
        {
            MovieWebContext data = new MovieWebContext();

            var phim = data.Phims.SingleOrDefault(n => n.MaPhim == id);
            if (phim == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phim);
        }
        public ActionResult TTDienVienTP(int? id)
        {
            MovieWebContext db = new MovieWebContext();
            var phim = db.Phims.FirstOrDefault(x => x.MaPhim == id);

            return PartialView(phim.DienVienDongPhims);
        }
        public ActionResult TTPhimTDV(int? id)
        {
            MovieWebContext db = new MovieWebContext();
            ViewBag.FoundNum = db.Phims.Count();

            var DienVien = db.DienViens.FirstOrDefault(x => x.MaDienVien == id);
            return PartialView(DienVien.DienVienDongPhims);
        }

        public ActionResult WatchingMovie(int? id)
        {
            MovieWebContext data = new MovieWebContext();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim phim = data.Phims.FirstOrDefault(x => x.MaPhim == id);
            if (phim == null)
            {
                return HttpNotFound();
            }

            return View(phim);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult WatchingMovie(FormCollection form, int maPhim)
        {
            MovieWebContext data = new MovieWebContext();
            var noiDung = form["NoiDungBinhLuan"];
            var soSao = Convert.ToInt32(form["SoSao"]);
            var tenTaiKhoan = Session["TaiKhoan"];
            var thoiGianBinhLuan = DateTime.Now;
            var nguoiDung = data.NguoiDungs.FirstOrDefault(nd => nd.TaiKhoan == tenTaiKhoan.ToString());
            if (soSao == 0)
            {
                ViewData["Loi"] = "Bạn chưa đánh giá!";
            }
            else
            {
                DanhGiaPhim danhGia = new DanhGiaPhim();
                danhGia.MaPhim = maPhim;
                danhGia.MaNguoiDung = nguoiDung.MaNguoiDung;
                danhGia.ThoiGianBinhLuan = thoiGianBinhLuan;
                danhGia.NoiDungBinhLuan = noiDung;
                danhGia.SoSao = soSao;
                data.DanhGiaPhims.Add(danhGia);
                data.SaveChanges();
                return WatchingMovie(maPhim);
            }
            return WatchingMovie(maPhim);
        }

        public ActionResult Comment(int? movieid, int? page)
        {
            MovieWebContext data = new MovieWebContext();

            var ratingList = data.DanhGiaPhims.Where(n => n.MaPhim == movieid).ToList().OrderByDescending(a => a.ThoiGianBinhLuan).Take(100);
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return PartialView(ratingList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SimilarMovie(int? movieid)
        {
            MovieWebContext data = new MovieWebContext();
            var relateList = data.Phims.Where(n => n.MaPhim > movieid || n.MaPhim < movieid).ToList().Take(3);
            return PartialView(relateList);
        }
        public ActionResult SearchByFilter(string Ten, int? MaTheLoai, int? MaNuoc, int? MaNgonNgu, int? dIMDB, int? From, int? To, int? page, bool? Grid, string sortOrder)
        {
            MovieWebContext data = new MovieWebContext();
            ViewBag.MaTheLoai = new SelectList(data.TheLoais, "MaTheLoai", "TenTheLoai");
            ViewBag.MaNuoc = new SelectList(data.DatNuocs, "MaNuoc", "TenNuoc");
            ViewBag.MaNgonNgu = new SelectList(data.NgonNgus, "MaNgonNgu", "TenNgonNgu");
            int pageNumber = (page ?? 1);

            var phims = from x in data.Phims select x;
            ViewBag.Ten = Ten;
            ViewBag.TheLoai = MaTheLoai;
            ViewBag.DatNuoc = MaNuoc;
            ViewBag.NgonNgu = MaNgonNgu;
            ViewBag.DIMDB = dIMDB;
            ViewBag.From = From;
            ViewBag.To = To;
            Grid = (Grid ?? true);
            ViewBag.Grid = Grid;
            ViewBag.Order = sortOrder;
            if (!String.IsNullOrEmpty(Ten))
            {
                phims = from x in phims where x.TenPhim.Contains(Ten) select x;
            }
            if (MaNuoc.HasValue)
            {
                phims = from x in phims where x.MaNuoc == MaNuoc select x;
            }

            if (dIMDB.HasValue)
            {
                phims = from x in phims where x.DiemIMDB >= dIMDB && x.DiemIMDB <= (dIMDB + 1) select x;
            }
            if (MaNgonNgu.HasValue)
            {
                phims = from x in phims where x.MaNgonNgu == MaNgonNgu select x;
            }
            if (From.HasValue)
            {
                if (!To.HasValue)
                {
                    To = DateTime.Now.Year;
                }
                else
                {
                    if (To < From) To = DateTime.Now.Year;
                }

                phims = from x in phims where x.NgaySanXuat != null && ((DateTime)x.NgaySanXuat).Year >= From && ((DateTime)x.NgaySanXuat).Year <= To select x;
            }

            if (MaTheLoai.HasValue)
            {
                phims = from x in phims where x.TheLoais.Any(k => k.MaTheLoai == MaTheLoai) select x;
            }
            ViewBag.SL = phims.Count();
            switch (sortOrder)
            {
                case "name_desc":
                    phims = phims.OrderByDescending(s => s.TenPhim);
                    break;
                case "date":
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
                default:
                    phims = phims.OrderBy(s => s.TenPhim);
                    break;
            }
            
            return View(phims.ToPagedList(pageNumber, 16));

        }

        [HttpGet]
        public ActionResult Filter()
        {
            MovieWebContext data = new MovieWebContext();
            dynamic model = new ExpandoObject();
            model.TheLoais = data.TheLoais.ToList();
            model.DatNuocs = data.DatNuocs.ToList();
            model.NgonNgus = data.NgonNgus.ToList();
            return View(model);
        }
  
        // Chi tiết Actor
        public ActionResult ActorDetail(int id)
        {
            MovieWebContext data = new MovieWebContext();

            var dienVien = data.DienViens.SingleOrDefault(n => n.MaDienVien == id);
            if (dienVien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dienVien);
        }
        public ActionResult PageNotFound()
        {
            return View(); 
        }
        public ActionResult ActorGrid(int? page)
        {

            MovieWebContext data = new MovieWebContext();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            ViewBag.FoundNum = data.DienViens.Count();
            return View((PagedList<DienVien>)data.DienViens.OrderByDescending(a => a.TenDienVien).ToPagedList(pageNumber, pageSize));
        }
    }
}