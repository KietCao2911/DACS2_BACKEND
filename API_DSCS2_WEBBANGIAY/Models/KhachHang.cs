using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            TaiKhoans = new HashSet<TaiKhoan>();
            HoaDons = new HashSet<HoaDon>();
        }

        public string Sdt { get; set; }
        public int Id { get; set; }
        public string TenKhachHang { get; set; }
        public string? DiaChi { get; set; }
        public int? GiamGia { get; set; }
        public decimal? TienThanhToan { get; set; }
        public string? Email { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public bool? Gioitinh { get; set; }

        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
