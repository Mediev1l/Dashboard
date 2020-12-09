using MyBoard.DataAccess.Data;
using MyBoard.Models.BoardModels;
using MyBoard.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        private readonly ApplicationDbContext _db;
        public CardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
