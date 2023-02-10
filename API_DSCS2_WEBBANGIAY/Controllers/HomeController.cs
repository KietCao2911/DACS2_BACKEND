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
        [HttpGet("ProductsLatesUpdate/{MaChiNhanh}")]
        public async Task<IActionResult> ProductsLatesUpdate(string MaChiNhanh)
        {
            var khohangs =  _context.KhoHangs.Include(x => x.SanPhamNavigation).Include(x => x.BranchNavigation);
            var temp = await _context.KhoHangs.Include(x => x.SanPhamNavigation).ThenInclude(x => x.IdBstNavigation)
                       .Include(x => x.SanPhamNavigation).ThenInclude(x => x.ChiTietHinhAnhs).
                       ThenInclude(x => x.IdHinhAnhNavigation)
                       .Include(x => x.SanPhamNavigation).ThenInclude(x => x.SanPhams).ThenInclude(x => x.ChiTietHinhAnhs)
                       .Include(x => x.SanPhamNavigation).ThenInclude(x => x.TypeNavigation)
                       .Include(x => x.SanPhamNavigation).ThenInclude(x => x.BrandNavigation)
                       .Include(x => x.SanPhamNavigation).ThenInclude(x => x.DanhMucDetails)
                .Include(x => x.BranchNavigation).Where(x => x.SanPhamNavigation.ParentID == null).Where(x=>x.BranchNavigation.MaChiNhanh.Trim()== MaChiNhanh).OrderBy(x => x.SanPhamNavigation.CreatedAt).ToListAsync();
            //var products = await _context.SanPhams
            //      .Include(x => x.IdBstNavigation).
            //           Include(x => x.ChiTietHinhAnhs).
            //           ThenInclude(x => x.IdHinhAnhNavigation)
            //           .Include(x => x.SanPhams).ThenInclude(x => x.ChiTietHinhAnhs)
            //           .Include(x => x.TypeNavigation)
            //           .Include(x => x.BrandNavigation)
            //           .Include(x => x.DanhMucDetails)
            //           .Include(x=>x.KhoHangs)
            //           .Where(x => x.ParentID == null)
            //   .OrderBy(x => x.CreatedAt).ToListAsync();
            var select = temp.Select(x => new
            {
                Id = x.SanPhamNavigation.Id,
                MaSanPham = x.MaSanPham.Trim(),
                TenSanPham = x.SanPhamNavigation.TenSanPham.Trim(),
                GiaBan = x.SanPhamNavigation.GiaBanLe,
                GiamGia = x.SanPhamNavigation.GiamGia,
                Slug = x.SanPhamNavigation.Slug,
                BoSuuTap = x.SanPhamNavigation.IdBstNavigation,
                HinhAnhs = x.SanPhamNavigation.ChiTietHinhAnhs.Select(x => new
                {
                    uid = x.IdHinhAnh,
                    name = x.IdHinhAnhNavigation.FileName,
                    status = "done",
                    url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim(),
                    IdMaMau = x.IdMaMau,
                }).GroupBy(x => x.IdMaMau),
                SoLuongTon = x.SoLuongTon,
                CoTheBan = x.SoLuongCoTheban,
                LoaiHang = x.SanPhamNavigation.TypeNavigation,
                NhanHieu = x.SanPhamNavigation.BrandNavigation,
                MauSac = x.SanPhamNavigation.MauSacNavigation,
                KichThuoc = x.SanPhamNavigation.SizeNavigation,
                IDType = x.SanPhamNavigation.IDType,
                IDBrand = x.SanPhamNavigation.IDBrand,
                SanPhams = x.SanPhamNavigation.SanPhams,
                KhoHangs= x.SanPhamNavigation.KhoHangs,
            }); ; ;
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
               .ThenInclude(x => x.IdHinhAnhNavigation).Include(x => x.MasanPhamNavigation).Where(x => x.MasanPhamNavigation.ParentID == null)
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
