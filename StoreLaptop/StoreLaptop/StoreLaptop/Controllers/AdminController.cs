using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        Service1Client db = new Service1Client();
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Index() { return View(); } 
        [HttpPost]
        public ActionResult Index(string tk,string mk,FormCollection f)
        {
            tk=  f["username"]; 
            mk= f["password"];
            Admin ad = db.XuLy_DangNhap(tk,mk);
            if (ad != null)
            {
                Session["Admin"] = ad.HoTen;
               
                ViewBag.DangNhap = "Đăng nhập thành công!";
                return RedirectToAction("Index", "QuanLySP");
            }
            else
            {
                ViewBag.DangNhap = "Sai tên hoặc mặt khẩu!";
                //return RedirectToAction("Index");
                return View(); 
            }
            
            
        }

        public ActionResult DangXuat()
        {
            Session.Remove("TaiKhoan");
            return RedirectToAction("Index");
        }
    }
}