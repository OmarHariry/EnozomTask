using App.Models;

namespace App.DataTransferObject
{
    public class BorrowBookDto
    {
        public int CopyId { get; set; }
        public int StudentId { get; set; }
        public DateOnly BorrowDate { get; set; }
        public DateOnly ExpectedReturnDate { get; set; }
    }
}
