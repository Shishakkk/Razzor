namespace WebApplication1.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public bool IsActive => ReturnedAt == null;
    }
}
