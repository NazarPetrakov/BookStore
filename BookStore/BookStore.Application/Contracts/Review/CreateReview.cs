using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Review
{
    public class CreateReview
    {
        [Required(ErrorMessage = "The Rating field is required")]
        [Range(1,10)]
        public ushort Rating { get; set; }
        [MaxLength(1024, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string? Comment { get; set; }
    }
}
