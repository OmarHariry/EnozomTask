namespace App.DataTransferObject
{
    public class ReturnBookDto
    {
        public int RecordId { get; set; }
        public int StatusId { get; set; }
        public DateOnly ActualReturnDate { get; set; }
    }
}
