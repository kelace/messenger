using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chat.UI.Models.Request
{
    public class CreateRelationRequest
    {
        [Required]
        public string toId { get; set; }
    }
}
