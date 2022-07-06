using MovieWeb.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace MovieWeb.Areas.Administrator.Controllers
{
    public class Adm_TrangChuController : Controller
    {

        // GET: Administrator/Adm_TrangChu
        private static int xx = 0, yy = 0, zz = 0;
        private string GetRandomColour()
        {
            Random random = new Random();
            xx = (xx + DateTime.Now.Millisecond + DateTime.Now.Second + random.Next(24)) % 256;
            yy = (yy + DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Hour) % 256;
            zz = (zz + DateTime.Now.Millisecond + DateTime.Now.Second + DateTime.Now.Minute) % 256;
            return "rgb(" + xx.ToString() + "," + yy.ToString() + "," + zz.ToString() + ")";
        }

        public ActionResult Statistical(int? month, int? year)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");
            MovieWebContext data = new MovieWebContext();
           // dynamic model = new ExpandoObject();
            year = year.HasValue ? year.Value : 2022;
            var gois = from x in data.GoiDichVus select x;
            ViewBag.year = year.ToString();
            ViewBag.month = month;
            int count = gois.Count();
            if (!month.HasValue)
            {
                float tongDoanhThu = 0;
                var doanhthunam = new Pie(count);
                int dem = -1;
                Int32 r = 0;
                var random = new Random((int)DateTime.Now.Ticks);
                List<Tuple<string, float>> DTTG = new List<Tuple<string, float>>();
                foreach (var item in gois)
                {
                    r += random.Next(0x1000000);
                    dem++;
                    doanhthunam.data.labels[dem] = item.TenGoiDV;
                    doanhthunam.data.datasets[0].backgroundColor[dem] = String.Format("#{0:X6}", r % 0x1000000);
                    float thanhtien = (float)(item.PhieuDangKies.Where(x => x.NgayThanhToan.Year == year).Sum(x => x.ThanhTien));
                    doanhthunam.data.datasets[0].data[dem] = thanhtien;
                    tongDoanhThu += thanhtien;
                    DTTG.Add(new Tuple<string, float>(item.TenGoiDV, thanhtien));
                }
                for (int i = 0; i < count; i++)
                {
                    doanhthunam.data.datasets[0].data[i] = doanhthunam.data.datasets[0].data[i] / tongDoanhThu * 100;
                }
                DTTG.Add(new Tuple<string, float>("Tổng doanh thu", tongDoanhThu));
                ViewBag.DTTG = DTTG;
                doanhthunam.options.plugins.title.text = "Doanh thu theo từng gói năm "+year.ToString();
                ViewBag.doanhthunam = doanhthunam;
            var dangkymoinam = new Pie(count);
            dem = -1;
            float tongDangKyMoi = 0;
            List<Tuple<string, int>> DKTG = new List<Tuple<string, int >>();
            foreach (var item in gois)
            {
                r += random.Next(0x1000000);
                dem++;
                dangkymoinam.data.labels[dem] = item.TenGoiDV;
                dangkymoinam.data.datasets[0].backgroundColor[dem] = String.Format("#{0:X6}", r % 0x1000000);
                float Dangky = (float)(item.PhieuDangKies.Where(x => x.NgayThanhToan.Year == year).Count());
                dangkymoinam.data.datasets[0].data[dem] = Dangky;
                tongDangKyMoi += Dangky;
                DKTG.Add(new Tuple<string, int>(item.TenGoiDV, (int)Dangky ) );
            }
            for (int i = 0; i < count; i++)
            {
                dangkymoinam.data.datasets[0].data[i] = dangkymoinam.data.datasets[0].data[i] / tongDangKyMoi * 100;
            }
            DKTG.Add(new Tuple<string, int>("Tổng đăng ký mới trong năm", (int)tongDangKyMoi));
            ViewBag.DKTG = DKTG;
            dangkymoinam.options.plugins.title.text = "Số đăng ký mới theo gói năm " + year.ToString();
            ViewBag.dangkymoinam = dangkymoinam;
            dem = -1;
            Line doanhthutungthang = new Line(count);
            doanhthutungthang.data.labels = new string[12] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };

            // Pie doanhthutheothangPie ;
            List< object> DTTT = new List<object>();
            List< object> tong = new List<object>();
            tong.Add("Tổng");
            for (int i = 0; i < 12; i++)
                tong.Add(0f);
            foreach (var item in gois)
            {
                dem++;
                doanhthutungthang.data.datasets[dem].data = new float[12];
                doanhthutungthang.data.datasets[dem].label = item.TenGoiDV;
                doanhthutungthang.data.datasets[dem].borderColor = GetRandomColour();
                // float[] tmp = new float[12];
                List< object> DTT = new List<object>();
                DTT.Add(item.TenGoiDV);
                for (int i = 0; i < 12; i++)
                {
                    doanhthutungthang.data.datasets[dem].data[i] = (float)(item.PhieuDangKies.Where(x => x.NgayThanhToan.Year == year && x.NgayThanhToan.Month == i).Sum(x => x.ThanhTien));
                    DTT.Add(doanhthutungthang.data.datasets[dem].data[i]);
                    tong[i + 1] =(float)tong[i+1]+ doanhthutungthang.data.datasets[dem].data[i];
                }
                DTTT.Add(DTT);
            }
            DTTT.Add(tong);
            ViewBag.DTTT = DTTT;
            doanhthutungthang.options.plugins.title.text = "Doanh thu theo từng tháng năm " + year.ToString();
            ViewBag.doanhthutungthang = doanhthutungthang;

            dem = -1;
            Line dangkymoitungthang = new Line(count);
            dangkymoitungthang.data.labels = new string[12] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            List<object> DKTT = new List<object>();
            List<object> tong2 = new List<object>();
            tong2.Add("Tổng");
            for (int i = 0; i < 12; i++)
                tong2.Add(0f);

            // Pie doanhthutheothangPie ;

            foreach (var item in gois)
            {
                dem++;
                dangkymoitungthang.data.datasets[dem].data = new float[12];
                dangkymoitungthang.data.datasets[dem].label = item.TenGoiDV;
                dangkymoitungthang.data.datasets[dem].borderColor = GetRandomColour();
                List<object> DKT = new List<object>();
                DKT.Add(item.TenGoiDV);
                // float[] tmp = new float[12];
                for (int i = 0; i < 12; i++)
                {

                    dangkymoitungthang.data.datasets[dem].data[i] = (float)(item.PhieuDangKies.Where(x => x.NgayThanhToan.Year == year && x.NgayThanhToan.Month == i).Count());
                    DKT.Add(dangkymoitungthang.data.datasets[dem].data[i]);
                    tong2[i + 1] = (float)tong2[i + 1] + dangkymoitungthang.data.datasets[dem].data[i];
                }
                DKTT.Add(DKT);
            }
            DKTT.Add(tong2);
            ViewBag.DKTT = DKTT;
            dangkymoitungthang.options.plugins.title.text = "Số đăng ký mới theo từng tháng " + year.ToString();
            ViewBag.dangkymoitungthang = dangkymoitungthang;
            }
            else
            {
                int days = DateTime.DaysInMonth((int)year, (int)month);
                Line doanhthutheothang = new Line(count);
                doanhthutheothang.data.labels = new string[days];
                for (int i = 0; i < days; i++)
                {
                    doanhthutheothang.data.labels[i] = (i + 1).ToString();
                }
                int dem = -1;
                foreach (var item in gois)
                {
                    dem++;
                    doanhthutheothang.data.datasets[dem].data = new float[days];
                    doanhthutheothang.data.datasets[dem].label = item.TenGoiDV;
                    doanhthutheothang.data.datasets[dem].borderColor = GetRandomColour();
                    // float[] tmp = new float[12];
                    for (int i = 0; i < days; i++)
                    {

                        doanhthutheothang.data.datasets[dem].data[i] = (float)(item.PhieuDangKies.Where(x => x.NgayThanhToan.Year == year && x.NgayThanhToan.Month == month && x.NgayThanhToan.Day == i).Sum(x => x.ThanhTien));
                    }
                }
               ViewBag.doanhthutheothang = doanhthutheothang;
           }
            return View();
        }
        public ActionResult Adm_Home()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Adm_TrangChu");
            MovieWebContext db = new MovieWebContext();
            ViewBag.month = (float)(db.PhieuDangKies.Where(x => x.NgayThanhToan.Year == DateTime.Now.Year && x.NgayThanhToan.Month == DateTime.Now.Month).Sum(x => x.ThanhTien));
            ViewBag.year = (float)(db.PhieuDangKies.Where(x => x.NgayThanhToan.Year == DateTime.Now.Year).Sum(x => x.ThanhTien));
            ViewBag.LuongDangKy = ((float)(db.NguoiDungs.Where(x => x.PhieuDangKies.Any(y => y.MaGoiDV != 3)).Count())) / ((float)(db.NguoiDungs.Count())) * 100;
            //ViewBag.TienHomNay = (float)(db.PhieuDangKies.Where(x => x.NgayThanhToan.Year == DateTime.Now.Year && x.NgayThanhToan.Month == DateTime.Now.Month && x.NgayThanhToan.Day == DateTime.Now.Day).Sum(x => x.ThanhTien));
            return View();
        }
        [HttpGet]

        public ActionResult Login()
        {
            
            Session.Clear();
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            MovieWebContext db = new MovieWebContext();
            var tendn = collection["Tendn"];
            var matkhau = collection["MatKhau"];
            //var ghinho = collection["GhiNho"];
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
                Admin N = db.Admins.FirstOrDefault(n => n.TaiKhoanAdmin == tendn && n.MatKhauAdmin == matkhau);
                if (N != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công!";
                    Session["Admin"] = N;
                    return RedirectToAction("Adm_Home");
                }
                else
                    ViewBag.Thongbao = "Đăng nhập thất bại. Vui lòng thử lại!";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]

        public ActionResult ForgotPassword(FormCollection Form)
        {
            String Email = Form["email"];
            MovieWebContext db = new MovieWebContext();
            Admin admin = db.Admins.SingleOrDefault(p => p.EmailAdmin == Email);
            Token token = new Token(10);

            if (admin != null)
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
        public ActionResult ResetPassword_2(FormCollection collection)
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
                Admin admin = db.Admins.SingleOrDefault(x => x.EmailAdmin == Email);
                admin.MatKhauAdmin = MatKhau;
                db.SaveChanges();
                Session.Clear();
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Clear();    
            return RedirectToAction("Login", "Adm_TrangChu");
        }
    }
}