using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Entities.Models
{
    public class Amount
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int amount { get; set; }
        public bool type_ { get; set; } //type özel bir keyword'tür o yüzden sonuna _ konur. Karışmasın diye.

        [Required(ErrorMessage = "amount zorunlu alandır.")]
        [MaxLength(200,ErrorMessage = "Maximum 200 karakter alır.")]
        [MinLength(3,ErrorMessage = "Minimum 3 karakter olabilir.")]

        public string name { get; set; }

    }
}
