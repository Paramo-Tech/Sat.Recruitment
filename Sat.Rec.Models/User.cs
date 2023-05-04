using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sat.Rec.Models
{
    [Index(nameof(Name),IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Address), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Phone { get; set; }
        public int UserTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Money { get; set; }
    }
}