using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SportStore.Web.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [UIHint("Password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";

    }
}
