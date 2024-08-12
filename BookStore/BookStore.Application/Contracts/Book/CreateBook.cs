using BookStore.Application.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Book
{
    public class CreateBook
    {
        [Required(ErrorMessage = "The Title field is required")]
        [MaxLength(128, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The ISBN field is required")]
        [IsbnLength]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "The PublicationYear field is required")]
        [ValidDateOnly]
        public DateOnly PublicationYear { get; set; }
        [Required(ErrorMessage = "The Price field is required")]
        public double Price { get; set; }
        [MaxLength(1024, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string? Description { get; set; }

        public int? PublisherId { get; set; } 
        public int[] AuthorIds { get; set; }
        public int[] CategoryIds { get; set; }
    }
}
