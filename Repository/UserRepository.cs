using MyBoard.DataAccess.Data;
using MyBoard.Models.Identity;
using MyBoard.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
