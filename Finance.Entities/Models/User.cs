using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Entities.Models
{
    public class User
    {
        public int id { get; set; }
        public string nick_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
