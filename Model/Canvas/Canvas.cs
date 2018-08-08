using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleEchoBot.Model
{
    public class Canvas
    {
        public Permissions permissions {get; set; }
        public Author author { get; set; }
        public DiscussionTopic discussionTopic { get; set; }
        public RootObject rootObject { get; set; }
    }
}