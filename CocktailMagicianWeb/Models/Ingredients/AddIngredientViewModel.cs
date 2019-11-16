using System.ComponentModel.DataAnnotations;

namespace CocktailMagicianWeb.Models
{
    public class AddIngredientViewModel
    {
        [Required, MinLength(3), MaxLength(15, ErrorMessage = "Name must be between 3 and 15 characters.")]
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
