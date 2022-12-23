using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ChiTietNhapHang : ControllerBase
    {
        private readonly ShoesEcommereContext _context;
        public ChiTietNhapHang(ShoesEcommereContext context)
        {
            this._context = context;
        }
        [HttpGet("{maPN}")]
        public async Task<IActionResult> GetCTNH(string maPN)
        {
            try
            {
                var ctnh = _context.ChiTietPhieuNhaps.Include(x => x.SanPhamNavigation);
                    
                return Ok(ctnh);
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpGet("search/{s}")]
        public async Task<IActionResult> searchProducts(string s)
        {
            try
            {
                var items = _context.SanPhams
                    .Include(x => x.ChiTietHinhAnhs)
                    .ThenInclude(x => x.IdHinhAnhNavigation);
                return Ok(items);
            }catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostCTNH(ChiTietPhieuNhap pn)
        {
            try
            {
                 _context.ChiTietPhieuNhaps.Add(pn);
                await _context.SaveChangesAsync();
                return Ok(pn);
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
           
        }

        [HttpPut]
        public async Task<IActionResult> PutCTNH(ChiTietPhieuNhap pn)
        {
            try
            {
                 _context.ChiTietPhieuNhaps.Update(pn);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> PutCTNH(int id)
        {
            try
            {
                var pn = _context.ChiTietPhieuNhaps.FirstOrDefault(x => x.Id == id);
                if(pn is not null)
                {

                    _context.ChiTietPhieuNhaps.Remove(pn);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
               
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }

        }
    }
}
