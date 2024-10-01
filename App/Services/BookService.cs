using App.DataTransferObject;
using App.Models;
using App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class BookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> BorrowBookAsync(int copyId, int studentId)
        {
            return await _repository.BorrowBookAsync(copyId, studentId);
        }

        public async Task<bool> ReturnBookAsync(ReturnBookDto returnBookDto)
        {
            return await _repository.ReturnBookAsync(returnBookDto);
        }
    }
}
