namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class  Branchs
    {
        public int ID { get; set; }
        public string MaChiNhanh { get; set; }
        public string TenChiNhanh { get; set; }
        public int? IDAddress { get; set; }
        public bool? isDefault { get; set; } = false;
        public DiaChi DiaChiNavigation { get; set; }
    }
}
