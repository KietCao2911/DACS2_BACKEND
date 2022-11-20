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
        public async Task<ActionResult<SoLuongDetails>> PostSoLuongDetails(SoLuongDetails soLuongDetails)
        {

            try
            {
                var soLuongOld = await _context.SoLuongDetails.FirstOrDefaultAsync(x=>x.maSanPham==soLuongDetails.maSanPham&&x._idSize==soLuongDetails._idSize&&x.maMau==soLuongDetails.maMau);
                if(soLuongOld is not null)
                {
                        int qtyUpdate = 0;
                        if(soLuongOld.Soluong<soLuongDetails.Soluong)
                        {
                            // số lượng tăng
                            qtyUpdate = (int)(soLuongDetails.Soluong - soLuongOld.Soluong);
                            soLuongDetails.Soluong += qtyUpdate;
                            soLuongDetails.SoluongTon = soLuongDetails.Soluong;
                        }
                        else
                        {
                            qtyUpdate = (int)(soLuongDetails.Soluong - soLuongOld.Soluong);
                            soLuongDetails.Soluong += qtyUpdate;
                            soLuongDetails.SoluongTon = soLuongDetails.Soluong;
                        }
                    _context.SoLuongDetails.Update(soLuongDetails);
                    var item = await _context.SoLuongDetails.Include(x => x.IdSizeNavigation).Include(x => x.IdMauSacNavigation).FirstOrDefaultAsync(x => x.maSanPham == soLuongDetails.maSanPham && x._idSize == soLuongDetails._idSize && x.maMau == soLuongDetails.maMau);

                    await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        _id = item._id,
                        colorLabel=item.IdMauSacNavigation.TenMau,
                        maMau = item.maMau,
                        idSize = item._idSize,
                        sizeLabel = item.IdSizeNavigation.Size1,
                        soLuong = item.Soluong,
                        action="Update",
                    });
                }
                else
                {
                    soLuongDetails.SoluongTon = (int)soLuongDetails.Soluong;
                    var item = await _context.SoLuongDetails.Include(x => x.IdSizeNavigation).Include(x => x.IdMauSacNavigation).FirstOrDefaultAsync(x => x.maSanPham == soLuongDetails.maSanPham && x._idSize == soLuongDetails._idSize && x.maMau == soLuongDetails.maMau );
                    _context.SoLuongDetails.Add(soLuongDetails);
                    await _context.SaveChangesAsync();
                    return Ok();
                   
                }
                
            }
            catch (Exception err)
            {
                return BadRequest();
            }

           
        }

        // DELETE: api/SoLuongDetail/5
        [HttpDelete("{id}")]
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
                await _context.SaveChangesAsync();
                return Ok(new {_id=id,maMau = soLuongDetails.maMau});
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
