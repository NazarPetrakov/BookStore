using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Category
{
    public class CreateCategory
    {
        [Required(ErrorMessage = "The Name field is required")]
        [MaxLength(64, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string Name { get; set; }
    }
}
