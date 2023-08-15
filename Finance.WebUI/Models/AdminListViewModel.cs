namespace Finance.WebUI.Models
{
    public class AdminListViewModel
    {
        public int user_id { get; set; }  //user_id
        public int id { get; set; }  //amount id
        public int amount { get; set; }

        public bool type_ { get; set; }

        public string name { get; set; }

        public string password { get; set; }

        public string nick_name { get; set; }
    }
}
