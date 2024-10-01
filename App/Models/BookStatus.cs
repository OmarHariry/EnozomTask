namespace App.Models
{
    public class BookStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public IEnumerable<BorrowingRecord> BorrowingRecords { get; set; }
        public IEnumerable<Copy> Copies { get; set; }
    }
}
