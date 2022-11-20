using System;
using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class PhieuNhap
    {
        public PhieuNhap()
        {
            SanPham  = new HashSet<SanPham>();
        }

        public string  maPhieuNhap { get; set; }
        public DateTime NgayNhap { get; set; }
        public string Dvt { get; set; }
        public virtual ICollection<SanPham> SanPham { get; set; }

    }
}
