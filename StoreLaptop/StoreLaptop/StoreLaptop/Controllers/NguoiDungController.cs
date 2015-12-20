using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky(string thongbao)
        {
            ViewBag.thongbao = thongbao;
            return View();
        }
        [HttpPost]
        
        public ActionResult DangKy(KhachHang kh, FormCollection f)
        {
            //int i = db.KiemTraTK(kh.Email);
            //if(i==1)
            //{
            //    ViewBag.KiemTraTK = "Email đã được sử dụng";
            //    return View();
            //}
            //else
            //{
            //    ViewBag.KiemTraTK = "";
            //    kh.NgayDK = DateTime.Now;
            //    ViewBag.Them = db.ThemKH(kh);
            //    ViewBag.KiemTraTk = "Đăng Ký Thành Công";
            //}
            if(kh.HoTen==null||kh.Email==null||kh.DienThoai==null||kh.MatKhau==null)
            { return RedirectToAction("DangKy", "NguoiDung",new {thongbao="Bạn phải nhập đầy đủ thông tin"}); }
            else
            {
                if (kh.MatKhau != f["repass"])
                { return RedirectToAction("DangKy", "NguoiDung", new { thongbao = "Mật khẩu nhập lại không đúng" }); }
                else
                {
                    int i = db.KiemTraTK(kh.Email);
                    if (i == 1)
                    {
                        ViewBag.KiemTraTK = "Email đã được sử dụng";
                        return View();
                    }
                    else
                    {
                        ViewBag.KiemTraTK = "";
                        kh.NgayDK = DateTime.Now;
                        kh.NgaySinh = DateTime.Now;
                        ViewBag.Them = db.ThemKH(kh);
                        ViewBag.KiemTraTk = "Đăng Ký Thành Công";
                    }
                }
            }
            return RedirectToAction("Index","Home");
        }
        
        
        [HttpGet]
        public ViewResult DangNhap()
        {
            Session["url"] = Request.UrlReferrer.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string taikhoan = f.Get("txtEmail").ToString();
            string matkhau = f.Get("txtpass").ToString();
            
            KhachHang kh = db.KTDangNhap(taikhoan,matkhau);
            
            if(kh!=null)
            {
                ViewBag.thongbao="Đặng nhập thành công!";
                
                    Session["taikhoan"]=kh;
                    Session["ten"] = kh.HoTen;
                    Session["idKH"] = kh.idKH;
                    Response.Redirect(Session["url"].ToString());           
            }
            ViewBag.thongbao = "Email hoặc mật khẩu không đúng!";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
        
    
    }
}