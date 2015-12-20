using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreLaptop.Models;
using StoreLaptop.ServiceReference;
using StoreLaptop.Controllers;

namespace StoreLaptop.Controllers
{
    public class GiohangController : Controller
    {
        Service1Client db = new Service1Client();
        // GET: Giohang
        //Lay gio hang
        #region Giỏ Hàng
        public List<Giohang> LayGioHang()
        {
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;
            if (listGioHang == null)
            {
                listGioHang = new List<Giohang>();
                Session["Giohang"] = listGioHang;
            }
            return listGioHang;
        }
        //gio hang them 1
        public ActionResult ThemGioHangs(int sidSP, string strURL)
        {
            SanPham sp = db.LaySPtheoid(sidSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if(sp.TonKho == 0)
            {
                ViewBag.hethang = "Xin lỗi quý khách! Sản phẩm này đã hết hàng!";
                return View();
            }

            List<Giohang> listGioHang = LayGioHang();
            Giohang sanpham = listGioHang.Find(x => x.sidSP == sidSP);
            if (sanpham == null)
            {
                sanpham = new Giohang(sidSP);
                //sanpham.sSoLuong++;
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.sSoLuong++;
                return Redirect(strURL);
            }
        }
        //gio hang them
        public ActionResult ThemGioHang(int sidSP, string strURL, FormCollection f)
        {
            if(f["txtsoluong"] =="")
            {
                return RedirectToAction("XemChiTiet", "SanPham", new { idSP = sidSP, thongbao = "Không được để trống số lượng" });
            }
            SanPham sp = db.LaySPtheoid(sidSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (sp.TonKho == 0)
            {
                ViewBag.hethang = "Xin lỗi quý khách! Sản phẩm này đã hết hàng!";
                return RedirectToAction("XemChiTiet","SanPham",new { idSP = sp.idSP}) ;
            }
            List<Giohang> listGioHang = LayGioHang();
            
           Giohang sanpham = listGioHang.Find(x => x.sidSP == sidSP);
            if (sanpham == null)
            {
                sanpham = new Giohang(sidSP);
                sanpham.sSoLuong = int.Parse((f["txtsoluong"]).ToString());
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.sSoLuong += int.Parse((f["txtsoluong"]).ToString());
                return Redirect(strURL);
            }
        }
        //cap nhat gio hang
        public ActionResult CapNhatGioHang(int sidSP, FormCollection f)
        {
            SanPham sp = db.LaySPtheoid(sidSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Giohang> listGioHang = LayGioHang();
            Giohang sanpham = listGioHang.SingleOrDefault(x => x.sidSP == sidSP);
            if (sanpham != null)
            {
                sanpham.sSoLuong = int.Parse((f["txtsoluong"]).ToString());
            }
            return RedirectToAction("SuaGioHang");
        }
        //xoa gio hang
        public ActionResult XoaGioHang(int sidSP)
        {
            SanPham sp = db.LaySPtheoid(sidSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Giohang> listGioHang = LayGioHang();
            Giohang sanpham = listGioHang.SingleOrDefault(x => x.sidSP == sidSP);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(x => x.sidSP == sidSP);

            }
            else { Session.Remove("GioHang"); }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult GioHang()
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Giohang> listGioHang = LayGioHang();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        private int TongSoLuong()
        {
            int TongSoLuong = 0;
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;
            if (listGioHang != null)
            {
                TongSoLuong = listGioHang.Sum(x => x.sSoLuong);
            }
            return TongSoLuong;
        }
        private double TongTien()
        {
            double TongTien = 0;
            List<Giohang> listGioHang = Session["Giohang"] as List<Giohang>;
            if (listGioHang != null)
            {
                TongTien = listGioHang.Sum(x => x.ThanhTien);
                
            }
            return TongTien;
        }
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.tt = TongTien();
            return PartialView();
        }
        public ActionResult SuaGioHang()
        {
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> listGioHang = LayGioHang();
            return View(listGioHang);
        }
        #endregion Gio Hang
        #region Đặt hàng
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {            
            if (Session["taikhoan"] == null || Session["taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            if (f["Email"] == "" || f["DiaDiemGiao"] == "" || f["TenNguoiNhan"] == "" || f["DienThoai"] == "")
            {

                return RedirectToAction("TTDatHang", "TTDatHang", new { thongbao = "Bạn phải nhập đủ thông tin!" });
            }
            else
            { 
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["taikhoan"];
            List<Giohang> gh = LayGioHang();
            
            ddh.Email = f["Email"];
            ddh.DiaDiemGiao = f["DiaDiemGiao"];
            ddh.TenNguoiNhan = f["TenNguoiNhan"];
            ddh.DienThoai = f["DienThoai"];
            ddh.GhiChu = f["GhiChu"];
            ddh.TinhTrang = 1;
            ddh.NgayGiao = DateTime.Now.AddDays(2);
            ddh.idKH = kh.idKH;
            ddh.ThoiGianDat = DateTime.Now;
            ddh.TinhTrang = 1;
           
            int check = db.ThemDonHang(ddh);
            //var id =;
            //
            if(check != -1)
            { 
                var idDH = db.Lay1DH();
                
                foreach (var item in gh)
                {
                    DonHangChiTiet ctdh = new DonHangChiTiet();
                    foreach (var i in idDH)
                    {
                    ctdh.idDH = i.idDH;                                      
                    ctdh.idSP = item.sidSP;
                    ctdh.SoLuong = item.sSoLuong;
                    ctdh.Gia = (decimal)item.sGia;
                    db.ThemCTDH(ctdh);
                    }
                    //db.SaveChanges();
                }
            }
            Session.Remove("Giohang");
            return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}