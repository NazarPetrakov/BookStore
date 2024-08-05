using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Author
{
    public class UpdateAuthor
    {
        [MaxLength(64, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string? FirstName { get; set; }
        [MaxLength(64, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string? LastName { get; set; }
        [MaxLength(512, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string? Bio { get; set; }
    }
}
