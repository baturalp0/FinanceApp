﻿namespace Finance.Entities.Models
{
    public class Log
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public string action_name { get; set; }
        public string controller_name { get; set; }
    }
}
