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
            return await _context.Copies.Include(c => c.Book).Include(c => c.BookStatus)
                .Select(c => new ReportDto
                {
                    BookTitle = c.Book.Title,
                    CopyId = c.Id,
                    Status = c.BookStatus.Status
                }).ToListAsync();
        }
    }
}
