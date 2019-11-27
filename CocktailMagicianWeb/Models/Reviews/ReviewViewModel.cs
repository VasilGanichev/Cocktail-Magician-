using System.ComponentModel.DataAnnotations;

namespace CocktailMagicianWeb.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5!")]
        public double Rating { get; set; }
        [MaxLength(300, ErrorMessage = "Comment limit is 300 characters!")]
        public string Comment { get; set; }
    }
}
