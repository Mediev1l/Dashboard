using MyBoard.DataAccess.Data;
using MyBoard.Models.BoardModels;
using MyBoard.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository
{
    public class PriorityRepository : Repository<Priority>, IPriorityRepository
    {
        private readonly ApplicationDbContext _db;
        public PriorityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
