﻿using App.DataTransferObject;
using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
    [ApiController]
    [Route("/api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }
        [HttpPost("/borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowBookDto borrowBookDto)
        {
            bool isBorrowed = await _service.BorrowBookAsync(borrowBookDto);
            if (isBorrowed)
                return Ok("Book Borrowed Successfully.");
            return BadRequest("Book Borrow Failed.");
        }

        [HttpPut("/return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDto returnBookDto)
        {
            bool isReturned = await _service.ReturnBookAsync(returnBookDto);
            if (isReturned)
                return Ok("Book Returned Successfully.");
            return BadRequest("Book Return Failed.");
        }

    }
}
