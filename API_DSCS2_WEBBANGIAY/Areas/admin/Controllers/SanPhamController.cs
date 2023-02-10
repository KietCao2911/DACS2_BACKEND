using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_DSCS2_WEBBANGIAY.Models;
using API_DSCS2_WEBBANGIAY.Utils;
using System.IO;
using API_DSCS2_WEBBANGIAY.BodyRequest.SanPhamController;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public SanPhamController(ShoesEcommereContext context)
        {
            _context = context;
        }

        // GET: api/SanPham
        [HttpGet]
        [Route("/api/san-pham/{id}")]
        public async Task<ActionResult> GetSanPhams(string? sort, [FromQuery(Name = "size")] string size, [FromQuery(Name = "color")] string color, int pageSize, int? page, string id, [FromQuery(Name = "sale")] bool sale, [FromQuery(Name = "s")]  string s)
        {

            try
            {
                pageSize = pageSize == 0 ? 5 : pageSize;
                IQueryable<SanPham> products = Enumerable.Empty<SanPham>().AsQueryable();
                if (id != "undefined")
                {
                    var getID = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Slug == id);
                    products = _context.SanPhams.
                       Include(x => x.IdBstNavigation).
                       Include(x => x.ChiTietHinhAnhs).
                       ThenInclude(x => x.IdHinhAnhNavigation)
                       .Include(x=>x.SanPhams).ThenInclude(x=>x.ChiTietHinhAnhs)
                       .Include(x=>x.TypeNavigation)
                       .Include(x=>x.BrandNavigation)
                       .Include(x=>x.VatNavigation)
                       .Include(x => x.DanhMucDetails)
                       .Where(x => x.DanhMucDetails.Any(x => x.danhMucId == getID.Id)).Include(x => x.SizeNavigation).Where(x=>x.ParentID==null);
                }
                else
                {
                    products = _context.SanPhams.
                      Include(x => x.IdBstNavigation)
                      .Include(x=>x.MauSacNavigation)
                      .Include(x=>x.SizeNavigation)
                      .Include(x => x.SanPhams).ThenInclude(x => x.ChiTietHinhAnhs)
                      .Include(x => x.VatNavigation)
                      .Include(x => x.TypeNavigation)
                      .Include(x => x.BrandNavigation).
                      Include(x => x.ChiTietHinhAnhs)
                      .ThenInclude(x => x.IdHinhAnhNavigation).Where(x => x.ParentID == null);
                  
                }
                if (s is not null && s.Length > 0)
                {
                    products = products.Where(x => x.TenSanPham.Trim().ToLower().Contains(s.Trim().ToLower()));
                }
              
                if (sale == true)
                {
                    products = products.Where(x => x.GiamGia > 0);
                }
                switch (sort)
                {
                    case "price-hight-to-low":
                        products = products.OrderByDescending(s => s.GiaBanLe);
                        break;
                    case "date-oldest":
                        products = products.OrderBy(s => s.CreatedAt);
                        break;
                    case "date-newest":
                        products = products.OrderByDescending(s => s.CreatedAt);
                        break;
                    default:
                        products = products.OrderBy(s => s.GiaBanLe);
                        break;
                }
                var result = await PaggingService<SanPham>.CreateAsync((IQueryable<SanPham>)products, page ?? 1, pageSize);
                var select = result.Select(x => new
                {
                    Id = x?.Id,
                    MaSanPham=x.MaSanPham.Trim(),
                    TenSanPham = x?.TenSanPham.Trim(),
                    GiaBan = x?.GiaBanLe,
                    GiamGia = x?.GiamGia,
                    Slug = x?.Slug,
                    BoSuuTap = x.IdBstNavigation,
                    IDVAT = x.IDVat,
                    VAT = x?.VatNavigation,
                    HinhAnhs = x?.ChiTietHinhAnhs.Select(x => new
                    {
                        uid = x.IdHinhAnh,
                        name = x.IdHinhAnhNavigation.FileName,
                        status = "done",
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim(),
                        IdMaMau = x.IdMaMau,
                    }).GroupBy(x => x.IdMaMau),
                    SoLuongTon = x.SoLuongTon,
                    CoTheBan = x.SoLuongCoTheban,
                    LoaiHang = x?.TypeNavigation,
                    NhanHieu = x?.BrandNavigation,
                    MauSac = x?.MauSacNavigation,
                    KichThuoc = x?.SizeNavigation,
                    IDType = x?.IDType,
                    IDBrand = x?.IDBrand,
                    SanPhams= x.SanPhams,
                    
                }); ; ;
                return Ok(new
                {
                    products=select,
                    totalRow = products.Count(),
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/SanPham/5
        [HttpGet]
        [Route("san-pham/{slug}")]
        public async Task<ActionResult<SanPham>> GetSanPham(string slug)
        {
            var sanPham = await _context.SanPhams.

                      Include(x => x.IdBstNavigation).Include(x => x.SanPhams).
                      ThenInclude(x=>x.SizeNavigation)
                      .Include(x => x.SanPhams)
                      .ThenInclude(x => x.MauSacNavigation)
                       .Include(x => x.SanPhams)
                       .ThenInclude(x=>x.KhoHangs)
                       .ThenInclude(x=>x.LichSuNhapXuatHangNavigation)
                       .Include(x => x.VatNavigation)
                      .Include(x => x.TypeNavigation)
                      .Include(x => x.BrandNavigation).
                       Include(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation).FirstOrDefaultAsync(x=>x.Slug.Trim()==slug.Trim()&&x.ParentID==null);
            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Id = sanPham?.Id,
                MaSanPham = sanPham.MaSanPham.Trim(),
                TenSanPham = sanPham?.TenSanPham.Trim(),
                GiaBan = sanPham?.GiaBanLe,
                GiamGia = sanPham?.GiamGia,
                Slug = sanPham?.Slug,
                BoSuuTap = sanPham.IdBstNavigation,
                SoLuongTon = sanPham.SoLuongTon,
                CoTheBan = sanPham.SoLuongCoTheban,
                LoaiHang = sanPham?.TypeNavigation,
                NhanHieu = sanPham?.BrandNavigation,
                VAT = sanPham?.VatNavigation,
                IDType = sanPham?.IDType,
                IDBrand= sanPham?.IDBrand,
                IDVAT = sanPham?.IDVat,
                HinhAnhs = sanPham?.ChiTietHinhAnhs.Select(x => new
                {
                    uid = x.IdHinhAnh,
                    name=x.IdHinhAnhNavigation.FileName,
                    status="done",
                    url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim(),
                    IdMaMau = x.IdMaMau,
                }).GroupBy(x => x.IdMaMau)
                ,SanPhams = sanPham.SanPhams,
            });; ;
        }

        // PUT: api/SanPham/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSanPham( SanPham sanPham)
        {

            try
            {
                foreach(var product in sanPham.SanPhams)
                {
                    _context.Entry(product).State = EntityState.Modified;
                }
                _context.Entry(sanPham).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        // POST: api/SanPham
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanPham>> PostSanPham(SanPham body)
        {
            var trans = _context.Database.BeginTransaction();
            ChiNhanh_SanPham khohang = new ChiNhanh_SanPham();
            try
            {
                
                if(body.MaSanPham==null || body.MaSanPham.Length==0)
                {
                    var ID = new GenKey();
                    _context.Keys.Add(ID);
                    await _context.SaveChangesAsync();
                    body.MaSanPham = "CTK0"+ ID.ID.ToString();
                    body.Slug = CustomSlug.Slugify(body.TenSanPham) + "_" + body.MaSanPham;
                    khohang.MaSanPham = body.MaSanPham;
                    khohang.MaChiNhanh = "CN01";
                    var history = new LichSuNhapXuatHang();
                    history.Name = "Khởi tạo version";
                    _context.LichSuNhapHangs.Add(history);
                    await _context.SaveChangesAsync();
                    khohang.IDLichSu = history.ID;
                }
                var products = body.SanPhams.ToList();
               if(products.Count>0)
                {
                    for (int i = 0; i < body.SanPhams.Count; i++)
                    {
                        if (products[i].MaSanPham == null || products[i].MaSanPham.Length == 0)
                        {
                            var ID = new GenKey();
                            var history = new LichSuNhapXuatHang();
                            history.Name = "Khởi tạo version";
                            _context.Keys.Add(ID);
                            _context.LichSuNhapHangs.Add(history);
                            await _context.SaveChangesAsync();
                            products[i].IDVat = body.IDVat;
                            products[i].IDBrand = body.IDBrand;
                            products[i].IDType = body.IDType;
                            products[i].MaSanPham = "CTK0" + ID.ID.ToString();
                            products[i].ParentID = body.MaSanPham;
                            products[i].Slug = CustomSlug.Slugify(products[i].TenSanPham) + "_" + products[i].MaSanPham;
                            products[i].KhoHangs.Add(new ChiNhanh_SanPham()
                            {
                                SoLuongTon =  products[i].SoLuongTon,
                                SoLuongCoTheban = products[i].SoLuongCoTheban,
                                SoLuongHangDangGiao = 0,
                                SoLuongHangDangVe=0,
                                GiaVon = products[i]?.GiaVon,    
                                IDLichSu = history.ID,
                                MaSanPham = products[i].MaSanPham,
                                MaChiNhanh ="CN01" /*body.KhoHangs.ToArray()[0].MaChiNhanh*/,
                            });
                        }
                    }
                }
                body.KhoHangs =new List<ChiNhanh_SanPham>();
                _context.SanPhams.Add(body);
                _context.KhoHangs.Add(khohang);
                await _context.SaveChangesAsync();
                await trans.CommitAsync();
                return Ok(body);
            }
            catch (Exception err)
            {
                await  trans.RollbackAsync();
                return BadRequest(err);
            }

        }

        // DELETE: api/SanPham/5
        [HttpDelete("{maSanPham}")]
        public async Task<IActionResult> DeleteSanPham(string maSanPham)
        {
            var sanPham = await _context.SanPhams.Include(x=>x.SanPhams).FirstOrDefaultAsync(x=>x.MaSanPham== maSanPham);
           
            if (sanPham == null)
            {
                return NotFound();
            }
            try {
                var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot//res//SanPhamRes//Imgs//" + sanPham.MaSanPham.Trim());
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                _context.SanPhams.RemoveRange(sanPham.SanPhams);
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPhams.Any(e => e.Id == id);
        }
        [HttpPost("Upload-Single")]
        public async Task<IActionResult> UploadSingle(IFormFile file,int id)
        {
            var files = Request.Form.Files;
            if (file != null)
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//res//SanPhamRes//Thumb",
                file.FileName);
                if(Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(stream);
                        var product = _context.SanPhams.FirstOrDefault(x => x.Id == id);
                        if (product != null)
                        {
                            var res = await _context.SaveChangesAsync();
                            if (res > 0)
                            {
                                return Ok(new
                                {
                                    success = true,
                                    img = file.FileName,

                                });
                            }
                            else
                            {
                                return Ok(new
                                {
                                    success = false,


                                });
                            }
                        }

                    }
                    catch (Exception err)
                    {
                        return BadRequest(new
                        {
                            success = false,
                           
                        });
                    }

                }
            }
           
            return BadRequest();
        }
        [HttpDelete("RemoveImg")]
        public async Task<IActionResult>RemoveImg(string fileName, int _id,string maSP,string maMau)
        {
            if (fileName != null)
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//res//SanPhamRes//Imgs//" + maSP + "//" + maMau + "//",
                fileName);
                
                FileInfo file = new FileInfo(path);    
                try
                {
                    if (file.Exists)
                    {
                        file.Delete();
                        var hinhAnh = await _context.HinhAnhs.FindAsync(_id);
                        if (hinhAnh != null)
                        {
                            _context.HinhAnhs.Remove(hinhAnh);
                            await _context.SaveChangesAsync();
                            return Ok(new
                            {
                                success = true,
                                path = path
                            }); ;
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();

                    }
                }
                catch(Exception err)
                {
                    return BadRequest();
                }

            }
            else
            {

            return BadRequest();
            }
        }
        [HttpPost("Upload/{MaSP}/{MaMau}")]
        public async Task<IActionResult> Upload(IFormFile file, string maSP,string MaMau)
        {
            if (file is null) return BadRequest();

            var folder = "wwwroot/res/SanPhamRes/Imgs/" + maSP + "/" + MaMau + "/";
            var path = Path.Combine(
      Directory.GetCurrentDirectory(), folder,
      file.FileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(folder);
            }
            if (System.IO.File.Exists(path))
            {
                return BadRequest("Đã tồn tại hình ảnh này!");
            }
            using (var stream = new FileStream(path, FileMode.Create))
            {
                var trans = _context.Database.BeginTransaction();
                try
                {
                    await file.CopyToAsync(stream);
                    HinhAnh anh = new HinhAnh();
                    anh.FileName = file.FileName;
                    _context.HinhAnhs.Add(anh);
                    await _context.SaveChangesAsync();
                    ChiTietHinhAnh hinhAnhSanPham = new ChiTietHinhAnh();
                    hinhAnhSanPham.IdHinhAnh = anh.Id;
                    hinhAnhSanPham.MaSanPham = maSP;
                    hinhAnhSanPham.IdMaMau = MaMau;
                    _context.ChiTietHinhAnhs.Add(hinhAnhSanPham);
                    await _context.SaveChangesAsync();
                    await trans.CommitAsync();
                    return Ok(new
                    {
                        idMaMau = hinhAnhSanPham.IdMaMau.Trim(),
                        uid = hinhAnhSanPham.IdHinhAnh,
                        name = file.FileName,
                        status = "done",
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + hinhAnhSanPham.MaSanPham + "\\" + hinhAnhSanPham.IdMaMau + "\\" + file.FileName.Trim()
                    });

                }
                catch (Exception err)
                {
                   await  trans.RollbackAsync();
                    return BadRequest(new
                    {
                        success = false
                    });
                }

            }



        }
        [HttpDelete("Remove-Mutiple")]
        public async Task<IActionResult> RemoveMutiple(string fileName, string _id)
        {
            if (fileName != null)
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//res//SanPhamRes//Imgs//"+_id+"//",
                fileName);

                FileInfo file = new FileInfo(path);
                    if (file.Exists)
                {
                   
                    var img = await _context.HinhAnhs.FirstOrDefaultAsync(x => x.FileName == fileName);
                    if (img != null)
                    {
                        _context.HinhAnhs.Remove(img);
                        try
                        {
                            await _context.SaveChangesAsync();
                            file.Delete();
                            return Ok(new
                            {
                                success = true,
                                path = path
                            }); ;
                        }
                        catch(Exception err)
                        {
                            return BadRequest();
                        }
                      
                    }

                }
            }
            return BadRequest();
        }
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(List<DanhMucDetails> dm)
        {
            try
            {
               foreach(var item in dm)
                {
                    _context.DanhMucDetails.Add(item);
                    await _context.SaveChangesAsync();
                }
                return Ok();
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
            
        }



    }
}
