using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models.ViewModels
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
    }

    public class Register : Login
    {
        [Required]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        public Login Login { get; set; }
        public Register Register { get; set; }
    }
}
