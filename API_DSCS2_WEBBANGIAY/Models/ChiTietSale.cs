using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class ChiTietSale
    {
        public int IdSale { get; set; }
        public string MaSanPham { get; set; }
        public int? Giamgia { get; set; }

        public virtual Sale IdSaleNavigation { get; set; }
        public virtual SanPham MaSanPhamNavigation { get; set; }
    }
}
