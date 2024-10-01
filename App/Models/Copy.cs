namespace App.Models
{
    public class Copy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } // navigation
        public IEnumerable<BorrowingRecord> BorrowingRecords { get; set; }
        public int StatusId { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}
