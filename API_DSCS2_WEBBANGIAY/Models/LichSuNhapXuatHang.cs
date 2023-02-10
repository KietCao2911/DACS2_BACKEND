using System;
using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class LichSuNhapXuatHang
    {
        public LichSuNhapXuatHang()
        {
            KhoHangs = new HashSet<ChiNhanh_SanPham>();
        }

        public int ID { get; set; } 
        public string Name { get; set; }
        public int? qtyChange { get; set; } = 0;
        public DateTime createdAT { get; set; } = DateTime.Now;
        public DateTime updatedAT { get; set; } = DateTime.Now;
        public virtual ICollection<ChiNhanh_SanPham> KhoHangs { get; set; }
    }
}
