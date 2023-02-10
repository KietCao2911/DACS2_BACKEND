using System;
using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
        }
        public int ID { get; set; }
        public string  maPhieuNhap { get; set; }
        public DateTime NgayNhap { get; set; } = DateTime.Now;  
        public string Dvt { get; set; }
        public decimal TongSoLuong { get; set; }
        public int SoMatHang { get; set; } = 0;
        public int status { get; set; } = 0;
        public decimal VAT { get; set; } = 00;
        public decimal? DaThanhToan { get; set; } = 0;

        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    }
}
