using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.ServiceReference;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace StoreLaptop.Controllers
{
    public class QuanLySPController : Controller
    {
        // GET: QuanLySP
        Service1Client db = new Service1Client();
        [HttpGet]
        public ActionResult Index(int? page, int id=1)
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                //Số sản phẩm trên trang
                int pageSize = 10;
                //Số trang
                int pageNumber = (page ?? 1);
                ViewBag.idHSX = id;
                ViewBag.hsx2 = db.LayHangsx();
                var sp = db.LaySPHsx(id);
                return View(sp.OrderByDescending(n => n.idSP).ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpPost]
        public ActionResult Index(int? page,int idHSX, FormCollection f )
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                //Số sản phẩm trên trang
                int pageSize = 10;
                //Số trang
                int pageNumber = (page ?? 1);
                
                idHSX = int.Parse(f["idhsx"]);
                ViewBag.idHSX = idHSX;
                ViewBag.hsx2 = db.LayHangsx();
                var sp = db.LaySPHsx(idHSX);
                return View(sp.OrderByDescending(n => n.idSP).ToPagedList(pageNumber, pageSize));
            }
        }
        
        //Them moi
         [ValidateInput(false)]
        [HttpGet]
        public ActionResult ThemMoi(int id, string thongbao)
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {
                ViewBag.idhsx = id;
                ViewBag.hsx = db.LayHangsx();
                ViewBag.thongbao = thongbao;
                return View();
            }
        }
         [ValidateInput(false)]
        [HttpPost]
        public ActionResult ThemMoi(SanPham sanpham, HttpPostedFileBase fileUpload,FormCollection f)
        {
            sanpham.idHSX = int.Parse(f["idHSX"]);
            //var hsx = db.LayHangsx();
            //string TenHSX=hsx.Single(n=>n.idHSX==sanpham.idHSX).TenHSX;
            //ViewBag.Hangsx = TenHSX;
            if (fileUpload != null)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/AnhSP"), fileName);
                
                // kiểm tra đường dẫn
                if (System.IO.File.Exists(path))
                {

                    return RedirectToAction("ThemMoi", "QuanLySP", new { id = sanpham.idHSX, thongbao = "Hình ảnh đã tồn tại" });
                }
                else
                {
                    fileUpload.SaveAs(path);
                    sanpham.Hinh = fileUpload.FileName;
                }
            }
            else
            {
                sanpham.Hinh = "macdinh.png";
            }
            ViewBag.hsx = new SelectList(db.LayHangsx(), "idHSX", "TenHSX");
            if(sanpham.TenSP==null||sanpham.Gia==null||sanpham.TonKho==null)
            {
                ViewBag.ThongbaoAll = "Bạn phải nhập đầy đủ thông tin";
                ViewBag.hsx = db.LayHangsx();
                ViewBag.idhsx = sanpham.idHSX;
                return View();
            }
            else
            {
                
                sanpham.NgayDang = DateTime.Now;
                sanpham.SoLanMua = 0;
                sanpham.SoLanXem = 0;
                sanpham.AnHien = 1;
                sanpham.idLoai = 1;
                sanpham.TonKho = int.Parse(f["TonKho"]);
                sanpham.MoTa = f["MoTa"].ToString();
                sanpham.TenSP = f["TenSP"];
                db.ThemMoi(sanpham);
            }
            return RedirectToAction("Index", new { id=sanpham.idHSX}); 
        }
        [ValidateInput(false)]
        [HttpGet]
        public ActionResult Sua(int idSP, string thongbao)
        {
            if (Session["Admin"] == null) { return RedirectToAction("Index", "Admin"); }
            else
            {

                SanPham sp = db.Sua(idSP);
                ViewBag.hsx = new SelectList(db.LayHangsx(), "idHSX", "TenHSX", sp.idHSX);
                ViewBag.idhsx = sp.idHSX;
                ViewBag.thongbao = thongbao;
                //List<SelectListItem> AnHien = new List<SelectListItem>();
                //AnHien.Add(new SelectListItem { Text = "Hiện", Value = "1" });
                //AnHien.Add(new SelectListItem { Text = "Ẩn", Value = "0" });
                //ViewBag.AnHien = AnHien;
                return View(sp);
            }
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Sua(SanPham sanpham,int id, HttpPostedFileBase fileUpload, FormCollection f)
        {
            sanpham.idSP = id;
            int idHSX = int.Parse(f["hsx"]);
            sanpham.idHSX = idHSX;
            sanpham.AnHien = int.Parse(f["AnHien"]);
            ViewBag.hsx = new SelectList(db.LayHangsx(), "idHSX", "TenHSX", sanpham.idHSX);

            //List<SelectListItem> AnHien = new List<SelectListItem>();
            //AnHien.Add(new SelectListItem { Text = "Hiện", Value = "1" });
            //AnHien.Add(new SelectListItem { Text = "Ẩn", Value = "0" });
            //ViewBag.AnHien = AnHien;
            if (fileUpload != null)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                //lưu đường dẫn
                var path = Path.Combine(Server.MapPath("~/AnhSP"), fileName);
                
                // kiểm tra đường dẫn
                if (System.IO.File.Exists(path))
                {
                   
                    return RedirectToAction("Sua", "QuanLySP", new { idSP = id, thongbao = "Hình ảnh đã tồn tại" });
                }
                else
                {
                    fileUpload.SaveAs(path);
                    sanpham.Hinh = fileUpload.FileName;
                }
            }
            else {
                sanpham.Hinh = db.LaySP().Single(n => n.idSP == id).Hinh;
            }
            db.ChinhSua(sanpham);
            return RedirectToAction("Index", new {id=idHSX }); 
        }
        

       public ActionResult HsxPartial()
       {
           
           ViewBag.hsx = db.LayHangsx();
           return PartialView();
       }
    }
}