using System;
using System.Collections.Generic;

#nullable disable

namespace API_DSCS2_WEBBANGIAY.Models
{
    public partial class Size
    {
        public Size()
        {
            SoLuongDetails = new HashSet<SoLuongDetails>();
            
        }

        public int Id { get; set; }
        public int Size1 { get; set; }
        public virtual ICollection<SoLuongDetails> SoLuongDetails { get; set; }
        
    }
}
