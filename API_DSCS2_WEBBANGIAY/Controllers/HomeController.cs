using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public HomeController(ShoesEcommereContext context)
        {
            _context = context;
        }
        [HttpGet("ProductsLatesUpdate")]
        public async Task<IActionResult> ProductsLatesUpdate()
        {
            var products = await _context.SanPhams
                .Include(x => x.IdBstNavigation)
                .Include(x=>x.ChiTietHinhAnhs)
                .ThenInclude(x=>x.IdHinhAnhNavigation)
               .OrderBy(x => x.CreatedAt).ToListAsync();
           var select = products.Select(x => new
            {
                IdSanPham = x?.Id,
                TenSanPham = x?.TenSanPham,
                GiamGia = x?.GiamGia,
                GiaBan = x?.GiaBanLe,
                Slug = x?.Slug,
                BoSuuTap = new { key = x?.IdBstNavigation?.Id, value = x?.IdBstNavigation?.TenBoSuuTap },
               
               Color = x?.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
               {
                   IdMaumau = x.First().IdMaMau,
                   HinhAnhInfo = x?.Select(x => new
                   {
                       uid = x.IdHinhAnh,
                       name = x?.IdHinhAnhNavigation?.FileName,
                       status = "done",
                       url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.IDSanPham + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()

                   })
               }),
              


           });; ;
            return Ok(select);
        }
        [HttpGet("ProductsHot")]
        public async Task<IActionResult> HotProducts()
        {
           try
            {
                var products = _context.ChiTietHoaDons
               .Include(x => x.MasanPhamNavigation)
               .ThenInclude(x => x.ChiTietHinhAnhs)
               .ThenInclude(x => x.IdHinhAnhNavigation).Include(x => x.MasanPhamNavigation)
               .OrderByDescending(x => x.Qty).ToList().GroupBy(x => x.IDSanPham).Select(x => x.First()).Take(8).ToList();
                var select = products.Select(x => new
                {
                    IdSanPham = x?.MasanPhamNavigation.Id,
                    TenSanPham = x?.MasanPhamNavigation.TenSanPham,
                    GiamGia = x?.MasanPhamNavigation.GiamGia,
                    GiaBan = x?.MasanPhamNavigation.GiaBanLe,
                    Slug = x?.MasanPhamNavigation.Slug,
                    BoSuuTap = new { key = x?.MasanPhamNavigation.IdBstNavigation?.Id, value = x?.MasanPhamNavigation.IdBstNavigation?.TenBoSuuTap },
              
                    Color = x?.MasanPhamNavigation.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
                    {
                        IdMaumau = x.First().IdMaMau,
                        HinhAnhInfo = x?.Select(x => new
                        {
                            uid = x.IdHinhAnh,
                            name = x?.IdHinhAnhNavigation?.FileName,
                            status = "done",
                            url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.IDSanPham + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()

                        })
                    }),
                 

                }); ; ;
                return Ok(select);
            }catch(Exception err)
            {
                return BadRequest(err);
            }
        }

    }
}
