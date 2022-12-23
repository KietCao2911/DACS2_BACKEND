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
        public bool status { get; set; } = false;
        public decimal VAT { get; set; } = 00;
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    }
}
