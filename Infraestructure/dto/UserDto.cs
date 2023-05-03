using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infraestructure.dto
{
    [Table("users")]
    public class UserDto
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("usertype")]
        public string UserType { get; set; }
        [Column("money")]
        public decimal Money { get; set; }

    }

    public enum UserType
    {
        Normal,
        SuperUser,
        Premium
    }
}
