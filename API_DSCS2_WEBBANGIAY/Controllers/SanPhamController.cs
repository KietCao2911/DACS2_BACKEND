using API_DSCS2_WEBBANGIAY.Models;
using API_DSCS2_WEBBANGIAY.Utils;
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
    public class SanPhamController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public SanPhamController(ShoesEcommereContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSanPhams(string? sort, [FromQuery(Name = "size")] string size, [FromQuery(Name = "color")] string color, int pageSize, int? page, string id)
        {

            pageSize = pageSize == 0 ? 5 : pageSize;
            var products = _context.SanPhams.Include(x => x.IdBstNavigation).Include(x => x.SoLuongDetails).ThenInclude(x => x.IdMauSacNavigation).Include(x => x.SoLuongDetails).ThenInclude(x => x.IdSizeNavigation).Where(x => (x.DanhMucDetails.FirstOrDefault(x => x.IdDanhMucNavigation.Slug == id).IdDanhMucNavigation.Slug == id)).Where(x => x.SoLuongNhap >= 0);

            if (color is not null)
            {
                products = products.Where(x => x.SoLuongDetails.FirstOrDefault(x => x.maMau == color).maMau == color);
            }
            if (size is not null)
            {
                int sizeInt = Int32.Parse(size);
                products = products.Where(x => x.SoLuongDetails.FirstOrDefault(x => x._idSize == sizeInt)._idSize == sizeInt);
            }
            switch (sort)
            {
                case "price-hight-to-low":
                    products = products.OrderByDescending(s => s.GiaBan);
                    break;
                case "date-oldest":
                    products = products.OrderBy(s => s.CreatedAt);
                    break;
                case "date-newest":
                    products = products.OrderByDescending(s => s.CreatedAt);
                    break;
                default:
                    products = products.OrderBy(s => s.GiaBan);
                    break;
            }
            var result = await PaggingService<SanPham>.CreateAsync(products, page ?? 1, pageSize);
            var select = result.Select(x => new
            {
                MaSanPham = x?.MaSanPham,
                TenSanPham = x?.TenSanPham,
                GiaBanDisplay = x?.VND(x.GiaBan),
                GiaBan = x?.GiaBan,
                SoLuongNhap = x?.SoLuongNhap,
                SoLuongTon = x?.SoLuongTon,
                Slug = x?.Slug,
                BoSuuTap = new { key = x?.IdBstNavigation?.Id, value = x?.IdBstNavigation?.TenBoSuuTap },
                Img = x?.Img,
                Size =  x.SoLuongDetails?.Select(x => new
                {
                    label = x.IdSizeNavigation.Size1,
                    value = x._idSize,
                }),
                Color = x.SoLuongDetails?.Select(x => new
                {
                    label = x.IdMauSacNavigation.TenMau,
                    value = x.maMau,
                })


            });; ;
            return Ok(new
            {
                products = select,
                totalRow = products.Count(),
            });
        }

    }
}

