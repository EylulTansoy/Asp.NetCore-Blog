using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Model
{
    public class Account:BaseEntity
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [MaxLength(50)]
        [MinLength(10)]
        public string EMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [ForeignKey("RoleId")]
        public int? RoleId { get; set; }
        public Role Role{ get; set; }
    }
}
