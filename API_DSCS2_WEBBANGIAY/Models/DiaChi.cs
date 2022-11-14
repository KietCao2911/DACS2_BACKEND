﻿using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class DiaChi
    {
        public DiaChi()
        {
          
        }
        public int ID { get; set; }
        public int? IDKH { get; set; } //bỏ
        public string TenTaiKhoan   { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName     { get; set; }
        public string AddressDsc { get; set; }
        public int ProvinceID { get; set; }
        public int DistrictID  { get; set; }
        public int WardID { get; set; }
        public virtual KhachHang KhachHangNavigation { get; set; }
        public virtual TaiKhoan TaiKhoanNavigation { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
