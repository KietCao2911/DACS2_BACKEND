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
        public int IDNCC { get; set; } 
        public DateTime NgayNhap { get; set; } = DateTime.Now;  
        public string Dvt { get; set; }
        public decimal ThanhTien { get; set; }    
        public int TongSoLuong { get; set; } = 0;
        public int status { get; set; } = 0;
        public int steps { get; set; } = 0;
        public bool DaNhapHang { get; set; } = false;
        public bool DaThanhToan { get; set; } = false;
        public decimal ChietKhau { get; set; } = 00;
        public decimal? TienDaThanhToan { get; set; } = 0;
        public virtual NCC NhaCungCapNavigation { get; set; }
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    }
}
