﻿using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class Types : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        public Types(ShoesEcommereContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var types = await _context.Types.ToListAsync();
           
            return Ok(types);
        }
        [HttpPost]
        public async Task<IActionResult> Post(API_DSCS2_WEBBANGIAY.Models.Type type)
        {
            try
            {
                _context.Types.Add(type);
                await _context.SaveChangesAsync();
                
                return Ok(type);
            }catch(Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}
