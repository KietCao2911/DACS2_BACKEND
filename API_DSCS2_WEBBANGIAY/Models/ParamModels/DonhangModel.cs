using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public class DonhangModel
    {
        public HoaDon HoaDon { get; set; }  
        public KhachHang? KhachHang { get; set; } //bỏ   
        public DiaChi? DiaChi { get; set; }
        public List<ChiTietHoaDon>? hoaDonDetails { get; set; }
        public string? BankCode { get; set; }
    }
}
