using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Entities
{
    public class Photo : BaseEntity
    {
        public string Path { get; set; }

        public Photo() : base()
        {

        }
    }
}
