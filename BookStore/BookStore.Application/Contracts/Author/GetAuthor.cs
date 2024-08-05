namespace BookStore.Application.Contracts.Author
{
    public class GetAuthor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
    }
}
