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
        public int SoLuong { get; set; } = 0;
        public virtual SanPham SanPhamNavigation { get; set; }
        public virtual PhieuNhap PhieuNhapNavigation { get; set; }


    }
}
