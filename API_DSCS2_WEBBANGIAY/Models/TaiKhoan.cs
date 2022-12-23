﻿using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan()
        {
            HoaDons = new HashSet<HoaDon>();
            DiaChis = new HashSet<DiaChi>();
            Messages = new HashSet<Message>();
            RoomMessages = new HashSet<RoomMessage>();
        }
        public string? Avatar { get; set; } = "";
        public string? TenHienThi { get; set; } = "";
        public string TenTaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public int? Role { get; set; }
        public int? idKH { get; set; }
        public decimal? TienThanhToan { get; set; }
        public bool? Gioitinh { get; set; }
        public int? addressDefault { get; set; } 
        public virtual KhachHang SdtKhNavigation { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<DiaChi> DiaChis { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<RoomMessage> RoomMessages { get; set; }
    }
}
