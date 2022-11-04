using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class ChiTietHoaDon
    {
        public int IdHoaDon { get; set; }
        public string MasanPham { get; set; }
        public int? Soluong { get; set; }
        public decimal? DonGia { get; set; }

        public virtual HoaDon IdHoaDonNavigation { get; set; }
        public virtual SanPham MasanPhamNavigation { get; set; }
    }
}
