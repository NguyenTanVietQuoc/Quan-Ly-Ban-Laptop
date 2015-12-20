using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LaptopService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        QuanLyLaptopDataContext db = null;
        public Service1()
        {
            db = new QuanLyLaptopDataContext();
        }

        List<SanPham> IService1.LaySPindex()
        {
            var sp = db.SanPhams.Take(9).OrderBy(x => x.Gia).Where(x=>x.AnHien ==1).ToList();
            return sp;
        }
        //public List<ChungLoai> LayCL()
        //{
        //    var sp = db.ChungLoais.ToList();
        //    return sp;
        //}
        List<SanPham> IService1.LaySPmoi()
        {
            var sp = db.SanPhams.Take(8).OrderByDescending(x => x.idSP).ToList();
            return sp;
        }
        List<HangSanXuat> IService1.LayHSX()
        {
            var sp = db.HangSanXuats.ToList();
            return sp;
        }
        HangSanXuat IService1.KiemtraHSX(int idHSX)
        {
            var i = db.HangSanXuats.SingleOrDefault(x => x.idHSX == idHSX);

            return i;
        }
        List<SanPham> IService1.LaySPtheoHSX(int idHSX)
        {
            var sp = db.SanPhams.Where(x => x.idHSX == idHSX).OrderBy(n => n.Gia).ToList();
            if(sp == null)
            {
                return null;
            }
            //if (sp.Count == 0)
            //{
            //cho test
            //}
            return sp;
        }
        SanPham IService1.ChiTietSP(int idSP)
        {
            var i = db.SanPhams.SingleOrDefault(x => x.idSP == idSP);
            i.SoLanXem++;           
            db.SubmitChanges();
            return i;
            
          
        }
        public int ThemKH(KhachHang kh)
        {
            try
            {
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        public int KiemTraTK(string Email)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(x => x.Email == Email);
            if (kh == null)
            {
                return 0;
            }
            return 1;
        }
        KhachHang IService1.KTDangNhap(string taikhoan, string matkhau)
        {
            KhachHang kh = db.KhachHangs.SingleOrDefault(x => x.Email == taikhoan && x.MatKhau == matkhau);
            if (kh == null)
            {
                return null;
            }
            return kh;
        }
        public SanPham LaySPcart(int idSP)
        {
            var k = db.SanPhams.SingleOrDefault(x => x.idSP == idSP);
            return k;
        }
        public SanPham LaySPtheoid(int sidSP)
        {
            var p = db.SanPhams.SingleOrDefault(x => x.idSP == sidSP);
            return p;
        }
        public int ThemDonHang(DonHang dh)
        {
            try
            {
                db.DonHangs.InsertOnSubmit(dh);
                db.SubmitChanges();
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        public int ThemCTDH(DonHangChiTiet ctdh)
        {
            SanPham sp = db.SanPhams.Single(x => x.idSP == ctdh.idSP);
            sp.SoLanMua++;
            sp.TonKho -= ctdh.SoLuong;
            db.SubmitChanges();
            try
            {
                db.DonHangChiTiets.InsertOnSubmit(ctdh);
                db.SubmitChanges();
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        //Admin
        //List<SanPham> IService1.LaySP()
        //{


        //    var sp = db.SanPhams.ToList();
        //    return sp;

        //}

        //List<HangSanXuat> IService1.LayHangsx()
        //{


        //    var hsx = db.HangSanXuats.ToList();
        //    return hsx;

        //}

        //public int ThemMoi(SanPham sanpham)
        //{
        //    try
        //    {
        //        db.SanPhams.InsertOnSubmit(sanpham);
        //        db.SubmitChanges();
        //    }
        //    catch { return -1; }
        //    return 1;
        //}

        //SanPham IService1.Sua(int idSP)
        //{
        //    SanPham sanpham = db.SanPhams.SingleOrDefault(x => x.idSP == idSP);
        //    if (sanpham == null)
        //    { return null; }
        //    else { return sanpham; }
        //}





        //public int ChinhSua(SanPham sanpham)
        //{
        //    try
        //    {
        //        SanPham sp = db.SanPhams.Single(x => x.idSP == sanpham.idSP);
        //        sp.TenSP = sanpham.TenSP;
        //        sp.Gia = sanpham.Gia;
        //        sp.MoTa = sanpham.MoTa;
        //        sp.TonKho = sanpham.TonKho;
        //        sp.AnHien = sanpham.AnHien;
        //        sp.idHSX = sanpham.idHSX;
        //        sp.Hinh = sanpham.Hinh;
        //        db.SubmitChanges();
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //    return 1;
        //}


        //public List<DonHang> LayDH()
        //{
        //    var dh = db.DonHangs.ToList();
        //    return dh;
        //}

        //public DonHang Lay(int idDH)
        //{
        //    DonHang donhang = db.DonHangs.SingleOrDefault(x => x.idDH == idDH);
        //    if (donhang == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return donhang;
        //    }

        //}

        //public int SuaTinhTrang(DonHang donhang)
        //{
        //    try
        //    {
        //        DonHang dh = db.DonHangs.Single(n => n.idDH == donhang.idDH);
        //        if (donhang.TinhTrang == 0)
        //        {
        //            return -1;
        //        }
        //        else
        //        {
        //            dh.TinhTrang = donhang.TinhTrang;
        //            db.SubmitChanges();
        //        }
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //    return 1;
        //}





        //public Admin XuLy_DangNhap(string tk, string mk)
        //{
        //    Admin ad = db.Admins.SingleOrDefault(n => n.TaiKhoan == tk && n.MatKhau == mk);
        //    if (ad == null)
        //    {
        //        return null;
        //    }
        //    return ad;
        //}


        //public List<KhachHang> LayKH()
        //{
        //    var kh = db.KhachHangs.ToList();
        //    return kh;
        //}


        //public int Duyet(KhachHang khachhang)
        //{
        //    try
        //    {
        //        KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.idKH == khachhang.idKH);
        //        kh.KichHoat = 1;
        //        db.SubmitChanges();
        //    }
        //    catch { return -1; }
        //    return 1;
        //}


        //public List<SanPham> Timkiem(string TuKhoa)
        //{
        //    var sp = db.SanPhams.Where(n => n.TenSP.Contains(TuKhoa)).ToList();
        //    return sp;
        //}
        //endadmin

        //public List<LoaiSP> LayLoaiSP()
        //{

        //    return db.LoaiSPs.ToList();
        //}


        //public List<LoaiSP> LayCLtheoL()
        //{
        //    return db.LoaiSPs.ToList();
        //}











        //int IService1.ThemKH(KhachHang kh)
        //{
        //    throw new NotImplementedException();
        //}

        //int IService1.KiemTraTK(string Email)
        //{
        //    throw new NotImplementedException();
        //}

        //admin new
        List<SanPham> IService1.LaySPHsx(int idHSX)
        {


            var sp = db.SanPhams.Where(n => n.idHSX == idHSX).ToList();
            return sp;

        }
        public List<SanPham> LaySP()
        {
            var sp = db.SanPhams.ToList();
            return sp;
        }

        List<HangSanXuat> IService1.LayHangsx()
        {


            var hsx = db.HangSanXuats.ToList();
            return hsx;

        }

        public int ThemMoi(SanPham sanpham)
        {
            try
            {
                db.SanPhams.InsertOnSubmit(sanpham);
                db.SubmitChanges();
            }
            catch { return -1; }
            return 1;
        }

        SanPham IService1.Sua(int idSP)
        {
            SanPham sanpham = db.SanPhams.SingleOrDefault(x => x.idSP == idSP);
            if (sanpham == null)
            { return null; }
            else { return sanpham; }
        }





        public int ChinhSua(SanPham sanpham)
        {
            try
            {
                SanPham sp = db.SanPhams.Single(x => x.idSP == sanpham.idSP);
                sp.TenSP = sanpham.TenSP;
                sp.Gia = sanpham.Gia;
                sp.MoTa = sanpham.MoTa;
                sp.TonKho = sanpham.TonKho;
                sp.AnHien = sanpham.AnHien;
                sp.idHSX = sanpham.idHSX;
                sp.Hinh = sanpham.Hinh;
                sp.SoLanMua = sanpham.SoLanMua;
                db.SubmitChanges();
            }
            catch
            {
                return -1;
            }
            return 1;
        }


        public List<DonHang> LayDH()
        {
            var dh = db.DonHangs.ToList();
            return dh;
        }

        public DonHang Lay(int idDH)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(x => x.idDH == idDH);
            if (donhang == null)
            {
                return null;
            }
            else
            {
                return donhang;
            }

        }

        public int SuaTinhTrang(DonHang donhang)
        {
            try
            {
                DonHang dh = db.DonHangs.Single(n => n.idDH == donhang.idDH);
                if (donhang.TinhTrang == 0)
                {
                    return -1;
                }
                else
                {
                    dh.TinhTrang = donhang.TinhTrang;
                    db.SubmitChanges();
                }
            }
            catch
            {
                return -1;
            }
            return 1;
        }





        public Admin XuLy_DangNhap(string tk, string mk)
        {
            Admin ad = db.Admins.SingleOrDefault(n => n.TaiKhoan == tk && n.MatKhau == mk);
            if (ad == null)
            {
                return null;
            }
            return ad;
        }


        public List<KhachHang> LayKH()
        {
            var kh = db.KhachHangs.ToList();
            return kh;
        }


        public int Duyet(KhachHang khachhang)
        {
            try
            {
                KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.idKH == khachhang.idKH);
                kh.KichHoat = 1;
                db.SubmitChanges();
            }
            catch { return -1; }
            return 1;
        }


        public List<SanPham> Timkiem(string TuKhoa)
        {
            var sp = db.SanPhams.Where(n => n.TenSP.Contains(TuKhoa)).ToList();
            return sp;
        }





        public int ThemHSX(HangSanXuat HSX)
        {
            try
            {
                db.HangSanXuats.InsertOnSubmit(HSX);
                db.SubmitChanges();
            }
            catch { return -1; }
            return 1;
        }


        public int SuaHSX(HangSanXuat HSX)
        {
            try
            {
                HangSanXuat hsx = db.HangSanXuats.Single(n => n.idHSX == HSX.idHSX);
                hsx.TenHSX = HSX.TenHSX;
                hsx.AnHien = HSX.AnHien;
                db.SubmitChanges();
            }
            catch { return -1; }
            return 1;
        }



        //Lấy đơn hàng chi tiết
        public List<DonHangChiTiet> LayDHCT(int id)
        {
            var dhct = db.DonHangChiTiets.Where(n => n.idDH == id).ToList();
            return dhct;
        }


        public int XoaKH(KhachHang khachhang)
        {
            try
            {
                KhachHang kh = db.KhachHangs.Single(n => n.idKH == khachhang.idKH);
                db.KhachHangs.DeleteOnSubmit(kh);
                db.SubmitChanges();
            }
            catch { return 0; }
            return 1;
        }
        //aand admin









        public List<ChungLoai> LayChungLoai()
        {
            return db.ChungLoais.ToList();
        }


        public List<LoaiSP> LayLoaiSP()
        {
            return db.LoaiSPs.ToList();
        }


        public LoaiSP KTLoaiSP(int idLoai)
        {
            var i = db.LoaiSPs.SingleOrDefault(x => x.idLoai == idLoai);
            return i;
        }


        public List<SanPham> LaySPtheoLoai(int idLoai)
        {
            var sp = db.SanPhams.Where(x => x.idLoai == idLoai).OrderBy(x=>x.Gia).ToList();
            if(sp == null)
            {
                return null;
            }
            return sp;
        }


        public List<SanPham> LaySPcungloai(int idLoai, int idSP)
        {
            var sp = db.SanPhams.Where(x => x.idLoai == idLoai && x.idSP != idSP).Take(5).OrderBy(x=>x.Gia).ToList();
            return sp;
        }





        public List<DonHang> Lay1DH()
        {
            var dh = db.DonHangs.OrderByDescending(n => n.idDH).Take(1).ToList();
            return dh;
        }
        public KhachHang KTKH(int idKH)
        {
            var i = db.KhachHangs.Single(x => x.idKH == idKH);
            return i;
        }
        public int DoiMK(KhachHang kh)
        {            
            try
            { 
                KhachHang khachhang = db.KhachHangs.Single(x => x.idKH == kh.idKH);
                khachhang.MatKhau = kh.MatKhau;
                db.SubmitChanges();
            }
            catch
            {
                return -1;
            }
            return 1;
        }





        public KhachHang Lay1KH(int idKH)
        {
           var kh = db.KhachHangs.SingleOrDefault(x=>x.idKH==idKH);
            return kh;
        }


        public int SuaKH(KhachHang kh)
        {
            try { 
                var h = db.KhachHangs.Single(x => x.idKH == kh.idKH);
                h.NgaySinh = kh.NgaySinh;
                h.HoTen = kh.HoTen;
                h.Email = kh.Email;
                h.DienThoai = kh.DienThoai;
                h.DiaChi = kh.DiaChi;
                h.GioiTinh = kh.GioiTinh;
                db.SubmitChanges();
                }
            catch
            {
                return -1;
            }
            return 1;
        }


        public List<DonHang> LayDHTheoKH(int idKH)
        {
            var dh = db.DonHangs.Where(x => x.idKH == idKH).ToList();
            return dh;
        }


        //public Object LayCTDHKH(int idDH)
        //{
        //    //DonHangChiTiet ctdh = db.DonHangChiTiets.ToString();
        //    //SanPham sp
           
        //        var donhang = db.DonHangChiTiets.Where(x => x.idDH == idDH).ToList();
        //        var sanpham = db.SanPhams.ToList();

        //        var ct = (from ctdh in donhang
        //                  join sp in sanpham on ctdh.idSP equals sp.idSP
        //                  select new
        //                  {
        //                      sp.TenSP,
        //                      sp.Hinh,
        //                      ctdh.Gia,
        //                      ctdh.SoLuong
        //                  }
        //                  ).ToList();
           
        //        return ct;
            
        //}


        public DonHang CapNhatDonHang(int idDH)
        {
            var i = db.DonHangs.Single(x => x.idDH == idDH);
            i.TinhTrang = 4;
            db.SubmitChanges();
            return i;
        }
    }
}
