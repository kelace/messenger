using System;
using System.Collections.Generic;


namespace Chat.Domain.Entities
{
    public class Offer : BaseEntity
    {
        public string SenderId { get; private set; }
        public User Sender { get; private set; }
        public string ReceiverId { get; private set; }
        public User Receiver { get; private set; }
    
        public OfferStatus Status { get; private set; }

        public Offer()
        {

        }
        public Offer(User sender, User receiver)
        {
            Sender = sender;
            Receiver = receiver;
            Status = OfferStatus.Sended;
        }
    }

    public enum OfferStatus
    {
        Accepted,
        Sended,
        Nothing
    }
}
