using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Entities.Models
{
    public class User
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunlu alandır.")]
        [MinLength(5,ErrorMessage = "Minimum 5 karakter girilmelidir.")]
        [MaxLength(20,ErrorMessage = "Maximum 20 karakter girilmelidir.")]
        public string nick_name { get; set; }

        [Required(ErrorMessage = "E-mail zorunlu alandır.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Şifre zorunlu alandır.")]
        [MinLength(8, ErrorMessage = "Minimum 8 karakter girilmelidir.")]
        [MaxLength(20, ErrorMessage = "Maximum 20 karakter girilmelidir.")]
        public string password { get; set; }
        public int role_id { get; set; }
    }
}
