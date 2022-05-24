using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Infrastructure
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Offer> Offers {get; set;}
        public List<Relation> Relations {get; set;}
        public List<Message> Messages { get; set; }
    }
}
