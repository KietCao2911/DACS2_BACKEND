using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class MauSac
    {
        public MauSac()
        {
            SoLuongDetails = new HashSet<SoLuongDetails>();
            ChiTietHinhAnhs = new HashSet<ChiTietHinhAnh>();
        }

        public string MaMau { get; set; }
        public string TenMau { get; set; }
        public virtual ICollection<SoLuongDetails> SoLuongDetails { get; set; }
        public virtual ICollection<ChiTietHinhAnh> ChiTietHinhAnhs { get; set; }
    }
}
