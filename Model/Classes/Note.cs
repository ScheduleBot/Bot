using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Model.Classes
{
    public class Note
    {
        public int ID { get; set; }
        //public int ClassID { get; set; }
        public int ScheduleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public int Days { get; set; }
    }
}