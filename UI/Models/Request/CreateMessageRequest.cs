using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UI.Models.Request
{
    public class CreateMessageRequest
    {
        public string toId { get; set; }
        public string text { get; set; }
    }
}
