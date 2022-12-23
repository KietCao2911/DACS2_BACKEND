﻿using System;
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
    public class BoSuuTapController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public BoSuuTapController(ShoesEcommereContext context)
        {
            _context = context;
        }

        // GET: api/BoSuuTap
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoSuuTap>>> GetBoSuuTaps()
        {
            var bsts = await _context.BoSuuTaps.ToListAsync();
            return Ok(bsts.Select(x => new
            {
                Key = x.Id,
                Id = x.Id,
                Img = x.Img,
                TenBoSuuTap = x.TenBoSuuTap
            }));;
        }
        [HttpGet("GetProductByBST/{id}")]
        public async Task<ActionResult<IEnumerable<BoSuuTap>>> GetProductByBST(int id)
        {
            var products = await _context.SanPhams.Where(x=>x.IdBst==id).ToListAsync();
            var select = products.Select(x => new
            {
                ID = x?.Id,
                TenSanPham = x?.TenSanPham,
                //DanhMuc = new { key = x?.IdDmNavigation?.Id, value = x?.IdDmNavigation?.TenDanhMuc },
                //BoSuuTap = new { key = x?.IdBstNavigation?.Id, value = x?.IdBstNavigation?.TenBoSuuTap },
                //Img = x?.Img,
                //Sex = new
                //{
                //    Value = x?.IdDmNavigation.GioiTinhCodeNavigation?.GioitinhText,
                //    Key = x?.IdDmNavigation?.GioiTinhCodeNavigation?.GioitinhCode
                //},

            }); ;
            return Ok(select); 

        }
        // GET: api/BoSuuTap/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBoSuuTap(int id)
        {
            var boSuuTap = await _context.BoSuuTaps.FindAsync(id);

            if (boSuuTap == null)
            {
                return NotFound();
            }

            return Ok( new
            {
                Key = boSuuTap.Id,
                Id = boSuuTap.Id,
                Img = boSuuTap.Img,
                TenBoSuuTap = boSuuTap.TenBoSuuTap,
            }); ;
        }

        // PUT: api/BoSuuTap/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoSuuTap(int id, BoSuuTap boSuuTap)
        {
            if (id != boSuuTap.Id)
            {
                return BadRequest();
            }

            _context.Entry(boSuuTap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoSuuTapExists(id))
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

        // POST: api/BoSuuTap
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoSuuTap>> PostBoSuuTap(BoSuuTap boSuuTap)
        {
            boSuuTap.Slug = CustomSlug.Slugify(boSuuTap.TenBoSuuTap);
            _context.BoSuuTaps.Add(boSuuTap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoSuuTap", new { id = boSuuTap.Id }, new
            {
                Key= boSuuTap.Id,
                Id = boSuuTap.Id,
                Img = boSuuTap.Img,
                TenBoSuuTap = boSuuTap.TenBoSuuTap,
            });
        }

        // DELETE: api/BoSuuTap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoSuuTap(int id)
        {
            var boSuuTap = await _context.BoSuuTaps.FindAsync(id);
            if (boSuuTap == null)
            {
                return NotFound();
            }

            _context.BoSuuTaps.Remove(boSuuTap);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost("UploadImgBST/{Id}")]
        public async Task<IActionResult> UploadImgBST(IFormFile file, int Id)
        {
            if (file != null)
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//res//BstImgs",
                file.FileName);
                FileInfo File = new FileInfo(path);
                if(File.Exists)
                {
                    return BadRequest(new
                    {
                        message = "Đã tồn tại hình ảnh này ."
                    });
                }
                using (var stream = new FileStream(path, FileMode.CreateNew))
                {
                    try
                    {
                        
                        await file.CopyToAsync(stream);
                        var BST = _context.BoSuuTaps.FirstOrDefault(x => x.Id == Id);
                        if (BST != null)
                        {
                                BST.Img = file.FileName;
                                _context.BoSuuTaps.Update(BST);
                                await _context.SaveChangesAsync();
                                return Ok(new
                                {
                                    success = true,
                                    img = file.FileName,

                                });
                            
                      
                               
                            
                          
                        }

                    }
                    catch (Exception err)
                    {
                        return BadRequest(err.Message);
                    }

                }
            }

            return BadRequest();
        }
        [HttpDelete("RemoveImgBST/{id}")]
        public async Task<IActionResult> RemoveImgBST(string fileName,int id)
        {
            if (fileName != null)
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot//res//BstImgs",
                fileName);

                FileInfo file = new FileInfo(path);
                try
                {
                    if (file.Exists)
                    {
                        file.Delete();
                        var BST = await _context.BoSuuTaps.FirstOrDefaultAsync(x => x.Id == id);
                        if (BST != null)
                        {
                            BST.Img = null;
                            await _context.SaveChangesAsync();
                            return Ok(new
                            {
                                success = true,
                                path = path
                            }); ;
                        }

                    }
                }
                catch (Exception err)
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        [HttpDelete("RemoveProductsFormBST/{maSP}")]
        public async Task<IActionResult> RemoveProductsFormBST(string maSP)
        {
            var product = await _context.SanPhams.FindAsync(maSP);
            if (product is not null)
            {
                try
                {
                    product.IdBst = null;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception err)
                {
                    BadRequest(err.Message);
                }
            }
            return BadRequest("Không tòn tại sản phẩm này");
        }
        private bool BoSuuTapExists(int id)
        {
            return _context.BoSuuTaps.Any(e => e.Id == id);
        }
    }
}
