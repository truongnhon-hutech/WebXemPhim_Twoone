using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;
using PagedList;

namespace MovieWeb.Controllers
{
    public class UserController : Controller
    {
        
        private string Encode(string originalPassword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            Byte[] encodedBytes = md5.ComputeHash(originalBytes);

            return BitConverter.ToString(encodedBytes);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RECaptcha());
        }
        [HttpPost]
        public JsonResult AjaxMethod(string response)
        {
            RECaptcha recaptcha = new RECaptcha();
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + recaptcha.Secret + "&response=" + response;
            recaptcha.Response = (new WebClient()).DownloadString(url);
            return Json(recaptcha);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection collection, NguoiDung nd)
        {
            // Gán các giá tị người dùng nhập liệu cho các biến 
            MovieWebContext db = new MovieWebContext();
            var HoTen = collection["HoTen"];
            var GioiTinh = collection["GioiTinh"];
            var SDT = collection["SDT"];
            var TaiKhoan = collection["TaiKhoan"];
            var MatKhau = collection["MatKhau"];
            var NhapLaiMatKhau = collection["NhapLaiMatKhau"];
            var captcha = collection["captcha"];
            var Email = collection["Email"];
            var NgaySinh = collection["NgaySinh"];
            var trungMail = db.NguoiDungs.FirstOrDefault(x => x.Email == Email);
            var trungTenDN = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == TaiKhoan);
            if (String.IsNullOrEmpty(HoTen))
            {
                ViewData["Err1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(SDT))
            {
                ViewData["Err4"] = "Số điện thoại không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(GioiTinh))
            {
                ViewData["Err2"] = "Phải nhập giới tính";
            }
            else if (String.IsNullOrEmpty(NgaySinh))
            {
                ViewData["Err9"] = "Ngày sinh không hợp lệ";
            }
            else if (String.IsNullOrEmpty(Email))
            {
                ViewData["Err3"] = "Email không được bỏ trống";
            }
            else if (trungMail != null)
            {
                ViewData["Err3"] = "Email này đã tồn tại";
            }
            
            else if (String.IsNullOrEmpty(TaiKhoan))
            {
                ViewData["Err5"] = "Phải nhập tên đăng nhập";
            }
            else if (trungTenDN != null)
            {
                ViewData["Err5"] = "Tên đăng nhập này đã tồn tại";
            }
            else if (String.IsNullOrEmpty(MatKhau))
            {
                ViewData["Err6"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(NhapLaiMatKhau))
            {
                ViewData["Err7"] = "Phải nhập lại mật khẩu";
            }
            else if (MatKhau != NhapLaiMatKhau)
            {
                ViewData["Err7"] = "Mật khẩu nhập lại không đúng";
            }
            else if (captcha == "")
            {
                ViewData["Err8"] = "Captcha validation is required.";
            }
            
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)

                nd.HoTen = HoTen;
                nd.GioiTinh = GioiTinh;
                nd.Email = Email;
                nd.SDT = SDT;
                nd.TaiKhoan = TaiKhoan;
                nd.MatKhau = Encode(MatKhau);
                nd.NgaySinh = Convert.ToDateTime(NgaySinh);
                db.NguoiDungs.Add(nd);
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }
            return Register();
        }
        private string requestCookie(string name)
        {
            HttpCookie nameCookie = Request.Cookies[name];
            if (nameCookie != null)
                return nameCookie.Value.ToString();
            else return null;
        }
        private void writeCookie(string name,string value)
        {
            HttpCookie nameCookie = new HttpCookie(name);
            nameCookie.Value = value;
            nameCookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(nameCookie);
        }
        private void disableCookie(string name)
        {
            HttpCookie nameCookie = new HttpCookie(name);
            nameCookie.Expires = DateTime.Now.AddDays(-1);
        }
        [HttpGet]
        public ActionResult Login()
        {
            string tendn = requestCookie("tendn");
            if(!string.IsNullOrEmpty(tendn))
            {
                ViewBag.tendn = tendn;
                ViewBag.matkhau = requestCookie("matkhau");
            }
            Session.Clear();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Tendn,string MatKhau,bool GhiNho)
        {
            MovieWebContext db = new MovieWebContext();
            var tendn = Tendn;
            var luumatkhau = MatKhau;
            var matkhau = Encode(luumatkhau);
            var ghinho = GhiNho;

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Err1"] = "Tên đăng nhập không được bỏ trống!";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Err2"] = "Mật khẩu không được bỏ trống!";

            }
            else
            {
                NguoiDung N = db.NguoiDungs.FirstOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                if (N != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công!";
                    Session["TaiKhoan"] = N.TaiKhoan;
                    Session["HinhAnh"] = N.HinhAnhNguoiDung;
                    Session["TenNguoiDung"] = N.HoTen;
                    PhieuDangKy phieuDangKy = db.PhieuDangKies.OrderByDescending(x => x.NgayHetHan).FirstOrDefault(x => x.MaNguoiDung == N.MaNguoiDung);
                    if (phieuDangKy != null && DateTime.Compare((DateTime)phieuDangKy.NgayHetHan, DateTime.Now) > 0)
                        Session["VIP"] = phieuDangKy.MaGoiDV;
                    else Session["VIP"] = null;
                    if (ghinho)
                    {
                        writeCookie("tendn", tendn);
                        writeCookie("matkhau", luumatkhau);
                    }
                    else
                    {
                        disableCookie("tendn");
                        disableCookie("matkhau");
                    }

                    return RedirectToAction("Home", "Movie");
                }
                else
                {
                    ViewData["Err2"] = "Sai mật khẩu hoặc tên đăng nhập";
                }
                
            }
            return View();

           // return RedirectToAction("Home", "Movie");
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(FormCollection Form)
        {
            String Email = Form["email"];
            MovieWebContext db = new MovieWebContext();
            NguoiDung nguoidung = db.NguoiDungs.SingleOrDefault(p => p.Email == Email);
            Token token = new Token(10);
            if (nguoidung != null)
            {
                MailModel forgot = new MailModel();
                forgot.From = "twoonez@outlook.com";
                forgot.To = Email;
                forgot.Subject = "Reset password";
                forgot.Body = "your code is : " + token.token;
                Session["Email"] = Email;
                Session["Token"] = token.token;
                forgot.SendMail();
                return RedirectToAction("ResetPassWord");
            }
            else
            {
                ViewData["Err"] = "Email khong ton tai";
            }
            return View("ForgotPassword");
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(FormCollection collection)
        {
            var token = collection["Token"];
            String Token = Session["Token"].ToString();
            if (token != null && token == Token)
            {
                Session["Token"] = null;
                return RedirectToAction("ResetPassword_2");
            }
            else
            {
                ViewData["Err"] = "Ma xac nhan khong dung";
            }
            return View("ResetPassword");
        }
        public ActionResult ResetPassword_2()
        {
            if (Session["Token"] != null)
                return RedirectToAction("ResetPassword");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword_2(FormCollection collection)
        {
            if (Session["Token"] != null)
                return RedirectToAction("Login");
            else
            {
                var MatKhau = collection["MatKhau"];
                var MatKhauNhapLai = collection["NhapLaiMatKhau"];
                if (MatKhau == null)
                {
                    ViewData["Err1"] = "Mật khẩu không được bỏ trống";
                }
                else if (MatKhauNhapLai == null)
                {
                    ViewData["Err2"] = "Mật khẩu nhập lại không được bỏ trống";
                }
                else if (MatKhauNhapLai != MatKhau)
                {
                    ViewData["Err2"] = "Mật khẩu nhập lại không đúng";
                }
                else
                {
                    MovieWebContext db = new MovieWebContext();
                    string Email = Session["Email"].ToString();
                    NguoiDung nguoiDung = db.NguoiDungs.SingleOrDefault(x => x.Email == Email);
                    nguoiDung.MatKhau = Encode(MatKhau);
                    db.SaveChanges();
                    Session.Clear();
                    return RedirectToAction("Login");
                }
                return View();
            }
        }
        public ActionResult ShowInfor(string mode,int? page)
        {
            int pageNumber = (page ?? 1);
            ViewBag.page = pageNumber;
            MovieWebContext db = new MovieWebContext();
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Login");
            var k = Session["TaiKhoan"].ToString();
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == k);
            if (string.IsNullOrEmpty(mode)) mode = null;
            ViewBag.Mode = mode;
            if (mode == null)
                ViewBag.info = "active";
            if (mode == "favorite")
                ViewBag.favo = "active";
            if (mode == "changepass")
                ViewBag.pass = "active";
            return View(nguoiDung);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeInfor(FormCollection collection)
        {
            MovieWebContext db = new MovieWebContext();

           
            var HoTen = collection["HoTen"];
            var NgaySinh = collection["NgaySinh"];
            var GioiTinh = collection["Gioitinh"];
            var Email = collection["Email"];
            var SDT = collection["SDT"];
            var taikhoan = Session["TaiKhoan"].ToString();
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == taikhoan);
            if (String.IsNullOrEmpty(Email))
            {
                ViewData["Err"] = "Email không được bỏ trống";
            }
            else
            {
                nguoiDung.HoTen = HoTen;
                nguoiDung.NgaySinh = DateTime.Parse(NgaySinh);
                nguoiDung.GioiTinh = GioiTinh;
                nguoiDung.Email = Email;
                nguoiDung.SDT = SDT;
                db.SaveChanges();
                return RedirectToAction("ShowInfor",1);
            }
            return PartialView("ChangeInfor");
        }
        public ActionResult ChangePassword()
        {
            MovieWebContext db = new MovieWebContext();
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Login");
            var k = Session["TaiKhoan"].ToString();
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == k);
            return PartialView(nguoiDung);
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {

            var old_pass = collection["old_pass"].ToString();
            var new_pass = collection["new_pass"].ToString();
            var confirm_pass = collection["confirm_pass"].ToString();
            old_pass = Encode(old_pass);
            new_pass = Encode(new_pass);
            confirm_pass = Encode(confirm_pass);
            MovieWebContext db = new MovieWebContext();
            var k = Session["TaiKhoan"].ToString();
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == k);
            if (old_pass == nguoiDung.MatKhau)
            {
                nguoiDung.MatKhau = new_pass;
                db.SaveChanges();
                return RedirectToAction("ShowInfor");
            }
            else
            {
                ViewData["Err"] = "Sai mat khau";
                return RedirectToAction("ShowInfor");
            }

        }

        public ActionResult ShowFavorite(int? page)
        {
            MovieWebContext db = new MovieWebContext();
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Login");
            var k = Session["TaiKhoan"].ToString();
            NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == k);
            List<DanhGiaPhim> list = db.DanhGiaPhims.Where(x => x.MaNguoiDung == nguoiDung.MaNguoiDung && x.SoSao==5).ToList();
            List<Phim> phims = new List<Phim>();
            nguoiDung.DanhGiaPhims.Where(i=>i.SoSao==5).ToList(); 
            foreach(var item in list)
                if(!phims.Contains(item.Phim))
            {
                phims.Add(item.Phim);
            }
            int pageNumber = (page ?? 1);
            ViewBag.page = pageNumber;
            return PartialView(phims.ToPagedList(pageNumber, 4));
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Home", "Movie");
        }
    }
}

