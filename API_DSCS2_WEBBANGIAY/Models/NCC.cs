namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class NCC
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone    { get; set; }
        public int? IDDiaChi { get; set; }
        public virtual DiaChi DiaChiNavigation { get; set; }
    }
}
