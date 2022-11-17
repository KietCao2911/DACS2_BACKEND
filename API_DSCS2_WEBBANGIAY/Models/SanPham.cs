using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            ChiTietSales = new HashSet<ChiTietSale>();
            ReviewStars = new HashSet<ReviewStar>();
            SoLuongDetails = new HashSet<SoLuongDetails>();
            DanhMucDetails = new HashSet<DanhMucDetails>();
            ChiTietHinhAnhs = new HashSet<ChiTietHinhAnh>();
        }

        public string MaSanPham { get; set; }
        public int Id { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuongNhap { get; set; }
        public int? SoLuongTon { get; set; }
        public string? Img { get; set; }
        public string Slug { get; set; }
        public int? IdBst { get; set; }
        public int? GiamGia { get; set; }
        public decimal GiaBan { get; set; }
        public string Mota { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual BoSuuTap IdBstNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual ICollection<ChiTietSale> ChiTietSales { get; set; }
        public virtual ICollection<ReviewStar> ReviewStars { get; set; }
        public virtual ICollection<SoLuongDetails> SoLuongDetails { get; set; }
        public virtual ICollection<DanhMucDetails> DanhMucDetails { get; set; }
        public virtual ICollection<ChiTietHinhAnh> ChiTietHinhAnhs { get; set; }
        public string VND(decimal money)
        {
            var format = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(format, "{0:c0}", money);
        }
    }
}
