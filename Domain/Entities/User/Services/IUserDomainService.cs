namespace Chat.Domain.Entities.Services
{
    public interface IUserDomainService
    {
        public bool IsOfferExist(User participant, User secondParticipant);
        public bool IsRelationBetweenUserExist(User participant, User secondParticipant);
        public void AcceptOffer(User sender, User receiver);
        public void RemoveOffer(User participant, User secondParticipant);
        public Offer FindOfferBetweenUsers(User participant, User secondParticipant);
    }
}