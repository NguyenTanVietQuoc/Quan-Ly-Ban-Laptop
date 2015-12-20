using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        Service1Client db = new Service1Client();
        public ActionResult XemChiTiet(int idSP,string thongbao)
        {
            ViewBag.thongbao = thongbao;
            SanPham SP = db.ChiTietSP(idSP);
            if (SP == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(SP);
            
        }
        public String TenHangSX(int id)
        {
            string aa = db.LayHSX().Single(n=>n.idHSX==id).TenHSX;

            return aa;
        }

        public List<ChungLoai> LayChungLoaiTheoID(int id)
        {
            return db.LayChungLoai().Where(n => n.idCL == id).ToList();
        }
    }
}