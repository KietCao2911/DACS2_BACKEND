using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_DSCS2_WEBBANGIAY.Areas.admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;

        
        public ThongKeController(ShoesEcommereContext context)
        {
            _context = context;
        }
        public class DoanhThuBody
        {
            public string type { get; set; }
           public DateTime dateStart { set; get; }

            public DateTime dateEnd { set; get; }

        }
        [HttpPost("{id}")]
        public async Task<IActionResult> DoanhThu(string id,DoanhThuBody body)
        {
            var type = body.type;
            var DayStart = body.dateStart.Day;
            var DayEnd = body.dateEnd.Day;
            var MonthStart = body.dateStart.Month;
            var MonthEnd = body.dateEnd.Month;
            var YearStart = body.dateStart.Year;
            var YearEnd = body.dateEnd.Year;
            List< Dictionary<string, decimal>> dicts = new List<Dictionary<string, decimal>>();
            List<string> labels = new List<string>();
            List<decimal> values = new List<decimal>();
            ArrayList details = new ArrayList();
            DateTime temp = body.dateStart;
            decimal doanhthu = 0;
            List <HoaDon> hd  = new List<HoaDon> ();
            var ggs = _context.HoaDons.Include(x => x.DiaChiNavigation);
            switch (type)
            {
                case "ngay":
                    for(int i =DayStart;i<=DayEnd ;i++)
                    {
                        var obj = _context.HoaDons.Where(x => x.createdAt.Day == i).Include(x => x.DiaChiNavigation).Where(x=>x.status==1).ToList();
                        var test = obj.Sum(x=>x.Thanhtien);
                        Dictionary<string ,decimal> rs = new Dictionary<string, decimal> ();
                        labels.Add("Ngày " + i);
                        values.Add(test);
                        details.Add(obj);
                        temp = new DateTime(body.dateStart.Year, body.dateStart.Month ,i);
                        doanhthu += test;
                    }
                           

                    break;
                case "thang":
                    for (int i = MonthStart; i <= MonthEnd; i++)
                    {
                        var obj = _context.HoaDons.Where(x => x.createdAt.Month == i).Include(x => x.DiaChiNavigation).Where(x => x.status == 1); ;
                        var test = obj.Sum(x => x.Thanhtien);
                        labels.Add("Tháng "+i);
                        values.Add(test);
                        temp = new DateTime(DateTime.Now.Year, i, temp.Day );
                        var gg = new DateTime(2001, 12, 1);
                        details.Add(obj);
                        doanhthu += test;
                    }

                    break;
                case "nam":
                    for (int i = YearStart; i <= YearEnd; i++)
                    {
                        var obj = _context.HoaDons.Where(x => x.createdAt.Year == temp.Year).Include(x => x.DiaChiNavigation).Where(x => x.status == 1); ;
                        var test = obj.Sum(x => x.Thanhtien);
                        labels.Add("Năm  " +i);
                        values.Add(test);
                        temp = new DateTime(i, temp.Month, temp.Day);
                        details.Add(obj);
                        doanhthu += test;
                    }
                    break;
                default:
                    break;
            }

            return Ok(new {labels,values,details, doanhthu });
        }
        //[HttpPost("{id}")]
        //public async Task<IActionResult> ThongKeTheoSanPham(string id, DoanhThuBody body)
        //{

        //    return Ok();
        //}
        [HttpPost("bao-cao-xuat-nhap-ton")]
        public async Task<IActionResult> BaoCaoXuatNhapTon(DoanhThuBody body)
        {
            var values = _context.ChiTietPhieuNhaps
                .Include(x=>x.PhieuNhapNavigation)
                .Where(x => x.PhieuNhapNavigation.NgayNhap > body.dateStart && x.PhieuNhapNavigation.NgayNhap <= body.dateEnd);
               
            return Ok(values);
        }
    }
}
