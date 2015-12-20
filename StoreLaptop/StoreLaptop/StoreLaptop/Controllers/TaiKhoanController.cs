using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        Service1Client db = new Service1Client();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TTKhachHang()
        {
            if (Session["idKH"] == null) { return RedirectToAction("DangNhap", "NguoiDung"); }
            else
            {
                int idKH = (int)Session["idKH"];
                KhachHang kh = db.Lay1KH(idKH);
                return View(kh);
            }
        }
        [HttpGet]
        public ActionResult DoiMatKhau()
        {            
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(int idKH, FormCollection f)
        {
            KhachHang kt = db.KTKH(idKH);
            if(kt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if(kt.MatKhau != f["matkhaucu"])
            {
                ViewBag.matkhaucu = "Sai Mật Khẩu";
                return View();
            }
            kt.MatKhau = f["MatKhau"];
            db.DoiMK(kt);
            Session.Remove("taikhoan");
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public ActionResult SuaKH(string thongbao)
        {

            if (Session["idKH"] == null) { return RedirectToAction("DangNhap", "NguoiDung"); }
            else
            {
                ViewBag.thongbao = thongbao;
                int idKH = (int)Session["idKH"];
                KhachHang kh = db.Lay1KH(idKH);
                return View(kh);
            }
        }
        [HttpPost]
        public ActionResult SuaKH(int idKH, KhachHang kh)
        {
            if (kh.HoTen == null || kh.Email == null || kh.DiaChi == null || kh.DienThoai == null)
            {
                
                return RedirectToAction("SuaKH", "TaiKhoan", new { idKH = idKH,thongbao = "Bạn Không được sửa trống!" });
            }
            else { 
            KhachHang kt = db.KTKH(idKH);
            if (kt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SuaKH(kh);
            return RedirectToAction("Index","Home");
            }
        }
        public ActionResult DonHangPartial()
        {
            if (Session["idKH"] == null) { return RedirectToAction("DangNhap", "NguoiDung"); }
            else
            {
                int idKH = (int)Session["idKH"];
                ViewBag.dh = db.LayDHTheoKH(idKH);
                return PartialView();
            }
        }
        public ActionResult ChiTietDonHang(int idDH)
        {
           
             ViewBag.ctdhkh  = db.LayDHCT(idDH);
             
             ViewBag.sp = db.LaySP();
             
            return View();
        }
        public ActionResult HuyDonHang(int idDH)
        {
            db.CapNhatDonHang(idDH);
            return RedirectToAction("TTKhachHang","TaiKhoan");
            }
        
    }
}