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
    public class PhieuNhapsController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public PhieuNhapsController(ShoesEcommereContext context)
        {
            _context = context;
        }

        // GET: api/PhieuNhaps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhieuNhap>>> GetPhieuNhaps()
        {
            return await _context.PhieuNhaps.ToListAsync();
        }

        // GET: api/PhieuNhaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhieuNhap>> GetPhieuNhap(string id)
        {
            var phieuNhap = await _context.PhieuNhaps.FindAsync(id);

            if (phieuNhap == null)
            {
                return NotFound();
            }

            return phieuNhap;
        }

        // PUT: api/PhieuNhaps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhieuNhap(string id, PhieuNhap phieuNhap)
        {
            if (id != phieuNhap.maPhieuNhap.Trim())
            {
                return BadRequest();
            }

            _context.Entry(phieuNhap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(phieuNhap);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhieuNhapExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PhieuNhaps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhieuNhap>> PostPhieuNhap(PhieuNhap phieuNhap)
        {
            _context.PhieuNhaps.Add(phieuNhap);
            try
            {
                await _context.SaveChangesAsync();
                phieuNhap.maPhieuNhap = "PN" + phieuNhap.ID;
                _context.PhieuNhaps.Update(phieuNhap);
                await _context.SaveChangesAsync();
                return Ok(phieuNhap);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }

        }

        // DELETE: api/PhieuNhaps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhieuNhap(string id)
        {
            var phieuNhap = await _context.PhieuNhaps.FindAsync(id);
            if (phieuNhap == null)
            {
                return NotFound();
            }

            _context.PhieuNhaps.Remove(phieuNhap);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhieuNhapExists(string id)
        {
            return _context.PhieuNhaps.Any(e => e.maPhieuNhap == id);
        }
    }
}
