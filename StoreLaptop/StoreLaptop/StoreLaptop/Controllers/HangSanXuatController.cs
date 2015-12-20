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
    public class HangSanXuatController : Controller
    {
        // GET: HangSanXuat
        Service1Client db = new Service1Client();
        public PartialViewResult HangSanXuatPartial()
        {
            ViewBag.hsx = db.LayHSX().Where(x=>x.AnHien==1);
            return PartialView();
        }
        public ViewResult SPHangSanXuat(int idHSX = 0, int? page = 1)
        {
           
            ViewBag.idhsx = idHSX;
            int pageSize = 9;

            int pageNumber = (page ?? 1);

            HangSanXuat HSX = db.KiemtraHSX(idHSX);
            if (HSX == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenHSX = HSX.TenHSX;
            var sp = db.LaySPtheoHSX(idHSX).Where(x=>x.AnHien==1);
            if(sp == null)
            {
                ViewBag.sp = "Không có sản phẩm thuộc hãng này";
            }

            return View(sp.ToPagedList(pageNumber, pageSize));
        }
    }
}