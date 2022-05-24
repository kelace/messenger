using System;

namespace Chat.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string fromId { get; set; }
        public User from { get; set; }
        public string toId { get; set; }
        public User to { get; set; }
        public string text { get; set; }
        public DateTime DateCreate { get; set; }

        public Message()
        {
            this.DateCreate = DateTime.UtcNow;
        }
    }
}
