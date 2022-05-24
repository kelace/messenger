using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Application
{
    public class CreateMessageOptions
    {
        public string toID { get; set; }
        public string fromID { get; set; }
        public string text { get; set; }
    }
}
