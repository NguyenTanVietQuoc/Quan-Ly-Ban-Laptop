using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
namespace StoreLaptop.Controllers
{
    public class CungLoaiController : Controller
    {
        // GET: CungLoai
        Service1Client db = new Service1Client();
        public PartialViewResult CungLoaiPartial(int idLoai,int idSP)
        {
            ViewBag.cl = idSP;
            LoaiSP loai = db.KTLoaiSP(idLoai);
            if(loai==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.cungloai = db.LaySPcungloai(idLoai,idSP);
            
            return PartialView();
        }
    }
}