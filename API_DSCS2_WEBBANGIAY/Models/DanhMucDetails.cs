namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class DanhMucDetails
    {
        public int Id { get; set; }
        public string maSP { get; set; }
        public int danhMucId { get; set; }

        public virtual DanhMuc IdDanhMucNavigation { get; set; }
        public virtual SanPham IdSanPhamNavigation { get; set; }
    }
}
