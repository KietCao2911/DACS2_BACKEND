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
            //ChiTietSales = new HashSet<ChiTietSale>();
            ReviewStars = new HashSet<ReviewStar>();
            DanhMucDetails = new HashSet<DanhMucDetails>();
            ChiTietHinhAnhs = new HashSet<ChiTietHinhAnh>();
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
            RoomMessages = new HashSet<RoomMessage>();
            SanPhams = new HashSet<SanPham>();

        }

        //public string MaSanPham { get; set; }
        public int Id { get; set; }
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string? ParentID { get; set; }
        public string Slug { get; set; }
        public int? IdBst { get; set; }
        public int? GiamGia { get; set; }
        public decimal? GiaNhap { set; get; } = 0;
        public decimal? GiaBanLe { set; get; } = 0;
        public decimal? GiaBanSi { set; get; } = 0;
        public string Mota { get; set; }
        public int? IDType { get; set; }
        public int? IDBrand { get; set; }
        public int? IDVat { get; set; }
        public string? IDSize { get; set; }
        public string? IDColor { get; set; }
        public int? IDAnh { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual BoSuuTap IdBstNavigation { get; set; }
        public virtual Type TypeNavigation { get; set; }
        public virtual Brand BrandNavigation { get; set; }
        public virtual VAT VatNavigation { get; set; }
        public virtual Size SizeNavigation { get; set; }
        public virtual MauSac MauSacNavigation { get; set; }
        public virtual HinhAnh HinhAnhNavigation { get; set; }
        public virtual SanPham SanPhamNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        //public virtual ICollection<ChiTietSale> ChiTietSales { get; set; }
        public virtual ICollection<ReviewStar> ReviewStars { get; set; }
        public virtual ICollection<DanhMucDetails> DanhMucDetails { get; set; }
        public virtual ICollection<ChiTietHinhAnh> ChiTietHinhAnhs { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual ICollection<SanPham>SanPhams { get; set; }
        public virtual ICollection<RoomMessage> RoomMessages { get; set; }
        public string VND(decimal money)
        {
            var format = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
            return String.Format(format, "{0:c0}", money);
        }
    }
}
