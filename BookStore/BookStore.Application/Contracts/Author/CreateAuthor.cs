using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Author
{
    public class CreateAuthor
    {
        [Required(ErrorMessage = "The FirstName field is required")]
        [MaxLength(64, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The LastName field is required")]
        [MaxLength(64, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string LastName { get; set; }
        [MaxLength(512, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string? Bio { get; set; }
    }
}
