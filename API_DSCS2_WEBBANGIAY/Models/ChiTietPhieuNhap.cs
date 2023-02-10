using System;
using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class ChiTietPhieuNhap
    {
        public ChiTietPhieuNhap()
        {
        }
        public string MaSanPham { get; set; }

        public int Id { get; set; }
        public int IDPN { get; set; }
        public string MaChiNhanh { get; set; }
        public int SoLuong { get; set; } = 0;
        public string logText { get; set; } = "";
        public DateTime createdAT { get; set; } = DateTime.Now;
        public DateTime updatedAT { get; set; } = DateTime.Now;
        public virtual SanPham SanPhamNavigation { get; set; }
        public virtual PhieuNhap PhieuNhapNavigation { get; set; }
        public virtual Branchs KhoHangNavigation { get; set; }

    }
}
