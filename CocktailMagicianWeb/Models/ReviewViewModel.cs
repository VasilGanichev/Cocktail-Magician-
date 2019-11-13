using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        [Required]
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10!")]
        public double Rating { get; set; }
        [MaxLength(300, ErrorMessage = "Comment limit is 300 characters!")]
        public string Comment { get; set; }
    }
}
