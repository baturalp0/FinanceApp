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
        [MinLength(3,ErrorMessage = "Minimum 3 karakter girilmelidir.")]
        [MaxLength(20,ErrorMessage = "Maximum 20 karakter girilmelidir.")]
        
        public string nick_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
