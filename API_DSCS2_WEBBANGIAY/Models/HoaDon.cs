using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int Id { get; set; }
        public decimal Thanhtien { get; set; }
        public decimal? Giamgia { get; set; }
        public decimal Phiship { get; set; }
        public string? idTaiKhoan { get; set; }
        public int? idKH { get; set; }
        public virtual TaiKhoan TenTaiKhoanNavigation { get; set; }
        public virtual KhachHang KhachHangNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
