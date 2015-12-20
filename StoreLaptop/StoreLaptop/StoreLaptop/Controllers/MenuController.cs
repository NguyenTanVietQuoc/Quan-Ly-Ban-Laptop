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
    public class MenuController : Controller
    {
        // GET: Menu
        Service1Client db = new Service1Client();
        public PartialViewResult MenuPartial()
        {
            //ViewBag.lsp= db.LayLoaiSP();
            ViewBag.hsx = db.LayHangsx().Where(x=>x.AnHien==1);
            return PartialView();
        }
        public ActionResult ChiTietLoai(int idLoai, int? page = 1)
        {
            ViewBag.idloai = idLoai;
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            LoaiSP loai = db.KTLoaiSP(idLoai);
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sp = db.LaySPtheoLoai(idLoai).ToList();
            if (sp == null)
            {
                ViewBag.loai = "Không có sản phẩm thuộc loại này!";
                return View();
            }
            
            return View(sp.ToPagedList(pageNumber, pageSize));
        }
    }
}