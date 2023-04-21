using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sat.Recruitment.Api.Models
{
    [Table("UserType")]
    public class UserTypeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserTypeId { get; set; }
        public string Name { get; set; }
    }
}
