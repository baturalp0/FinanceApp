using System;
using System.Collections.Generic;
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
        public string name { get; set; }

    }
}
