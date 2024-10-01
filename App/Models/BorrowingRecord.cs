namespace App.Models
{
    public class BorrowingRecord
    {
        public int Id { get; set; }
        public int CopyId {  get; set; }
        public Copy Copy { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateOnly BorrowDate { get; set; }
        public DateOnly ExpectedReturnDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
    }
}
