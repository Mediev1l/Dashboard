using MyBoard.DataAccess.Data;
using MyBoard.Models.BoardModels;
using MyBoard.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository
{
    public class UserBoardRepository : Repository<UserBoard>, IUserBoardRepository
    {
        private readonly ApplicationDbContext _db;
        public UserBoardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
