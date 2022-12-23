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
                       Include(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation)
                       .Include(x => x.DanhMucDetails).Where(x => x.DanhMucDetails.Any(x => x.danhMucId == getID.Id));
                }
                else
                {
                    products = _context.SanPhams.
                      Include(x => x.IdBstNavigation).
                      Include(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation);
                  
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
                    TenSanPham = x?.TenSanPham,
                    GiaBan = x?.GiaBanLe,
                    GiamGia = x?.GiamGia,
                    Slug = x?.Slug,
                    BoSuuTap = x.IdBstNavigation,
                  
                    Color = x.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
                    {
                        IdMaumau = x.First().IdMaMau,
                        HinhAnhInfo = x.Select(x => new
                        {
                            uid = x.IdHinhAnh,
                            name = x.IdHinhAnhNavigation.FileName,
                            status = "done",
                            url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.IDSanPham + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()
                        })
                    }),
                   


                }); ; ;
                return Ok(new
                {
                    products = select,
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
            var sanPham = await _context.SanPhams.Include(x => x.IdBstNavigation).Include(x=>x.ChiTietHinhAnhs).ThenInclude(x=>x.IdHinhAnhNavigation)
              /*.ThenInclude(x=>x.IdSizeNavigation)*/.Include(x=>x.DanhMucDetails).
                ThenInclude(x=>x.IdDanhMucNavigation).FirstOrDefaultAsync(x=>x.Slug.Trim()==slug.Trim());

            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Id = sanPham?.Id,
                TenSanPham = sanPham?.TenSanPham,
                GiaBanDisplay = sanPham?.VND((decimal)sanPham?.GiaBanLe),
                GiaBan = sanPham?.GiaBanLe,
                GiamGia=sanPham?.GiamGia,
                Mota = sanPham.Mota,
                BoSuuTap = new
                {
                    key = sanPham?.IdBstNavigation?.Id,
                    value = sanPham?.IdBstNavigation?.TenBoSuuTap
                },
                HinhAnh = sanPham?.ChiTietHinhAnhs.Select(x => new
                {
                    key = x.IdHinhAnhNavigation?.Id,
                    value = x.IdHinhAnhNavigation?.FileName.Trim(),
                }),
              
                DanhMuc = sanPham.DanhMucDetails.Select(x => new
                {
                    IdDanhMuc = x.IdDanhMucNavigation.Id,
                    ParentId = x.IdDanhMucNavigation.ParentCategoryID,
                    TenDanhMuc = x.IdDanhMucNavigation.TenDanhMuc
                }),
                Color = sanPham.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
                {
                    IdMaumau = x.First().IdMaMau,
                    HinhAnhInfo = x.Select(x => new
                    {
                        uid = x.IdHinhAnh,
                        name=x.IdHinhAnhNavigation.FileName,
                        status="done",
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.IDSanPham+ "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()
                    })
                })

            }); ;
        }

        // PUT: api/SanPham/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, SanPham sanPham)
        {
            var sp = await _context.SanPhams.FirstOrDefaultAsync(x => x.Id == sanPham.Id);
            sanPham.Slug = CustomSlug.Slugify(sanPham.TenSanPham);
            if (id != sanPham.Id)
            {
                return BadRequest();
            }
            if (sanPham.GiamGia != 0)
            {
                if(sp.GiamGia>0)
                {
                    var giaGoc = (decimal)sp.GiaBanLe / ((decimal)(100 - sp.GiamGia)/100);
                    var giaGiam= giaGoc * ((decimal)(100 - sanPham.GiamGia) / 100);
                    sanPham.GiaBanLe = (decimal)giaGiam;
                }
                else
                {
                    var phantram = (decimal)(100 - sanPham.GiamGia) / 100 ;
                    var giaGiam = (decimal)(sanPham.GiaBanLe * phantram);
                    sanPham.GiaBanLe = giaGiam;
                }
            }
            else
            {
                if (sp.GiamGia > 0)
                {
                    var giaGoc = (decimal)sp.GiaBanLe / ((decimal)(100 - sp.GiamGia) / 100);
                    var giaGiam = giaGoc * ((decimal)(100 - sanPham.GiamGia) / 100);
                    sanPham.GiaBanLe = (decimal)giaGiam;
                }
                else
                {
                    var phantram = (decimal)(100 - sanPham.GiamGia) / 100;
                    var giaGiam = (decimal)(sanPham.GiaBanLe * phantram);
                    sanPham.GiaBanLe = giaGiam;
                }
            }
            _context.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new
            {
                success=true,
            });
        }

        // POST: api/SanPham
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanPham>> PostSanPham(PostSanPhamRequest body)
        {
            try
            {
                var slug = CustomSlug.Slugify(body.product.TenSanPham) + "_" + body.product.Id;
                body.product.Slug = slug;
                _context.SanPhams.Add(body.product);
                await _context.SaveChangesAsync();
                if(body.productVersions.Count>0)
                {
                    foreach(var version in body.productVersions)
                    {
                        var maSP = version.MaSanPham is null || version.MaSanPham.Length <= 0 ? "CTK" + version.Id : version.MaSanPham;
                        version.MaSanPham = maSP;
                        version.ParentID = body.product.MaSanPham;
                        _context.SanPhams.Add(version);
                    }
                await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    var maSP = body.maSP.Length>0||body.maSP is not null?body.maSP: "CTK" + body.product.Id;
        
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/SanPham/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
           
            if (sanPham == null)
            {
                return NotFound();
            }
            try {
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "wwwroot//res//SanPhamRes//Imgs//" + sanPham.Id);
                Directory.Delete(path, true);
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch(Exception err)
            {
                return BadRequest();
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
        public async Task<IActionResult>RemoveImg(string fileName, int _id,int maSP,string maMau)
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
                        var hinhAnh = await _context.ChiTietHinhAnhs.FirstOrDefaultAsync(x=>x.IDSanPham==maSP&&x.IdMaMau == maMau&&x.IdHinhAnh == _id);
                        if (hinhAnh != null)
                        {
                            _context.ChiTietHinhAnhs.Remove(hinhAnh);
                            await _context.SaveChangesAsync();
                            return Ok(new
                            {
                                success = true,
                                path = path
                            }); ;
                        }
                        return BadRequest();
                    }
                }
                catch(Exception err)
                {
                    return BadRequest();
                }
               
            }
            return BadRequest();
        }
        [HttpPost("Upload-Mutiple/{MaSP}/{MaMau}")]
        public async Task<IActionResult> UploadMutiple(IFormFile file, int IDSanPham,string MaMau)
        {
            if (file is null) return BadRequest();
  
                    var folder = "wwwroot//res//SanPhamRes//Imgs//" + IDSanPham + "//"+MaMau+"//";
                    var path = Path.Combine(
              Directory.GetCurrentDirectory(), folder,
              file.FileName);
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    else
            {
                return BadRequest("fileName existed");
            }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        try
                        {
                            await file.CopyToAsync(stream);
                            HinhAnh anh = new HinhAnh();
                            ChiTietHinhAnh hinhAnhSanPham = new ChiTietHinhAnh();
                            anh.FileName = file.FileName;
                            _context.HinhAnhs.Add(anh);
                            await _context.SaveChangesAsync();
                            hinhAnhSanPham.IdHinhAnh = anh.Id;
                            hinhAnhSanPham.IDSanPham = IDSanPham;
                            hinhAnhSanPham.IdMaMau = MaMau; 
                            _context.ChiTietHinhAnhs.Add(hinhAnhSanPham);
                            await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        uid = hinhAnhSanPham.IdHinhAnh,
                        name = file.FileName,
                        status = "done",
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + hinhAnhSanPham.IDSanPham + "\\" + hinhAnhSanPham.IdMaMau + "\\" + file.FileName.Trim()
                    });

                }
                        catch (Exception err)
                        {
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
