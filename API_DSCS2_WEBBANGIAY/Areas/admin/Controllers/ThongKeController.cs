using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
           public DateTime dateStart { set; get; }
            public DateTime dateEnd { set; get; }

        }
        [HttpPost("{type}")]
        public async Task<IActionResult> DoanhThu(string type,DoanhThuBody body)
        { 
            var DayStart = body.dateStart.Day;
            var DayEnd = body.dateEnd.Day;
            var MonthEnd = body.dateStart.Month;
            var MonthStart = body.dateEnd.Month;
            var YearStart = body.dateStart.Year;
            var YearEnd = body.dateEnd.Year;
            IQueryable<HoaDon> query = Enumerable.Empty<HoaDon>().AsQueryable();
            List< Dictionary<string, decimal>> dicts = new List<Dictionary<string, decimal>>();
            List<string> labels = new List<string>();
            List<decimal> values = new List<decimal>();
            DateTime temp = body.dateStart;
            decimal doanhthu = 0;
            switch (type)
            {
                case "ngay":
                    for(int i =DayStart;i<=DayEnd ;i++)
                    {
                        var test = _context.HoaDons.Where(x => x.createdAt.Day ==temp.Day).Sum(x=>x.Thanhtien);
                        Dictionary<string ,decimal> rs = new Dictionary<string, decimal> ();

                        labels.Add(temp.ToShortDateString());
                        values.Add(test);
                        temp = new DateTime(body.dateStart.Year, body.dateStart.Month , temp.Day + 1);
                    }
                           

                    break;
                case "thang":
                    for (int i = MonthStart; i <= MonthEnd; i++)
                    {
                        var test = _context.HoaDons.Where(x => x.createdAt.Month == temp.Month).Sum(x => x.Thanhtien);
                        labels.Add(temp.ToShortDateString());
                        values.Add(test);
                        temp = new DateTime(body.dateStart.Year, temp.Month+1, temp.Day );
                    }

                    break;
                case "nam":
                    for (int i = YearStart; i <= YearEnd; i++)
                    {
                        var test = _context.HoaDons.Where(x => x.createdAt.Year == temp.Year).Sum(x => x.Thanhtien);
                        labels.Add(temp.ToShortDateString());
                        values.Add(test);
                        temp = new DateTime(temp.Year+1, temp.Month , temp.Day);
                    }
                    break;
                default:
                    break;
            }

            return Ok(new {labels,values});
        }
    }
}
