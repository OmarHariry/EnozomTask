namespace App.Models
{
    public class Copy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; } // navigation
        public string Status { get; set; }
        public IEnumerable<BorrowingRecord> BorrowingRecords { get; set; }
    }
}
