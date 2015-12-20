using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreLaptop.ServiceReference;
 
namespace StoreLaptop.Models
{
    public class Giohang
    {
        Service1Client db = new Service1Client();
        public int sidSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinh { get; set; }
        public double sGia { get; set; }
        public int sSoLuong { get; set; }
        public double ThanhTien
        {
            get { return sSoLuong * sGia; }

        }
        public Giohang(int idSP)
        {
            sidSP = idSP;
            SanPham sp = db.LaySPcart(idSP);
            sTenSP = sp.TenSP;
            sHinh = sp.Hinh;
            sGia = double.Parse(sp.Gia.ToString());
            sSoLuong = 1;
        }
    }
}