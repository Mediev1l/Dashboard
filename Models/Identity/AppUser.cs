using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBoard.Models.BoardModels;


namespace MyBoard.Models.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string NickName { get; set; }
        public string ImageUrl { get; set; }
        virtual public ICollection<UserBoard> Boards { get; set; }

    }
}
