﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_DSCS2_WEBBANGIAY.Models;
using API_DSCS2_WEBBANGIAY.Utils;
using System.Collections;
using API_DSCS2_WEBBANGIAY.Areas.admin.Models;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public DanhMucController(ShoesEcommereContext context)
        {
            _context = context;
        }

        // GET: api/DanhMuc
        [HttpGet]
        public async Task<IActionResult> GetDanhMucUI()
        {
            try
            {
                Dictionary<string, List<DanhMuc>> dic = new Dictionary<string, List<DanhMuc>>();
                var Bac0 = await _context.DanhMucs.Where(x => x.ParentCategoryID == -1).ToListAsync();
                var danhmucs = await _context.DanhMucs.ToListAsync();
                List<DanhMuc> Bac1 = new List<DanhMuc>();
                List<DanhMuc> Bac2 = new List<DanhMuc>();
                List<Menu> menu = new List<Menu>();
                for (int i = 0; i < Bac0.Count; i++)
                {
                    Menu Level0 = new Menu();
                    Level0.info = Bac0[i];
                    menu.Add(Level0);
                    var lv1 = danhmucs.Where(x => x.ParentCategoryID == Bac0[i].Id).ToList();
                    if (lv1.Count > 0)
                    {

                        for (int item1 = 0; item1 < lv1.Count; item1++)
                        {
                            Level1 Level1 = new Level1();
                            Level1.info = lv1[item1];
                            menu[i].Children.Add(Level1);
                            var lv2 = danhmucs.Where(x => x.ParentCategoryID == lv1[item1].Id).ToList();
                            if (lv2.Count > 0)
                            {

                                for (int item2 = 0; item2 < lv2.Count; item2++)
                                {
                                    Level2 Level2 = new Level2();
                                    Level2.info = lv2[item2];
                                    menu[i].Children[item1].Children.Add(Level2);
                                }
                            }
                        }

                    }
                }
                return Ok(new
                {
                    menu,
                    danhmucs = danhmucs,
                });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
           
            
           
          
        }
        [HttpGet("GetAllDanhMuc")]
        public async Task<IActionResult> GetAllDanhMuc()
        {
            var danhmucs = await _context.DanhMucs.ToListAsync();
            return Ok(danhmucs);
        }
        // GET: api/DanhMuc/5
        [HttpGet("GetDanhMucByParentId/{id}")]
        public async Task<ActionResult> GetDanhMucByParentId(int id)
        {
            
            var danhMuc =  _context.DanhMucs.Where(x=>x.ParentCategoryID == id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return Ok(danhMuc);
        }

        // PUT: api/DanhMuc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDanhMuc(int id, DanhMuc danhMuc)
        {
            //if (id != danhMuc.Id)
            //{
            //    return BadRequest();
            //}
            danhMuc.Slug = CustomSlug.Slugify(danhMuc.TenDanhMuc);
            _context.Entry(danhMuc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucExists(id))
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

        // POST: api/DanhMuc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhMuc>> PostDanhMuc(DanhMuc danhMuc)
        {
            if (String.IsNullOrEmpty(danhMuc.TenDanhMuc))  return BadRequest();
            
           
            try
            {
                var parentDM = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Id == danhMuc.ParentCategoryID);
                if (parentDM != null)
                {
                    danhMuc.Slug = CustomSlug.Slugify(parentDM.Slug+" "+danhMuc.TenDanhMuc);
                }
                else
                {
                    danhMuc.Slug = CustomSlug.Slugify(danhMuc.TenDanhMuc);
                }
                _context.DanhMucs.Add(danhMuc);
                await _context.SaveChangesAsync();
                var danhMucObj = await _context.DanhMucs.FirstOrDefaultAsync(x => x.Id == danhMuc.Id);
                if(danhMucObj is not null)
                {
                    return Ok(new
                    {
                        id = danhMucObj.Id,
                        tenDanhMuc = danhMucObj.TenDanhMuc,
                        slug = danhMucObj.Slug,
                        parentCategoryId = danhMucObj.ParentCategoryID
                    });
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
                return BadRequest(err.Message);
            }
            return BadRequest();
           
        }

        // DELETE: api/DanhMuc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhMuc(int id)
        {
            var danhMuc = await _context.DanhMucs.FindAsync(id);
            if (danhMuc == null)
            {
                return NotFound();
            }
            var danhMucs  = _context.DanhMucs.Where(x=>x.ParentCategoryID==id ).ToList();
            if(danhMuc is not null)
            {
                foreach(var dm in danhMucs)
                {
                    var danhmucChildrens = _context.DanhMucs.Where(x=>x.ParentCategoryID == dm.Id).ToList();
                    foreach(var danhmucChild in danhmucChildrens)
                    {
                        _context.DanhMucs.Remove(danhmucChild);
                    }
                    _context.DanhMucs.Remove(dm);
                }
                _context.DanhMucs.Remove(danhMuc);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool DanhMucExists(int id)
        {
            return _context.DanhMucs.Any(e => e.Id == id);
        }
    }
}
