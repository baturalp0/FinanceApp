using System.ComponentModel.DataAnnotations;

namespace Finance.Entities.Models
{
    public class Amount
    {
        public int id { get; set; }
        public int user_id { get; set; }

        //[Required(ErrorMessage = "Miktar zorunlu alandır.")]
        public int amount { get; set; }

        public bool type_ { get; set; } //type özel bir keyword'tür o yüzden sonuna _ konur. Karışmasın diye.

        //[Required(ErrorMessage = "İşlem zorunlu alandır.")]
        //[MaxLength(30, ErrorMessage = "Maximum 30 karakter alır.")]
        //[MinLength(2, ErrorMessage = "Minimum 2 karakter olabilir.")]
        public string name { get; set; }

    }
}
