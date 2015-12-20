using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
using PagedList;
using PagedList.Mvc;

namespace StoreLaptop.Controllers
{
    public class QuanLyDHController : Controller
    {
 
        // GET: QuanLyHD
        Service1Client db = new Service1Client();
        
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                int pageSize = 10;
                int pageNumber=(page ?? 1);
                var dh = db.LayDH();
                return View(dh.OrderBy(n=>n.idDH).ToPagedList(pageNumber,pageSize));
            }
        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult CapNhatTinhTrang(int idDH)
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                DonHang dh = db.Lay(idDH);
                //DonHang dh = db.Lay(idDH);
                //List<SelectListItem> TinhTrang = new List<SelectListItem>();
                //TinhTrang.Add(new SelectListItem { Text = "Đang chờ xử lý", Value = "1" });
                //TinhTrang.Add(new SelectListItem { Text = "Đang giao", Value = "2" });
                //TinhTrang.Add(new SelectListItem { Text = "Đã giao", Value = "3" });
                //TinhTrang.Add(new SelectListItem { Text = "Hủy", Value = "4" });
                //ViewBag.TinhTrang = TinhTrang;
                return View(dh);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
         public ActionResult CapNhatTinhTrang(int idDH,DonHang donhang, FormCollection f)
         {
             donhang.idDH = idDH;
             //List<SelectListItem> TinhTrang = new List<SelectListItem>();
             //TinhTrang.Add(new SelectListItem { Text = "Đang chờ xử lý", Value = "1" });
             //TinhTrang.Add(new SelectListItem { Text = "Đang giao", Value = "2" });
             //TinhTrang.Add(new SelectListItem { Text = "Đã giao", Value = "3" });
             //TinhTrang.Add(new SelectListItem { Text = "Hủy", Value = "4" });
             //ViewBag.TinhTrang = TinhTrang;
             donhang.TinhTrang = int.Parse(f["tinhtrang"]);
             db.SuaTinhTrang(donhang);
             if (donhang.TinhTrang == 4)
             {
                 var dhct = db.LayDHCT(idDH);
                 foreach(var i in dhct){
                     var sp = db.LaySP().Where(n => n.idSP == i.idSP);
                     foreach (var j in sp)
                     {
                         j.TonKho += i.SoLuong;
                         j.SoLanMua -= i.SoLuong;
                         db.ChinhSua(j);
                     }
                 }
             }
             return RedirectToAction("Index"); 
         }
        public ActionResult DonhangCT(int id)
        {
            ViewBag.dhct = db.LayDHCT(id);
            return View();
        }
    }
}