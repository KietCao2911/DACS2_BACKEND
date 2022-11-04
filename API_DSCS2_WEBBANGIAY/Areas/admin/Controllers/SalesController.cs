using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API_DSCS2_WEBBANGIAY.Models;
using System.Collections.Generic;
using System;
using API_DSCS2_WEBBANGIAY.Areas.admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        public readonly ShoesEcommereContext _context;

        public SalesController(ShoesEcommereContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSales(SaleBody body)
        {
           
            try
            {
                var maSP = body.sales[0].MaSanPham;
                if (body.sales.Count < 0) return BadRequest();
                _context.Sales.Add(body.sale);
                await _context.SaveChangesAsync();
                foreach(var item in body.sales)
                {
                    var product = _context.SanPhams.FirstOrDefault(x => x.MaSanPham == item.MaSanPham);
                    item.IdSale = body.sale.Id;
                    _context.ChiTietSales.Add(item);
                    var CountGiamGia =(decimal) (product.GiaBan * item.Giamgia) / 100;
                    product.GiaDaGiam =CountGiamGia;
                    product.GiaBan -= CountGiamGia;
                    await _context.SaveChangesAsync();
                }
            }catch(Exception err)
            {
                Console.Write(err.ToString()); 
                return BadRequest();
            }
            return Ok(body);
        }
    }
}
