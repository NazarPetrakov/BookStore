using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Publisher
{
    public class GetPublisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
