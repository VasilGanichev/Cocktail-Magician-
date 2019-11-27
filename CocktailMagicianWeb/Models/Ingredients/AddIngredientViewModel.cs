using System.ComponentModel.DataAnnotations;

namespace CocktailMagicianWeb.Models
{
    public class AddIngredientViewModel
    {
        [Required, MinLength(3), MaxLength(20, ErrorMessage = "Name must be between 3 and 20 characters.")]
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
