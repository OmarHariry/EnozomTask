using App.DataTransferObject;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BorrowBookAsync(int copyId, int studentId)
        {
            // find if copy is available
            var copy = await _context.Copies.FindAsync(copyId);
            if (copy == null)
                return false;

            if (copy.StatusId != 1)
                return false;
            // change its status to Borrowed
            copy.StatusId = 2;
            // set borrowing date, expected return date
            BorrowingRecord br = new BorrowingRecord
            {
                CopyId = copyId,
                StudentId = studentId,
                BorrowDate = DateOnly.FromDateTime(DateTime.UtcNow),
                ExpectedReturnDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(3),
                StatusId = 2
            };

            await _context.BorrowingRecords.AddAsync(br);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReturnBookAsync(ReturnBookDto returnBookDto)
        {
            var record = await _context.BorrowingRecords
                .Include(c => c.Copy)
                .FirstOrDefaultAsync(r => r.Id == returnBookDto.RecordId);
            if (record == null)
                return false;

            // change copy status 
            record.StatusId = returnBookDto.StatusId;
            record.Copy.StatusId = returnBookDto.StatusId;

            // record the actual return date
            record.ActualReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
