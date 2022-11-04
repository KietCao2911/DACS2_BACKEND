using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class HinhAnh
    {
        public HinhAnh()
        {            ChiTietHinhAnhs = new HashSet<ChiTietHinhAnh>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }

        public virtual ICollection<ChiTietHinhAnh> ChiTietHinhAnhs { get; set; }
    }
}
