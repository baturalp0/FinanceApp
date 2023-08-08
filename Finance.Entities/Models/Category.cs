using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Entities.Models
{
    public class Category
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
    }
}
