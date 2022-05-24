using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Application.ViewModel
{
    public class InterlocutorVm 
    {
        public string Id { get; set; }
        public List<Message> Messages { get; set; }
        public string Name { get; set; }
        public OfferStatus Status { get; set; }
        public bool IsThisUserInitiatorOfRelation { get; set; }
        public string Photo { get; set; }
    }
}
