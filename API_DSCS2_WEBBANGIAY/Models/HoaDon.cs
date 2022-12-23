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
        public decimal TienThanhToan { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public decimal? Giamgia { get; set; }
        public decimal Phiship { get; set; }
        public string? idTaiKhoan { get; set; }
        public int? idKH { get; set; }
        public int? IdDiaChi    { get; set; }
        public int status { get; set; }
        public int totalQty { get; set; }
        public int DeliveryStatus { get; set; }
        public DateTime createdAt   { get; set; }
        public DateTime updatedAt { get; set; }
        public virtual TaiKhoan TenTaiKhoanNavigation { get; set; }
        public virtual DiaChi DiaChiNavigation { get; set; }
        public virtual KhachHang KhachHangNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
