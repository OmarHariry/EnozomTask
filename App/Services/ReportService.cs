using App.DataTransferObject;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class ReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReportDto>> GetReport()
        {
            var report = await _context.Copies.Include(c => c.Book).Include(c => c.BookStatus)
                .Select(c => new ReportDto
                {
                    BookTitle = c.Book.Title,
                    CopyId = c.Id,
                    Status = c.BookStatus.Status
                }).ToListAsync();

            return report;
        }
    }
}
