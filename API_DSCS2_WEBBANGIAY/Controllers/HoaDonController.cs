using API_DSCS2_WEBBANGIAY.Models;
using API_DSCS2_WEBBANGIAY.Utils;
using API_DSCS2_WEBBANGIAY.Utils.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API_DSCS2_WEBBANGIAY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly ShoesEcommereContext _context;
        private readonly IMailService mailService;
        private readonly IConfiguration _configuration;
        public HoaDonController(ShoesEcommereContext context, IMailService mailService, IConfiguration Configuration)
        {
            this.mailService = mailService;
            _context = context;
            _configuration = Configuration;
        }

        [HttpPost("PostWithGuess")]
        public async Task<IActionResult> PostWithGuess([FromBody] DonhangModel body)
        {
            try
            {
                if(body.HoaDon.PhuongThucThanhToan=="COD")
                {
                var HoaDon = await CreateOrder(body);
                    if (HoaDon == null)
                    {
                        return BadRequest();
                    }
                    List<ChiTietHoaDon> cthd = new List<ChiTietHoaDon>();
                    foreach(var item in body.hoaDonDetails)
                    {

                        var chitietHoaDon = _context.ChiTietHoaDons.Include(x => x.MasanPhamNavigation)/*.Include(x => x.SizePhamNavigation)*/
                        .Include(x => x.MausacPhamNavigation).ThenInclude(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation).FirstOrDefault(x => x.IdHoaDon == item.IdHoaDon);
                        cthd.Add(chitietHoaDon);
                    }
                    await mailService.SendEmailAsync(new MailRequest { ToEmail = body.DiaChi.Email
                        , Subject = "Xác nhận đơn hàng", Body = FillData.Teamplate(HoaDon, cthd) });
                    return Ok();
                }
                else
                {
                    var HoaDon = await CreateOrder(body);
                    if(HoaDon == null)
                    {
                        return BadRequest();
                    }
                    if(HoaDon.HoaDon.idTaiKhoan!=null)
                    {
                         var tk = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.TenTaiKhoan.Trim() == HoaDon.HoaDon.idTaiKhoan.Trim());
                         tk.TienThanhToan = HoaDon.HoaDon.Thanhtien;
                        _context.TaiKhoans.Update(tk);
                         await _context.SaveChangesAsync();
                    }
                    else
                    {
                        HoaDon.HoaDon.TienThanhToan = HoaDon.HoaDon.Thanhtien;
                        _context.HoaDons.Update(HoaDon.HoaDon);
                        await _context.SaveChangesAsync();
                    }
                    return await VNPAY(HoaDon);

                }
                
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
           
            
        }
        //[HttpPost("PostWithUser")]
        //public async Task<IActionResult> PostWithUser([FromBody] DonhangModel body)
        //{
        //    if (body.HoaDon.PhuongThucThanhToan == "COD")
        //    {
        //        var HoaDon = await CreateOrder(body);
        //        if (HoaDon == null)
        //        {
        //            return BadRequest();
        //        }
        //        List<ChiTietHoaDon> cthd = new List<ChiTietHoaDon>();
        //        foreach (var item in body.hoaDonDetails)
        //        {

        //            var chitietHoaDon = _context.ChiTietHoaDons.Include(x => x.MasanPhamNavigation).Include(x => x.SizePhamNavigation)
        //                .Include(x => x.MausacPhamNavigation).ThenInclude(x => x.ChiTietHinhAnhs).ThenInclude(x => x.IdHinhAnhNavigation).FirstOrDefault(x => x.IdHoaDon == item.IdHoaDon);
        //            cthd.Add(chitietHoaDon);
        //        }
        //        await mailService.SendEmailAsync(new MailRequest { ToEmail = "truongkiet.hn289@gmail.com", Subject = "Xác nhận đơn hàng", Body = FillData.Teamplate(HoaDon, cthd) });
        //        return Ok();
        //    }
        //    else
        //    {
        //        var HoaDon = await CreateOrder(body);
        //        var tk = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.TenTaiKhoan.Trim() == HoaDon.HoaDon.idTaiKhoan.Trim());
        //        tk.TienThanhToan = HoaDon.HoaDon.TienThanhToan;
        //        _context.TaiKhoans.Update(tk);
        //        await _context.SaveChangesAsync();
        //        return await VNPAY(HoaDon);
        //    }
        //        return Ok();
        //}
        private async Task<IActionResult> VNPAY(DonhangModel HoaDon)
        {
            
            //Get Config Info
            string vnp_Returnurl = _configuration["VNPAYConfigs:vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = _configuration["VNPAYConfigs:vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = _configuration["VNPAYConfigs:vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = _configuration["VNPAYConfigs:vnp_HashSecret"]; //Chuoi bi mat
            if (string.IsNullOrEmpty(vnp_TmnCode) || string.IsNullOrEmpty(vnp_HashSecret))
            {
                //lblMessage.Text = "Vui lòng cấu hình các tham số: vnp_TmnCode,vnp_HashSecret trong file web.config";
                //return;
            }
            //Get payment input
            VNPay vnpay = new VNPay();
            vnpay.AddRequestData("vnp_Version", VNPay.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (HoaDon.HoaDon.Thanhtien * 100).ToString());
            //vnpay.AddRequestData("vnp_BankCode", body.BankCode?? "NCB");
            vnpay.AddRequestData("vnp_CreateDate", HoaDon.HoaDon.createdAt.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            var ipAddressParams = HttpContext.GetServerVariable("HTTP_X_FORWARDED_FOR");
            var remote_ADDR = HttpContext.GetServerVariable("REMOTE_ADDR"); ;
            vnpay.AddRequestData("vnp_IpAddr", Utils.Utils.GetIpAddress(ipAddressParams, remote_ADDR));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + HoaDon.HoaDon.Id);
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", HoaDon.HoaDon.Id.ToString());
            //Add Params of 2.1.0 Version
            //vnpay.AddRequestData("vnp_ExpireDate", txtExpire.Text);
            //info
            vnpay.AddRequestData("vnp_Bill_Mobile", HoaDon.DiaChi.Phone.Trim());
            vnpay.AddRequestData("vnp_Bill_Email", HoaDon.DiaChi.Email.Trim());
            vnpay.AddRequestData("vnp_Bill_Address", $"({HoaDon.DiaChi.AddressDsc}), {HoaDon.DiaChi.WardName}, {HoaDon.DiaChi.DistrictName}, {HoaDon.DiaChi.ProvinceName}");
            vnpay.AddRequestData("vnp_Bill_City", HoaDon.DiaChi.ProvinceID.ToString());
            vnpay.AddRequestData("vnp_Bill_Country", HoaDon.DiaChi.DistrictID.ToString());
            vnpay.AddRequestData("vnp_Bill_State", HoaDon.DiaChi.WardID.ToString());
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Ok(new
            {
                redirect=paymentUrl,
            });
        }
        [HttpGet("VNPAY_RETURN")]
        public async Task<IActionResult> VNPAY_RETURN()
        {
            var vnpayData = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            string vnp_HashSecret = _configuration["VNPAYConfigs:vnp_HashSecret"]; //Chuoi bi mat
            VNPay vnpay = new VNPay();
            foreach (string s in vnpayData)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(s, vnpayData[s]);
                }
            }
            long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            String vnp_SecureHash = vnpayData["vnp_SecureHash"];
            String TerminalID = vnpayData["vnp_TmnCode"];
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            String bankCode = vnpayData["vnp_BankCode"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                var hd = _context.HoaDons.FirstOrDefault(x => x.Id == orderId);
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    hd.status = 1;
                    hd.TienThanhToan = vnp_Amount;
                    _context.HoaDons.Update(hd);
                    await _context.SaveChangesAsync();
                    return Content("<h1>Thanh toán thành công</h1>");
                }
                else
                {
                    //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                    _context.HoaDons.Remove(hd);
                    await _context.SaveChangesAsync();
                    return BadRequest();
                }
            }
                return Ok();
        }
        private async Task<DonhangModel> CreateOrder(DonhangModel body)
        {
            if (body.hoaDonDetails.Count <= 0 || body.hoaDonDetails ==null|| body.HoaDon ==null || body.DiaChi ==null)
            {
                return null;
            }
            using (var _context = new ShoesEcommereContext())
            {
                try
                {
                    
                   
                    if (body.HoaDon.idTaiKhoan == null)
                    {
                        DiaChi DiaChi = new DiaChi();
                        DiaChi = body.DiaChi;
                        _context.DiaChis.Add(DiaChi);
                        await _context.SaveChangesAsync();
                        body.HoaDon.IdDiaChi = DiaChi.ID;
                        _context.HoaDons.Add(body.HoaDon);
                    }
                    else
                    {
                        if (body.HoaDon.IdDiaChi == null)
                        {
                            return null;
                        }
                        _context.HoaDons.Add(body.HoaDon);
                    }
                    await _context.SaveChangesAsync();
                    foreach (var item in body.hoaDonDetails)
                    {
                        ChiTietHoaDon cthd = new ChiTietHoaDon();
                        cthd = item;
                        cthd.IdHoaDon = body.HoaDon.Id;
                        cthd.IDSanPham = item.IDSanPham;
                        cthd.Color = item.Color;
                        cthd.Size = item.Size;
                        _context.ChiTietHoaDons.Add(cthd);
                    }
                    await _context.SaveChangesAsync();
                   
                    body.HoaDon.status = 0;
                    await _context.SaveChangesAsync();
                    return body;

                }
                catch (Exception err)
                {
                    throw err;
                }
            }
        }
        [HttpPut("UpdateProductOrder/{id}")]
        public async Task<IActionResult> UpdateProductOrder(ChiTietHoaDon cthd)
        {
            try
            {
                //var product = await _context.SoLuongDetails.FirstOrDefaultAsync(x => x.maSanPham == cthd.MasanPham && x.maMau == cthd.Color && x._idSize == cthd.Size);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest();
            }
        }

    }
}
