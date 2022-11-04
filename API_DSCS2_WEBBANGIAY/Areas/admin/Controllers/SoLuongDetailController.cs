using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_DSCS2_WEBBANGIAY.Models;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoLuongDetailController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public SoLuongDetailController(ShoesEcommereContext context)
        {
            _context = context;
        }

        // GET: api/SoLuongDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoLuongDetails>>> GetSoLuongDetails()
        {
            return await _context.SoLuongDetails.ToListAsync();
        }

        // GET: api/SoLuongDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSoLuongDetails(string id)
        {
            var soLuongDetails = await _context.SoLuongDetails.Where(x=>x.maSanPham==id).ToListAsync();

            if (soLuongDetails == null)
            {
                return NotFound();
            }

            return Ok(soLuongDetails);
        }

        // PUT: api/SoLuongDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoLuongDetails(int id, SoLuongDetails soLuongDetails)
        {
           

            var obj = _context.SoLuongDetails.FirstOrDefault(x => x._id == id);
            if(obj is not null)
            {
              
                try
                {
                    obj.Soluong = soLuongDetails.Soluong;
                    _context.SoLuongDetails.Update(obj);
                    await _context.SaveChangesAsync();
                    var product = await _context.SanPhams.FirstOrDefaultAsync(x => x.MaSanPham == soLuongDetails.maSanPham);
                    var productQty = await _context.SoLuongDetails.Where(x => x.maSanPham == product.MaSanPham).ToListAsync();
                    if (product != null)
                    {
                        product.SoLuongNhap = (int)productQty.Sum(x => x.Soluong);
                        product.SoLuongTon = product.SoLuongNhap;
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // POST: api/SoLuongDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SoLuongDetails>> PostSoLuongDetails(SoLuongDetails soLuongDetails,string maSP)
        {

            _context.SoLuongDetails.Add(soLuongDetails);
            try
            {
                
                var product = await _context.SanPhams.FirstOrDefaultAsync(x=>x.MaSanPham==maSP);
                if(product!=null)
                {
                    product.SoLuongNhap += (int)soLuongDetails.Soluong;
                    product.SoLuongTon = product.SoLuongNhap;
                    await _context.SaveChangesAsync();
                    return Ok(soLuongDetails);
                }
            }
            catch (DbUpdateException)
            {
                if (SoLuongDetailsExists(soLuongDetails.maMau))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return BadRequest();
        }

        // DELETE: api/SoLuongDetail/5
        [HttpDelete]
        public async Task<IActionResult> DeleteSoLuongDetails(int id)
        {
            try
            {
                var soLuongDetails = await _context.SoLuongDetails.FirstOrDefaultAsync(x =>x._id==id);
                if (soLuongDetails == null)
                {
                    return NotFound();
                }
                _context.SoLuongDetails.Remove(soLuongDetails);
                var product = await _context.SanPhams.FirstOrDefaultAsync(x => x.MaSanPham == soLuongDetails.maSanPham);
                if (product != null)
                {
                    product.SoLuongNhap -= (int)soLuongDetails.Soluong;
                    product.SoLuongTon = product.SoLuongNhap;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            

            return NoContent();
        }

        private bool SoLuongDetailsExists(string id)
        {
            return _context.SoLuongDetails.Any(e => e.maMau == id);
        }
    }
}
