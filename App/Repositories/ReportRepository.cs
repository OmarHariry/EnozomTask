using App.DataTransferObject;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ReportDto>> GetReportAsync()
        {
            var records = _context.BorrowingRecords;
            if (records.Count() == 0)
                return  await _context.Copies.Include(c => c.Book).Include(c => c.BookStatus)
                .Select(c => new ReportDto
                {
                    BookTitle = c.Book.Title,
                    CopyId = c.Id,
                    Status = c.BookStatus.Status
                }).ToListAsync();

            return await _context.BorrowingRecords.Include(br => br.Copy).ThenInclude(c => c.Book)
                .Include(br => br.BookStatus)
                .Select(br => new ReportDto
                {
                    BookTitle = br.Copy.Book.Title,
                    CopyId = br.Copy.Id,
                    Status = br.Copy.BookStatus.Status,
                    RecordId = br.Id
                }).ToListAsync();
        }
    }
}
