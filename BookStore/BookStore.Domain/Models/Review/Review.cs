namespace BookStore.Domain.Models.Review
{
    public class Review : BaseEntity
    {
        public ushort Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
