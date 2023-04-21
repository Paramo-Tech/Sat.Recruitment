using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Sat.Recruitment.Api.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [ForeignKey("UserTypeModel")]
        public Guid UserTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Money { get; set; }
    
        public virtual UserTypeModel UserType { get; set; }

    }
}
