namespace BookStore.Domain.Models.Category
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<JoinEntities.BookCategory> BookCategories { get; set; } = new List<JoinEntities.BookCategory>();

    }
}
