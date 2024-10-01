using App.DataTransferObject;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("/borrow/{copyId}/{studentId}")]
        public async Task<IActionResult> BorrowBook(int copyId, int studentId)
        {
            // find if copy is available
            var copy = await _context.Copies.FindAsync(copyId);
            if (copy == null)
                return NotFound("Copy Not Found.");

            if (copy.StatusId != 1)
                return BadRequest("Copy Already Borrowed.");
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

            return Ok("Book Borrowed Successfully.");
        }

        // momken a3mel dto feh l recordId, wel copy status ashal request body
        [HttpPut("/return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {
            var record = await _context.BorrowingRecords
                .Include(c => c.Copy)
                .FirstOrDefaultAsync(r => r.Id == returnBookDto.RecordId);
            if (record == null)
                return NotFound("Record Not Found.");

            // change copy status 
            record.StatusId = returnBookDto.StatusId;
            record.Copy.StatusId = returnBookDto.StatusId;

            // record the actual return date
            record.ActualReturnDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _context.SaveChangesAsync();
            return Ok("Book Returned Successfully.");
        }

    }
}
