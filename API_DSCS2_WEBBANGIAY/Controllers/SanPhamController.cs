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
        //[HttpGet("{id}")]
        //public async Task<ActionResult> GetSanPhams(string? sort, [FromQuery(Name = "size")] string size, [FromQuery(Name = "color")] string color, int pageSize, int? page, string id)
        //{

        //    pageSize = pageSize == 0 ? 5 : pageSize;
        //    var getID =await _context.DanhMucs.FirstOrDefaultAsync(x => x.Slug == id);
        //        var test = _context.SanPhams.Include(x => x.DanhMucDetails).Where(x=>x.DanhMucDetails.First().danhMucId == getID.Id).ToList();
        //    var products = _context.SanPhams.
        //        Include(x => x.IdBstNavigation).
        //        Include(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation).
        //        Include(x => x.SoLuongDetails).ThenInclude(x => x.IdMauSacNavigation).
        //        Include(x => x.SoLuongDetails).ThenInclude(x => x.IdSizeNavigation)
        //        .Include(x => x.DanhMucDetails).Where(x => x.DanhMucDetails.Any(x=>x.danhMucId==getID.Id));
        //    if (color is not null)
        //    {
        //        products = products.Where(x => x.SoLuongDetails.FirstOrDefault(x => x.maMau == color).maMau == color);
        //    }
        //    if (size is not null)
        //    {
        //        int sizeInt = Int32.Parse(size);
        //        products = products.Where(x => x.SoLuongDetails.FirstOrDefault(x => x._idSize == sizeInt)._idSize == sizeInt);
        //    }
        //    switch (sort)
        //    {
        //        case "price-hight-to-low":
        //            products = products.OrderByDescending(s => s.GiaBan);
        //            break;
        //        case "date-oldest":
        //            products = products.OrderBy(s => s.CreatedAt);
        //            break;
        //        case "date-newest":
        //            products = products.OrderByDescending(s => s.CreatedAt);
        //            break;
        //        default:
        //            products = products.OrderBy(s => s.GiaBan);
        //            break;
        //    }
        //    var result = await PaggingService<SanPham>.CreateAsync(products, page ?? 1, pageSize);
        //    var select = result.Select(x => new
        //    {
        //        MaSanPham = x?.MaSanPham,
        //        TenSanPham = x?.TenSanPham,
        //        GiaBanDisplay = x?.VND(x.GiaBan),
        //        GiaBan = x?.GiaBan,
        //        Slug = x?.Slug,
        //        BoSuuTap = new { key = x?.IdBstNavigation?.Id, value = x?.IdBstNavigation?.TenBoSuuTap },
        //        Img = x?.Img,
        //        Size =  x.SoLuongDetails?.Select(x => new
        //        {
        //            label = x.IdSizeNavigation.Size1,
        //            value = x._idSize,
        //        }),
        //        Color = x.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
        //        {
        //            IdMaumau = x.First().IdMaMau,
        //            HinhAnhInfo = x.Select(x => new
        //            {
        //                uid = x.IdHinhAnh,
        //                name = x.IdHinhAnhNavigation.FileName,
        //                status = "done",
        //                url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()
        //            })
        //        })


        //    });; ;
        //    return Ok(new
        //    {
        //        products = select,
        //        totalRow = products.Count(),
        //    });
        //}

    }
}

