using App.Models;
using App.Repositories;

namespace App.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<Copy> Copies { get; }
        IGenericRepository<Student> Students { get; }
        IGenericRepository<BorrowingRecord> BorrowingRecords { get; }
        IReportRepository Reports { get; }

        public Task BeginTranscationAsync();
        public Task CommitAsync();
        public Task RollbackAsync();
        public Task<int> CompleteAsync();
    }
}
