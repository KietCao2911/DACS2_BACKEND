﻿using System;
using System.Collections.Generic;

namespace API_DSCS2_WEBBANGIAY.Models
{
    public class PhieuNhapXuat
    {
        public PhieuNhapXuat()
        {
            ChiTietNhapXuats = new HashSet<ChiTietNhapXuat>();
        }

        public int Id { get; set; }
        public int? IDNCC { get; set; }
        public decimal? ThanhTien { get; set; } = 0;
        public string? LoaiPhieu { get; set; }
        public bool? DaNhapHang { get; set; } = false;
        public bool? DaThanhToan { get; set; } = false;
        public decimal? TienDaThanhToan { get; set; }
        public decimal? TienDaGiam { get; set; }    
        public string? PhuongThucThanhToan { get; set; }
        public int? ChietKhau { get; set; } = 0;
        public decimal? Phiship { get; set; } = 0;
        public string? idTaiKhoan { get; set; }
        public int? idKH { get; set; }
        public int? IdDiaChi { get; set; }
        public int? TongSoLuong { get; set; }
        public int? DeliveryStatus { get; set; }
        public int? status { get; set; } = 0;
        public int? steps { get; set; } = 0;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public virtual TaiKhoan TenTaiKhoanNavigation { get; set; }
        public virtual DiaChi DiaChiNavigation { get; set; }
        public virtual KhachHang KhachHangNavigation { get; set; }
        public virtual NCC NhaCungCapNavigation { get; set; }
        public virtual LoaiPhieu LoaiPhieuNavigation { get; set; }
        public virtual ICollection<ChiTietNhapXuat> ChiTietNhapXuats { get; set; }
    }
}
