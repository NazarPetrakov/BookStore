using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Contracts.Publisher
{
    public class UpdatePublisher
    {
        [MaxLength(64, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string? Name { get; set; }
        [MaxLength(256, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        public string? Address { get; set; }

    }
}
