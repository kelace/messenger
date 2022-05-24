using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "The name cannot be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The password cannot be empty")]
        public string Password { get; set; }
    }
}
