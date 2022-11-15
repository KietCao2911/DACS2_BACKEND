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
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSanPhams(string? sort, [FromQuery(Name = "size")] string size, [FromQuery(Name = "color")] string color, int pageSize, int? page, string id)
        {

            pageSize = pageSize == 0 ? 5 : pageSize;
            IQueryable<SanPham> products = Enumerable.Empty<SanPham>().AsQueryable();
            if(id != "undefined")
            {
                var getID = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Slug == id);
                products = _context.SanPhams.
                   Include(x => x.IdBstNavigation).
                   Include(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation).
                   Include(x => x.SoLuongDetails).ThenInclude(x => x.IdMauSacNavigation).
                   Include(x => x.SoLuongDetails).ThenInclude(x => x.IdSizeNavigation)
                   .Include(x => x.DanhMucDetails).Where(x => x.DanhMucDetails.Any(x => x.danhMucId == getID.Id)).Where(x => x.SoLuongTon >= 0);
            }
            else
            {
                products= _context.SanPhams.
                  Include(x => x.IdBstNavigation).
                  Include(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation).
                  Include(x => x.SoLuongDetails).ThenInclude(x => x.IdMauSacNavigation).
                  Include(x => x.SoLuongDetails).ThenInclude(x => x.IdSizeNavigation)
                  .Include(x => x.DanhMucDetails);
            }
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
            var result = await PaggingService<SanPham>.CreateAsync((IQueryable<SanPham>)products, page ?? 1, pageSize);
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
                Size = x.SoLuongDetails?.Select(x => new
                {
                    label = x.IdSizeNavigation.Size1,
                    value = x._idSize,
                }),
                Color = x.ChiTietHinhAnhs?.GroupBy(x => x.IdMaMau).Select(x => new
                {
                    IdMaumau = x.First().IdMaMau,
                    HinhAnhInfo = x.Select(x => new
                    {
                        uid = x.IdHinhAnh,
                        name = x.IdHinhAnhNavigation.FileName,
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


            }); ; ;
            return Ok(new
            {
                products = select,
                totalRow = products.Count(),
            });
        }

        // GET: api/SanPham/5
        [HttpGet]
        [Route("san-pham/{id}")]
        public async Task<ActionResult<SanPham>> GetSanPham(string id)
        {
            var sanPham = await _context.SanPhams.Include(x => x.IdBstNavigation).Include(x=>x.ChiTietHinhAnhs).ThenInclude(x=>x.IdHinhAnhNavigation).Include(x=>x.SoLuongDetails).ThenInclude(x=>x.IdMauSacNavigation).Include(x=>x.SoLuongDetails).ThenInclude(x=>x.IdSizeNavigation).Where(x => x.SoLuongTon >= 0).Include(x=>x.DanhMucDetails).ThenInclude(x=>x.IdDanhMucNavigation).FirstOrDefaultAsync(x=>x.MaSanPham==id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                MaSanPham = sanPham?.MaSanPham,
                TenSanPham = sanPham?.TenSanPham,
                GiaBanDisplay = sanPham?.VND((decimal)sanPham?.GiaBan),
                GiaBan = sanPham?.GiaBan,
                SoLuongNhap = sanPham?.SoLuongNhap,
                SoLuongTon = sanPham?.SoLuongTon,
                Img = sanPham?.Img,
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
            
                SoLuong = sanPham.SoLuongDetails.Select(x => new
                {
                    _id = x._id,
                    _idMau = x.maMau,
                    maSanPham=x.maSanPham,
                    sizeDetail = new
                    {
                        sizeLabel=x.IdSizeNavigation?.Size1,
                        sizeId = x.IdSizeNavigation?.Id
                    },
                    colorDetail = new
                    {
                        colorLabel = x.IdMauSacNavigation?.TenMau,
                        colorId = x.IdMauSacNavigation.MaMau,
                    },
                    Qty = x.Soluong
                }),
                ChiTietSoLuong = sanPham?.SoLuongDetails.GroupBy(x => x.maMau).Select(x => new
                {
                    Idmau= x.First().maMau,
                    sizeDetails = x.Select(x => new
                    {
                        _id = x._id,
                        idSize = x._idSize,
                        sizeLabel = x.IdSizeNavigation.Size1,
                        soLuong = x.Soluong,
                    }),
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
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + x.MaSanPham.Trim() + "\\" + x.IdMaMau.Trim() + "\\" + x.IdHinhAnhNavigation.FileName.Trim()
                    })
                })

            }); ;
        }

        // PUT: api/SanPham/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(string id, SanPham sanPham)
        {
            sanPham.Slug = CustomSlug.Slugify(sanPham.TenSanPham);
            if (id != sanPham.MaSanPham.Trim())
            {
                return BadRequest();
            }

            sanPham.SoLuongTon = sanPham.SoLuongNhap;
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
        public async Task<ActionResult<SanPham>> PostSanPham(SanPham sanPham)
        {
            if (sanPham != null)
            {
                sanPham.Slug = CustomSlug.Slugify(sanPham.TenSanPham);
                sanPham.SoLuongTon = sanPham.SoLuongNhap;
                _context.SanPhams.Add(sanPham);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException err)
                {
                    if (SanPhamExists(sanPham.MaSanPham))
                    {
                        return Conflict(new
                        {
                            title="Cập nhật thất bại",
                            message="Trùng mã sản phẩm, vui lòng kiểm tra lại"
                        });

                    }
                    else
                    {
                        return BadRequest(new
                        {
                            title="Có lỗi xảy ra",
                            message=err.Message,
                        });
                    }
                }
            }

            return Ok(new
            {
                success=true,
                MaSanPham = sanPham.MaSanPham,
                TenSanPham = sanPham.TenSanPham,
                GiaBanDisplay = sanPham.VND(sanPham.GiaBan),
                GiaBan = sanPham.GiaBan,
                SoLuongNhap = sanPham.SoLuongNhap,
                SoLuongTon = sanPham.SoLuongTon,
                Img = sanPham.Img,
                Mota = sanPham.Mota,
            }) ;
        }

        // DELETE: api/SanPham/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(string id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPhams.Remove(sanPham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanPhamExists(string id)
        {
            return _context.SanPhams.Any(e => e.MaSanPham == id);
        }
        [HttpPost("Upload-Single")]
        public async Task<IActionResult> UploadSingle(IFormFile file,string maSP)
        {
            var files = Request.Form.Files;
            if (file != null)
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//res//SanPhamRes//Thumb",
                file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(stream);
                        var product = _context.SanPhams.FirstOrDefault(x => x.MaSanPham == maSP);
                        if (product != null)
                        {
                            product.Img = file.FileName;
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
                        var hinhAnh = await _context.ChiTietHinhAnhs.FirstOrDefaultAsync(x=>x.MaSanPham==maSP&&x.IdMaMau == maMau&&x.IdHinhAnh == _id);
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
        public async Task<IActionResult> UploadMutiple(IFormFile file, string MaSP,string MaMau)
        {
            if (file is null) return BadRequest();
  
                    var folder = "wwwroot//res//SanPhamRes//Imgs//" + MaSP+"//"+MaMau+"//";
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
                            hinhAnhSanPham.MaSanPham = MaSP;
                            hinhAnhSanPham.IdMaMau = MaMau; 
                            _context.ChiTietHinhAnhs.Add(hinhAnhSanPham);
                            await _context.SaveChangesAsync();
                    return Ok(new
                    {
                        uid = hinhAnhSanPham.IdHinhAnh,
                        name = file.FileName,
                        status = "done",
                        url = "https:\\localhost:44328\\wwwroot\\res\\SanPhamRes\\Imgs\\" + hinhAnhSanPham.MaSanPham.Trim() + "\\" + hinhAnhSanPham.IdMaMau + "\\" + file.FileName.Trim()
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
