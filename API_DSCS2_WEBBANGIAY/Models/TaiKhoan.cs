using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public string TenTaiKhoan { get; set; }
        public int Id { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public int? Role { get; set; }
        public int? idKH { get; set; }

        public virtual KhachHang SdtKhNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
