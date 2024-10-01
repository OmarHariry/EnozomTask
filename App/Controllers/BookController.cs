using App.DataTransferObject;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }
        [HttpPost("/borrow/{copyId}/{studentId}")]
        public async Task<IActionResult> BorrowBook(int copyId, int studentId)
        {
            bool isBorrowed = await _service.BorrowBook(copyId, studentId);
            if (isBorrowed)
                return Ok("Book Borrowed Successfully.");
            return BadRequest("Book Borrow Failed.");
        }

        [HttpPut("/return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {
            bool isReturned = await _service.ReturnBook(returnBookDto);
            if (isReturned)
                return Ok("Book Returned Successfully.");
            return BadRequest("Book Return Failed.");
        }

    }
}
