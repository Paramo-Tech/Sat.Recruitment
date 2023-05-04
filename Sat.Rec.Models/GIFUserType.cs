using System.ComponentModel.DataAnnotations.Schema;

namespace Sat.Rec.Models
{
    public class GIFUserType
    {
        public int GIFUserTypeId { get; set; }
        public int UserTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LowerLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UpperLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GIF { get; set; }
    }
}
