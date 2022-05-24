using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public class Relation : BaseEntity
    {
        public string SenderId { get; private set; }
        public User Sender { get; private set; }
        public string ReceiverId { get; private set; }
        public User Receiver { get; private set; }

        public Relation() : base()
        {

        }
        public Relation(User sender, User receiver) : base()
        {
            Sender =  sender;
            Receiver = receiver;
        }
    }

}
