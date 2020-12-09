using MyBoard.DataAccess.Data;
using MyBoard.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Card = new CardRepository(_db);
            Status =  new StatusRepository(_db);
            Priority = new PriorityRepository(_db);
            User = new UserRepository(_db);
            Board = new BoardRepository(_db);
            UserBoard = new UserBoardRepository(_db);
            StoredProcedure = new StoredProcedure(_db);
        }

        public IStoredProcedure StoredProcedure { get; private set; }

        public ICardRepository Card { get; private set; }

        public IStatusRepository Status { get; private set; }

        public IPriorityRepository Priority { get; private set; }

        public IUserRepository User { get; private set; }

        public IBoardRepository Board { get; private set; }

        public IUserBoardRepository UserBoard { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
