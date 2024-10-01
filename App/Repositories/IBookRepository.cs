using App.DataTransferObject;

namespace App.Repositories
{
    public interface IBookRepository
    {
        public Task<bool> BorrowBookAsync(int copyId, int studentId);
        public Task<bool> ReturnBookAsync(ReturnBookDto returnBookDto);
    }
}
