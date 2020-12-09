using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository Card { get; }
        IStatusRepository Status { get; }
        IPriorityRepository Priority { get; }
        IUserRepository User { get; }
        IBoardRepository Board { get; }
        IUserBoardRepository UserBoard { get; }
        IStoredProcedure StoredProcedure { get; }

        void Save();
    }
}
