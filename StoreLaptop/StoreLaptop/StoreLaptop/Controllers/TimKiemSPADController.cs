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
    public class TimKiemSPADController : Controller
    {
        // GET: TimKiemSPAD
        Service1Client db = new Service1Client();
        [HttpGet]
        public ActionResult Timkiem(int? page, String TuKhoa)
        {
            ViewBag.tukhoa = TuKhoa;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            ViewBag.tukhoa = TuKhoa;
            var timkiem = db.Timkiem(TuKhoa).OrderBy(n => n.TenSP);
            return View(timkiem.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult Timkiem(int? page, FormCollection f)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            string TuKhoa = f["TuKhoa"].ToString();
            ViewBag.tukhoa = TuKhoa;
            var timkiem = db.Timkiem(TuKhoa).OrderBy(n=>n.TenSP);
            return View(timkiem.OrderBy(n=>n.TenSP).ToPagedList(pageNumber,pageSize));
        }
        //nguoi  dung
        //[HttpPost]
        //public ActionResult TimkiemNN(FormCollection f)
        //{
        //    ViewBag.tukhoa1 = f["TuKhoa"].ToString();
        //    string TuKhoa = f["TuKhoa"].ToString();
        //    ViewBag.tukhoa = TuKhoa;
        //    ViewBag.timkiem = db.Timkiem(TuKhoa).OrderBy(n => n.TenSP);
        //    return View();
        //}

        [HttpGet]
        public ActionResult TimkiemNN(int? page, String TuKhoa)
        {
            ViewBag.tukhoa = TuKhoa;
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            ViewBag.tukhoa = TuKhoa;
            var timkiem = db.Timkiem(TuKhoa).OrderBy(n => n.TenSP);
            return View(timkiem.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult TimkiemNN(int? page, FormCollection f)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            string TuKhoa = f["TuKhoa"].ToString();
            ViewBag.tukhoa = TuKhoa;
            var timkiem = db.Timkiem(TuKhoa).OrderBy(n => n.TenSP);
            return View(timkiem.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }
    }
}