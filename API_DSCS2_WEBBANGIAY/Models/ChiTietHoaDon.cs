using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class ChiTietHoaDon
    {
        public int IdHoaDon { get; set; }
        public int _Id { get; set; }
        public int IDSanPham { get; set; }
        public string MaSanPham { set; get; }
        public int? Qty { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal? giaBan { get; set; } = 0;
        public string? img { get; set; }    
        public virtual HoaDon IdHoaDonNavigation { get; set; }
        public virtual SanPham MasanPhamNavigation { get; set; }
        public virtual MauSac MausacPhamNavigation { get; set; }
        //public virtual Size SizePhamNavigation { get; set; }
    }
}
