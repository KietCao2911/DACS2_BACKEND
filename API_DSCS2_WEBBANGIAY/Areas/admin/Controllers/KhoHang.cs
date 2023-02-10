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
    public class KhoHang : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public KhoHang(ShoesEcommereContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var khohangs = _context.KhoHangs.Include(x=>x.LichSuNhapXuatHangNavigation).Include(x=>x.BranchNavigation).FirstOrDefault(x => x.MaSanPham == id);
                return Ok(khohangs);
            }
            catch(Exception err)
            {
                return BadRequest(err);
            }
        }
    }
}
