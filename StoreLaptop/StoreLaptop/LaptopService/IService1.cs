using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LaptopService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        List<SanPham> LaySPindex();

        [OperationContract]
        List<LoaiSP> LayLoaiSP(); 
        //[OperationContract]
        //List<ChungLoai> LayCL();

        //lay 1 don hang moi nhat
        [OperationContract]
        List<DonHang> Lay1DH();
        //[OperationContract]
        //List<LoaiSP> LayCLtheoL();

        //[OperationContract]
        //List<LoaiSP> LayLoaiSP();

        [OperationContract]
        List<SanPham> LaySPmoi();

        [OperationContract]
        List<HangSanXuat> LayHSX();

        [OperationContract]
        HangSanXuat KiemtraHSX(int idHSX);

        [OperationContract]
        List<SanPham> LaySPtheoHSX(int idHSX);

        [OperationContract]
        SanPham ChiTietSP(int idSP);

        [OperationContract]
        int ThemKH(KhachHang kh);

        [OperationContract]
        int ThemDonHang(DonHang dh);

        [OperationContract]
        int KiemTraTK(string Email);

        [OperationContract]
        KhachHang KTDangNhap(string taikhoan, string matkhau);

        [OperationContract]
        SanPham LaySPcart(int idSP);

        [OperationContract]
        SanPham LaySPtheoid(int sidSP);

        [OperationContract]
        int ThemCTDH(DonHangChiTiet ctdh);

        [OperationContract]
        List<ChungLoai> LayChungLoai();

        [OperationContract]
        LoaiSP KTLoaiSP(int idLoai);

        [OperationContract]
        List<SanPham> LaySPtheoLoai(int idLoai);

        [OperationContract]
        List<SanPham> LaySPcungloai(int idLoai, int idSP);
        // TODO: Add your service operations here
        //Admin
        //[OperationContract]
        //List<SanPham> LaySP();

        //[OperationContract]
        //List<HangSanXuat> LayHangsx();

        //[OperationContract]
        //int ThemMoi(SanPham sanpham);

        //[OperationContract]
        //SanPham Sua(int idSP);

        //[OperationContract]

        //int ChinhSua(SanPham sanpham);

        //[OperationContract]
        //List<DonHang> LayDH();

        //[OperationContract]
        //DonHang Lay(int idDH);

        //[OperationContract]
        //int SuaTinhTrang(DonHang donhang);

        //[OperationContract]
        //Admin XuLy_DangNhap(string tk, string mk);

        //[OperationContract]

        //List<KhachHang> LayKH();

        //[OperationContract]
        //int Duyet(KhachHang khachhang);
        //// TODO: Add your service operations here
        //[OperationContract]
        //List<SanPham> Timkiem(String TuKhoa);
        //end admin
        //admin new
        [OperationContract]
        List<SanPham> LaySPHsx(int idHSX);

        [OperationContract]
        List<SanPham> LaySP();

        [OperationContract]
        List<HangSanXuat> LayHangsx();

        [OperationContract]
        int ThemMoi(SanPham sanpham);

        [OperationContract]
        SanPham Sua(int idSP);

        [OperationContract]

        int ChinhSua(SanPham sanpham);

        [OperationContract]
        List<DonHang> LayDH();

        [OperationContract]
        DonHang Lay(int idDH);

        [OperationContract]
        int SuaTinhTrang(DonHang donhang);

        [OperationContract]
        Admin XuLy_DangNhap(string tk, string mk);

        [OperationContract]

        List<KhachHang> LayKH();

        [OperationContract]
        int Duyet(KhachHang khachhang);
        // xóa khách hàng
        [OperationContract]
        int XoaKH(KhachHang khachhang);



        [OperationContract]
        List<SanPham> Timkiem(String TuKhoa);

        [OperationContract]

        int ThemHSX(HangSanXuat HSX);

        //sua hang san xuat
        [OperationContract]

        int SuaHSX(HangSanXuat HSX);

        //Sua tất cả sản phẩm thành ẩn
        //Lấy đơn hàng chi tiết
        [OperationContract]
        List<DonHangChiTiet> LayDHCT(int id);
        //end admin
        [OperationContract]
        KhachHang KTKH(int idKH);

        [OperationContract]
        int DoiMK(KhachHang kh);

        [OperationContract]
        KhachHang Lay1KH(int idKH);

        [OperationContract]
        int SuaKH(KhachHang kh);

        [OperationContract]
        List<DonHang> LayDHTheoKH(int idKH);

        //[OperationContract]
        //Object LayCTDHKH(int idDH);

        [OperationContract]
        DonHang CapNhatDonHang(int idDH);

      
    }
    

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    
    
}
