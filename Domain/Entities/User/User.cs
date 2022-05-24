using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Chat.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public List<Offer> SendedOffers { get; set; } 
        public List<Offer> ReceivedOffers { get; set; } 
        public List<Relation> SendedRelation { get; set; } 
        public List<Relation> ReceivedRelations { get; set; } 
        public List<Message> SendedMessages { get; set; } 
        public List<Message> ReceivedMessages { get; set; } 

        [NotMapped]
        public List<Offer> Offers
        {
            get
            {
                return SendedOffers.Concat(ReceivedOffers).ToList();
            }
        }
        [NotMapped]
        public List<Relation> Relations
        {
            get
            {
                return SendedRelation.Concat(ReceivedRelations).ToList();
            }
        }
        [NotMapped]
        public List<Message> Messages
        {
            get
            {
                return SendedMessages.Concat(ReceivedMessages).ToList();
            }
        }

        public string Photo { get; set; }
        public User() : base()
        {

        }

        public User(string name) : base()
        {
            Name = name;
        }

        public Offer SendOfferToUser(User receiver)
        {
            var offer = new Offer(this, receiver);

            SendedOffers.Add(offer);

            return offer;
        }
        public Relation AcceptOffer(User sender)
        {
            var relation = new Relation(sender, this);

            ReceivedRelations.Add(relation);

            return relation;
        }

        public void RemoveOffer(Offer offer)
        {
            SendedOffers.Remove(offer);

            ReceivedOffers.Remove(offer);
        }

        public void AddRelation(Relation relation)
        {
            SendedRelation.Add(relation);
        }

        public void DeclineOffer(User user)
        {
            var offer = Offers.Where(offer => (offer.SenderId == Id && offer.ReceiverId == user.Id) || (offer.SenderId == user.Id && offer.ReceiverId == Id)).Single();
           
            if(offer != null)
            {
                SendedOffers.Remove(offer);
            }
        }

        public void ReceiveOffer(Offer offer)
        {
            ReceivedOffers.Add(offer);
        }

        public void RemoveUserRelation(User user)
        {
            foreach(var relation in Relations)
            {
                if((relation.Sender.Id == Id && relation.Receiver.Id == user.Id) || (relation.Sender.Id == user.Id && relation.Receiver.Id == Id) )
                {
                    SendedRelation.Remove(relation);

                    ReceivedRelations.Remove(relation);

                    return;
                }
            }
        }
    }
}
