using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieWeb.Models;
using System.Configuration;
using log4net;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace MovieWeb.Controllers
{
    public class PaymentController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: ThanhToan
        public ActionResult PaymentPage()
        {
            return View();
        }
        public ActionResult ThongTinThanhToan()
        {

            var userName = Session["TaiKhoan"];
            if (userName == null) return RedirectToAction("Login", "User");
            MovieWebContext db = new MovieWebContext();
            dynamic model = new ExpandoObject();

            model.User = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == userName);
            var goiMuonMua = int.Parse(Session["GoiMuonMua"].ToString());
            model.GoiDichVu = db.GoiDichVus.Find(goiMuonMua);

            return View(model);
        }
        public ActionResult ChonPhuongThucThanhToan(int? id)
        {
            id = (id ?? 2);
            Session["GoiMuonMua"] = id;
            return View();
        }
        [HttpPost]
        public void ThongTinThanhToan(FormCollection collection)
        {
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat

            //Get payment input
            MovieWebContext db = new MovieWebContext();
            int id = int.Parse(Session["GoiMuonMua"].ToString());
            string userName = Session["TaiKhoan"].ToString();
            var goi = db.GoiDichVus.Find(id);
            var user = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == userName);


            OrderInfo order = new OrderInfo();
            order.OrderId = DateTime.Now.Ticks;// Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
            order.Amount = (long)goi.GiaGoiDV;// Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY giá gói
            order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending"
            order.OrderDesc = "Thanh toan goi " + goi.TenGoiDV;
            order.CreatedDate = DateTime.Now;
            string locale = collection["cboLanguage"];

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
            var cboBankCode = collection["cboBankCode"];
            if (cboBankCode != null && !string.IsNullOrEmpty(cboBankCode))
            {
                vnpay.AddRequestData("vnp_BankCode", cboBankCode);
            }
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            if (!string.IsNullOrEmpty(locale))
            {
                vnpay.AddRequestData("vnp_Locale", locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }
            var orderCategory = "Thanh toán trực tuyến";
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.OrderId);
            vnpay.AddRequestData("vnp_OrderType", orderCategory); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
                                                                          //Add Params of 2.1.0 Version


            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            Response.Redirect(paymentUrl);
        }
        public ActionResult Result()
        {
            String s = "Something went wrong. Please go back";
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string ss in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(ss) && ss.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(ss, vnpayData[ss]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        int k = int.Parse(Session["GoiMuonMua"].ToString());
                        Session["GoiMuonMua"] = null;
                        MovieWebContext db = new MovieWebContext();

                        PhieuDangKy phieuDangKy = new PhieuDangKy();
                        phieuDangKy.MaGoiDV = k;
                        string taiKhoan = Session["TaiKhoan"].ToString();
                        NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == taiKhoan);
                        phieuDangKy.MaNguoiDung = nguoiDung.MaNguoiDung;
                        phieuDangKy.NgayThanhToan = DateTime.Now;
                        phieuDangKy.NgayHetHan = DateTime.Now.AddMonths(1).AddDays(-1);
                        phieuDangKy.ThanhTien = db.GoiDichVus.Find(k).GiaGoiDV;
                        db.PhieuDangKies.Add(phieuDangKy);
                        db.SaveChanges();
                        Session["VIP"] = k;
                        //Thanh toan thanh cong
                        s = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";


                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        s = "Thanh toan khong thanh cong. Ma loi la: " + vnp_ResponseCode;


                    }
                }
                else
                {
                    s = "Thanh toan khong thanh cong";
                }
            }
            ViewBag.Result = s;
            return View();
        }
        public ActionResult ThongTinThanhToan_MOMO()
        {

            MovieWebContext db = new MovieWebContext();
            int goiMuonMua = int.Parse(Session["GoiMuonMua"].ToString()); ;
            GoiDichVu goi = db.GoiDichVus.FirstOrDefault(x => x.MaGoiDV == goiMuonMua);
            string endpoint = ConfigurationManager.AppSettings["endpoint"];
            string partnerCode = ConfigurationManager.AppSettings["partnerCode"];
            string accessKey = ConfigurationManager.AppSettings["accessKey"];
            string serectkey = ConfigurationManager.AppSettings["serectkey"];
            string orderInfo = "thanh_toan";
            string redirectUrl = ConfigurationManager.AppSettings["returnMOMO"];
            string ipnUrl = ConfigurationManager.AppSettings["returnMOMO"];
            string requestType = "captureWallet";

            string amount = ((int)goi.GiaGoiDV).ToString();
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + redirectUrl +
                "&requestId=" + requestId +
                "&requestType=" + requestType
                ;
            log.Debug("rawHash = " + rawHash);

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);
            log.Debug("Signature = " + signature);
            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };
            log.Debug("Json request to MoMo: " + message.ToString());
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            //  ViewBag.hi = responseFromMomo;
            log.Debug("Return from MoMo: " + jmessage.ToString());
            System.Diagnostics.Process.Start(jmessage.GetValue("payUrl").ToString());
            return View();
        }
        public ActionResult Result_MOMO()
        {
            string serectkey = ConfigurationManager.AppSettings["serectkey"];
            var momoData = Request.QueryString;
            if (momoData != null)
            {
                int k = int.Parse(Session["GoiMuonMua"].ToString());
                Session["GoiMuonMua"] = null;
                MovieWebContext db = new MovieWebContext();

                PhieuDangKy phieuDangKy = new PhieuDangKy();
                phieuDangKy.MaGoiDV = k;
                string taiKhoan = Session["TaiKhoan"].ToString();
                NguoiDung nguoiDung = db.NguoiDungs.FirstOrDefault(x => x.TaiKhoan == taiKhoan);
                phieuDangKy.MaNguoiDung = nguoiDung.MaNguoiDung;
                phieuDangKy.NgayThanhToan = DateTime.Now;
                phieuDangKy.NgayHetHan = DateTime.Now.AddMonths(1).AddDays(-1);
                phieuDangKy.ThanhTien = db.GoiDichVus.Find(k).GiaGoiDV;
                db.PhieuDangKies.Add(phieuDangKy);
                db.SaveChanges();
                Session["VIP"] = k;
            }
            return View();
        }
    }
}