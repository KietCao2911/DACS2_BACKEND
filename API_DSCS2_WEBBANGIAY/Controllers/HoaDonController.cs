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
                var HoaDon = await CreateOrder(body);

                return await VNPAY(HoaDon);
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
           
            
        }
        [HttpPost("PostWithUser")]
        public async Task<IActionResult> PostWithUser([FromBody] DonhangModel body)
        {
            return Ok();
        }
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
            return Ok(paymentUrl);
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
                    return Ok();
                }
                else
                {
                    //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                    var diachi = await _context.DiaChis.FirstOrDefaultAsync(x => x.ID == hd.IdDiaChi);
                    _context.DiaChis.Remove(diachi);
                    await _context.SaveChangesAsync();
                    return BadRequest();
                }
            }
                return Ok();
        }
        private async Task<DonhangModel> CreateOrder(DonhangModel body)
        {
            using (var _context = new ShoesEcommereContext())
            {
                try
                {
                    DiaChi DiaChi = new DiaChi();
                    foreach (var item in body.hoaDonDetails)
                    {
                        var ctsl = await _context.SoLuongDetails.FirstOrDefaultAsync(x => x.maSanPham == item.MasanPham && x.maMau == item.Color && x._idSize == item.Size);
                        if (ctsl is not null)
                        {
                            if (ctsl.Soluong <= 0 && (ctsl.Soluong - item.Qty < 0))
                            {
                                return null;
                            }
                            else
                            {
                                ctsl.SoluongTon -= item.Qty;
                                ctsl.SoluongBan += (int)item.Qty;
                                _context.SoLuongDetails.Update(ctsl);
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }

                    DiaChi = body.DiaChi;
                    _context.DiaChis.Add(DiaChi);
                    await _context.SaveChangesAsync();
                    body.HoaDon.IdDiaChi = DiaChi.ID;
                    _context.HoaDons.Add(body.HoaDon);
                    await _context.SaveChangesAsync();
                    foreach (var item in body.hoaDonDetails)
                    {
                        ChiTietHoaDon cthd = new ChiTietHoaDon();
                        cthd = item;
                        cthd.IdHoaDon = body.HoaDon.Id;
                        cthd.MasanPham = item.MasanPham.Trim();
                        cthd.Color = item.Color;
                        cthd.Size = item.Size;
                        _context.ChiTietHoaDons.Add(cthd);
                    }
                    await _context.SaveChangesAsync();
                    var cthdTemp = _context.ChiTietHoaDons.Include(x => x.MasanPhamNavigation).Where(x => x.IdHoaDon == body.HoaDon.Id).ToList();
                    string cthdString = TemplateConfirmcs.DetailsOrder(cthdTemp);
                    await mailService.SendEmailAsync(new MailRequest { ToEmail = "truongkiet.hn289@gmail.com", Subject = "Xác nhận đơn hàng", Body = TemplateConfirmcs.Teamplate(body.HoaDon, cthdString) });
                    body.HoaDon.status = 0;
                    await _context.SaveChangesAsync();
                    return body;

                }
                catch (Exception err)
                {
                    return null;
                }
            }
        }

    }
}
