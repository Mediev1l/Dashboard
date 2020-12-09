using MyBoard.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository.IRepository
{
    public interface IUserRepository : IRepository<AppUser>
    {
    }
}
