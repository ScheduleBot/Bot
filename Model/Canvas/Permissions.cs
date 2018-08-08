using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Model
{
    public class Permissions
    {
        public bool attach { get; set; }
        public bool update { get; set; }
        public bool reply { get; set; }
        public bool delete { get; set; }
    }
}