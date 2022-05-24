using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UI.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "The password cannot be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The password cannot be empty")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "The password cannot be empty")]

        [Compare(nameof(Password), ErrorMessage = "Password mismatch")]
        public string PasswordConfirm { get; set; }
    }
}
