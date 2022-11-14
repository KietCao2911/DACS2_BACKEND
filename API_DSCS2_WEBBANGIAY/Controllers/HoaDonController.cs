using API_DSCS2_WEBBANGIAY.Models;
using API_DSCS2_WEBBANGIAY.Utils.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;
        private readonly IMailService mailService;

        public HoaDonController(ShoesEcommereContext context, IMailService mailService)
        {
            this.mailService = mailService;
            _context = context;
        }

        [HttpPost("PostWithGuess")]
        public async Task<IActionResult> PostWithGuess([FromBody] DonhangModel body)
        {
            using (var _context = new ShoesEcommereContext())
            {
                try
                {
                    DiaChi DiaChi = new DiaChi();
                    HoaDon hd = new HoaDon();
                    foreach (var item in body.hoaDonDetails)
                    {
                        var ctsl = await _context.SoLuongDetails.FirstOrDefaultAsync(x => x.maSanPham == item.MasanPham && x.maMau == item.Color && x._idSize == item.Size);
                        if (ctsl is not null)
                        {
                            if (ctsl.Soluong <=0  && (ctsl.Soluong - item.Qty < 0))
                            {
                                return BadRequest();
                            }
                            else
                            {
                                ctsl.Soluong -= item.Qty;
                                _context.SoLuongDetails.Update(ctsl);
                            }
                          
                        }
                    }

                    DiaChi = body.DiaChi;
                    _context.DiaChis.Add(DiaChi);
                    await _context.SaveChangesAsync();
                    hd.IdDiaChi = DiaChi.ID;
                    hd.Thanhtien = (decimal)body.totalPrice;
                    hd.Phiship = (decimal)body.phiShip;
                    hd.Giamgia = body.giamGia;
                    _context.HoaDons.Add(hd);
                   
                    await _context.SaveChangesAsync();
                    foreach (var item in body.hoaDonDetails)
                    {
                                ChiTietHoaDon cthd = new ChiTietHoaDon();
                                cthd = item;
                                cthd.IdHoaDon = hd.Id;
                                cthd.MasanPham = item.MasanPham.Trim();
                                cthd.Color = item.Color;
                                cthd.Size = item.Size;
                                _context.ChiTietHoaDons.Add(cthd);
                    }
                    await _context.SaveChangesAsync();
                    var cthdTemp = _context.ChiTietHoaDons.Include(x=>x.MasanPhamNavigation).Where(x => x.IdHoaDon == hd.Id).ToList();
                    string cthdString = TemplateConfirmcs.DetailsOrder(cthdTemp);
                    await mailService.SendEmailAsync(new MailRequest { ToEmail = "truongkiet.hn289@gmail.com", Subject = "Xác nhận đơn hàng", Body = TemplateConfirmcs.Teamplate(hd, cthdString) });
                    hd.status = 0;
                    await _context.SaveChangesAsync();
                    return Ok();

                }
                catch (Exception err)
                {
                    return BadRequest(err.Message);
                }
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
                hd.Thanhtien = (decimal)body.totalPrice;
                hd.Phiship = (decimal)body.phiShip;
                hd.Giamgia = (decimal)body.giamGia;
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
