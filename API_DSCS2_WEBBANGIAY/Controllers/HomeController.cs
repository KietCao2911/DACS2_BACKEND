using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Include(x => x.SoLuongDetails)
                .ThenInclude(x => x.IdMauSacNavigation).Include(x => x.SoLuongDetails)
                .ThenInclude(x => x.IdSizeNavigation)
                .Where(x=>x.SoLuongDetails.Any(x=>x.SoluongTon>0)).OrderBy(x => x.CreatedAt).ToListAsync();
           var select = products.Select(x => new
            {
                MaSanPham = x?.MaSanPham,
                TenSanPham = x?.TenSanPham,
                GiaBanDisplay = x?.VND(x.GiaBan),
                GiamGia = x?.GiamGia,
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
               Color = x?.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
               {
                   IdMaumau = x.First().IdMaMau,
                   HinhAnhInfo = x?.Select(x => new
                   {
                       uid = x.IdHinhAnh,
                       name = x?.IdHinhAnhNavigation?.FileName,
                       status = "done",
                       url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()

                   })
               }),
               ChiTietSoLuong = x?.SoLuongDetails.GroupBy(x => x.maMau).Select(x => new
               {
                   Idmau = x.First().maMau,
                   sizeDetails = x.Select(x => new
                   {
                       _id = x._id,
                       idSize = x._idSize,
                       sizeLabel = x.IdSizeNavigation.Size1,
                       soLuong = x.Soluong,
                   }),
               }),


           });; ;
            return Ok(select);
        }
        [HttpGet("ProductsHot")]
        public async Task<IActionResult> HotProducts()
        {
            var products = _context.ChiTietHoaDons
                .Include(x => x.MasanPhamNavigation)
                .ThenInclude(x=>x.ChiTietHinhAnhs)
                .ThenInclude(x => x.IdHinhAnhNavigation).Include(x=>x.MasanPhamNavigation)
                .ThenInclude(x=>x.SoLuongDetails).ThenInclude(x => x.IdMauSacNavigation)
                .Include(x => x.MasanPhamNavigation).ThenInclude(x => x.SoLuongDetails).ThenInclude(x => x.IdSizeNavigation).Where(x => x.MasanPhamNavigation.SoLuongDetails.Any(x => x.SoluongTon > 0))
                .OrderByDescending(x => x.Qty).ToList().GroupBy(x => x.MasanPham).Select(x => x.First()).Take(8).ToList();
            var select = products.Select(x => new
            {
                MaSanPham = x?.MasanPhamNavigation.MaSanPham,
                TenSanPham = x?.MasanPhamNavigation.TenSanPham,
                GiamGia = x?.MasanPhamNavigation.GiamGia,
                GiaBan = x?.MasanPhamNavigation.GiaBan,
                SoLuongNhap = x?.MasanPhamNavigation.SoLuongNhap,
                SoLuongTon = x?.MasanPhamNavigation.SoLuongTon,
                Slug = x?.MasanPhamNavigation.Slug,
                BoSuuTap = new { key = x?.MasanPhamNavigation.IdBstNavigation?.Id, value = x?.MasanPhamNavigation.IdBstNavigation?.TenBoSuuTap },
                Size = x.MasanPhamNavigation.SoLuongDetails?.Select(x => new
                {
                    label = x.IdSizeNavigation.Size1,
                    value = x._idSize,
                }),
                Color = x?.MasanPhamNavigation.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
                {
                    IdMaumau = x.First().IdMaMau,
                    HinhAnhInfo = x?.Select(x => new
                    {
                        uid = x.IdHinhAnh,
                        name = x?.IdHinhAnhNavigation?.FileName,
                        status = "done",
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()

                    })
                }),
                ChiTietSoLuong = x?.MasanPhamNavigation.SoLuongDetails.GroupBy(x => x.maMau).Select(x => new
                {
                    Idmau = x.First().maMau,
                    sizeDetails = x.Select(x => new
                    {
                        _id = x._id,
                        idSize = x._idSize,
                        sizeLabel = x.IdSizeNavigation.Size1,
                        soLuong = x.Soluong,
                    }),
                }),


            }); ; ;
            return Ok(products);
        }

    }
}
