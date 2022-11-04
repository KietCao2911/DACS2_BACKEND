using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public HoaDonController(ShoesEcommereContext context)
        {
            _context = context;
        }

        [HttpPost("PostWithGuess")]
        public async Task<IActionResult> PostWithGuess([FromBody] DonhangModel body)
        {
            try
            {
                _context.KhachHangs.Add(body.KhachHang);
                await _context.SaveChangesAsync();
                HoaDon hd = new HoaDon();
                hd.idKH = body.KhachHang.Id;
                hd.Thanhtien = body.totalPrice;
                hd.Phiship = body.phiShip;
                hd.Giamgia = body.giamGia;
                _context.HoaDons.Add(hd);
                await _context.SaveChangesAsync();
                foreach (var item in body.hoaDonDetails)
                {
                    item.IdHoaDon = hd.Id;
                    item.MasanPham = item.MasanPham;
                    _context.ChiTietHoaDons.Add(item);
                }
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
        [HttpPost("PostWithUser")]
        public async Task<IActionResult> PostWithUser([FromBody] DonhangModel body)
        {
            try
            {
                
                HoaDon hd = new HoaDon();
                hd.idTaiKhoan = body.TaiKhoan.TenTaiKhoan;
                hd.idKH = body.TaiKhoan.idKH;
                hd.Thanhtien = body.totalPrice;
                hd.Phiship = body.phiShip;
                hd.Giamgia = body.giamGia;
                _context.HoaDons.Add(hd);
                await _context.SaveChangesAsync();
                foreach (var item in body.hoaDonDetails)
                {
                    item.IdHoaDon = hd.Id;
                    item.MasanPham = item.MasanPham;
                    _context.ChiTietHoaDons.Add(item);
                }
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
    }
}
