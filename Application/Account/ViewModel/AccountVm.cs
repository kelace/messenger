using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Application.ViewModel
{
    public class AccountVm
    {
        public List<InterlocutorVm> Users { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Photo { get; set; }
        public InterlocutorVm DialogUser { get; set; }
        public string Token { get; set; }
    }
}
