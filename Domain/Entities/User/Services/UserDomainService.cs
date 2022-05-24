using Chat.Domain.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Domain.Entities.Services
{
    public class UserDomainService : IUserDomainService
    {
        IUserRepository _userRepository;
        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool IsOfferExist(User participant, User secondParticipant)
        {
            var offer = participant.Offers.Where(offer => (offer.SenderId == participant.Id && offer.ReceiverId == secondParticipant.Id) || (offer.SenderId == secondParticipant.Id && offer.ReceiverId == participant.Id));

            if (offer is null) return false;

            return true;
        }
        public bool IsRelationBetweenUserExist(User participant, User secondParticipant)
        {
           var relations = participant.Relations.Where(r => (r.ReceiverId == participant.Id && r.SenderId == secondParticipant.Id) || (r.SenderId == secondParticipant.Id && r.ReceiverId == participant.Id));

           if (relations.Count() != 0) return false;

           return true;
        }

        public void RemoveOffer(User participant, User secondParticipant)
        {
            var offer = participant.Offers.Where(offer => (offer.SenderId == participant.Id && offer.ReceiverId == secondParticipant.Id) || (offer.SenderId == secondParticipant.Id && offer.ReceiverId == participant.Id)).SingleOrDefault();
            
            if (offer != null)
            {
                participant.RemoveOffer(offer);
                secondParticipant.RemoveOffer(offer);
            }
        }

        public Offer FindOfferBetweenUsers(User participant, User secondParticipant)
        {
            var offer = participant.Offers.Where(offer => (offer.SenderId == participant.Id && offer.ReceiverId == secondParticipant.Id) || (offer.SenderId == secondParticipant.Id && offer.ReceiverId == participant.Id)).SingleOrDefault();
            return offer;
        }

        public void AcceptOffer(User sender, User receiver)
        {
            var relation = receiver.AcceptOffer(sender);

            sender.AddRelation(relation);
        }
    }
}
