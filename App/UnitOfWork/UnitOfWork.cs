using App.Models;
using App.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace App.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Book> Books { get; private set; }

        public IGenericRepository<Copy> Copies { get; private set; }

        public IGenericRepository<Student> Students { get; private set; }

        public IGenericRepository<BorrowingRecord> BorrowingRecords { get; private set; }
        public IReportRepository Reports { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(context);
            Copies = new GenericRepository<Copy>(context);
            Students = new GenericRepository<Student>(context);
            BorrowingRecords = new GenericRepository<BorrowingRecord>(context);
            Reports = new ReportRepository(context);
        }
        public async Task BeginTranscationAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}
