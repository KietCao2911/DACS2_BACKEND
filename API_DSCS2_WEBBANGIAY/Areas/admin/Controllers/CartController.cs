using API_DSCS2_WEBBANGIAY.Areas.admin.Models;
using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;


        public CartController(ShoesEcommereContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var items = HttpContext.Session.Get("Cart");
            if (items != null)
            {
                var data = JsonSerializer.Deserialize<List<CartItem>>(items);
                var totalBill = data.Sum(x => x.total);
                return Ok(data.ToList());
            }

            return BadRequest();
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(string maSp)
        {
            var items = HttpContext.Session.Get("Cart");

            if (items != null)
            {
                var product = await _context.SanPhams.Where(x => x.SoLuongTon > 0).FirstOrDefaultAsync(x => x.MaSanPham == maSp);
                Stream stream = new MemoryStream(items);
                if (product is not null)
                {

                    var data = await JsonSerializer.DeserializeAsync<List<CartItem>>(stream);
                    var item = data.FirstOrDefault(x => x.items.MaSanPham == product.MaSanPham);
                    if (item is not null)
                    {
                        item.qty++;
                        item.total = (decimal)(item.qty * item.items.GiaBan);
                        var cart = JsonSerializer.Serialize(data);
                        HttpContext.Session.SetString("Cart", cart);
                        return Ok(new
                        {
                            success = true,
                            items = data
                        });
                    }
                    else
                    {
                        data.Add(new CartItem(product, 1));
                        var cart = JsonSerializer.Serialize(data);
                        HttpContext.Session.SetString("Cart", cart);
                        return Ok(new
                        {
                            success = true,
                            items = data
                        });
                        return BadRequest();
                    }

                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                var sach = await _context.SanPhams.FirstOrDefaultAsync(x => x.MaSanPham == maSp);
                if (sach is not null)
                {
                    var cartitems = new List<CartItem>();
                    cartitems.Add(new CartItem(sach, 1));
                    var item = JsonSerializer.Serialize(cartitems);
                    HttpContext.Session.SetString("Cart", item);

                    return Ok(new
                    {
                        success = true,
                        items = cartitems
                    });
                }
                return BadRequest();
            }

        }
        [HttpPost("UpdateQty")]
        public async Task<IActionResult> UpdateQty(string maSP, int qty)
        {
            var items = HttpContext.Session.Get("Cart");
            Stream stream = new MemoryStream(items);
            var data = await JsonSerializer.DeserializeAsync<List<CartItem>>(stream);
            var res = data.FirstOrDefault(x => x.items.MaSanPham == maSP);
            if (res != null)
            {
                res.qty = qty;
                res.total = (decimal)res.items.GiaBan * qty;
                var cart = JsonSerializer.Serialize(data);
                HttpContext.Session.SetString("Cart", cart);
                return Ok(new
                {
                    success = true,
                    qty = data.Sum(x => x.qty),
                    item = res,
                    finalPrice = data.Sum(x => x.total)
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
        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCart(string maSP)
        {
            var items = HttpContext.Session.Get("Cart");
            Stream stream = new MemoryStream(items);
            var data = await JsonSerializer.DeserializeAsync<List<CartItem>>(stream);
            var res = data.FirstOrDefault(x => x.items.MaSanPham == maSP);
            if (res != null)
            {
                data.Remove(res);
                var cart = JsonSerializer.Serialize(data);
                HttpContext.Session.SetString("Cart", cart);
                return Ok(new
                {
                    success = true,
                    qtyDelete = res.qty,
                    item = res,
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
        [HttpGet("CheckQtyCart")]
        public async Task<IActionResult> CheckQtyCart()
        {
            var items = HttpContext.Session.Get("Cart");

            if (items != null)
            {
                Stream stream = new MemoryStream(items);
                var data = await JsonSerializer.DeserializeAsync<List<CartItem>>(stream);
                var cart = JsonSerializer.Serialize(data);
                HttpContext.Session.SetString("Cart", cart);
                return Ok(new
                {
                    success = true,
                    qty = data.Sum(x => x.qty),
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
}