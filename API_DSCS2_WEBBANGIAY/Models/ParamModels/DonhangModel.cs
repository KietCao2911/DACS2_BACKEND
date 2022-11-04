using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public class DonhangModel
    {
        public KhachHang KhachHang { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
        public List<ChiTietHoaDon> hoaDonDetails { get; set; }
        public decimal totalPrice { get; set; }
        public int totalQty { get; set; }
        public decimal phiShip { get; set; }
        public decimal giamGia { get; set; }
    }
}
