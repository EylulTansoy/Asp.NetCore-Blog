using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDeneme2.Dto
{
    public class SignInDto
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Password")]
        [DataType(DataType.Password, ErrorMessage = "Şifreniz geçerli değil")]
        public string Password { get; set; }
    }
}
