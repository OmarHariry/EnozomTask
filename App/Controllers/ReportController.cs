using App.DataTransferObject;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{

    [ApiController]
    [Route("report")]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetBookWithCopies()
        {
            var report = await _context.Copies.Include(c => c.Book).Include(c => c.BookStatus)
                .Select(c => new ReportDto
            {
                BookTitle = c.Book.Title,
                CopyId = c.Id,
                Status = c.BookStatus.Status
            }).ToListAsync();

            return Ok(report);
        }
    }
}
