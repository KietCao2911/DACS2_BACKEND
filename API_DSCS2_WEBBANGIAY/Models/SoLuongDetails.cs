namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class SoLuongDetails
    {
        public int _id { set; get; }
        public string maMau { set; get; }
        public string maSanPham { set; get; }
        public int _idSize { set; get; }
        public int? Soluong { set; get; }
        public int? SoluongTon { set; get; }
        public int? SoluongBan { set; get; }
        public virtual MauSac IdMauSacNavigation { set; get; }
        public virtual Size IdSizeNavigation { set; get; }
        public virtual SanPham IdSanPhamNavigation { set; get; }
    }
}
