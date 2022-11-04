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
    public class DanhMucDetailsController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public DanhMucDetailsController(ShoesEcommereContext context)
        {
            _context = context;
        }

        // GET: api/DanhMucDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DanhMucDetails>>> GetDanhMucDetails()
        {
            return await _context.DanhMucDetails.ToListAsync();
        }

        // GET: api/DanhMucDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucDetails>> GetDanhMucDetails(int id)
        {
            var danhMucDetails = await _context.DanhMucDetails.FindAsync(id);

            if (danhMucDetails == null)
            {
                return NotFound();
            }

            return danhMucDetails;
        }

        // PUT: api/DanhMucDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhMucDetails(int id, DanhMucDetails danhMucDetails)
        {
            if (id != danhMucDetails.danhMucId)
            {
                return BadRequest();
            }

            _context.Entry(danhMucDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucDetailsExists(id))
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

        // POST: api/DanhMucDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhMucDetails>> PostDanhMucDetails(DanhMucDetails danhMucDetails)
        {
            _context.DanhMucDetails.Add(danhMucDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DanhMucDetailsExists(danhMucDetails.danhMucId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDanhMucDetails", new { id = danhMucDetails.danhMucId }, danhMucDetails);
        }

        // DELETE: api/DanhMucDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhMucDetails(int id)
        {
            var danhMucDetails = await _context.DanhMucDetails.FindAsync(id);
            if (danhMucDetails == null)
            {
                return NotFound();
            }

            _context.DanhMucDetails.Remove(danhMucDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanhMucDetailsExists(int id)
        {
            return _context.DanhMucDetails.Any(e => e.danhMucId == id);
        }
    }
}
