namespace App.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Copy> Copies { get; set; }
    }
}
