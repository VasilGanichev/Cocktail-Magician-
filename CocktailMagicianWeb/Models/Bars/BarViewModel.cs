using CocktailMagician.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models
{
    public class BarViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a name")]
        public string Name{ get; set; }
        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [Required(ErrorMessage = "Please enter a number")]
        public string PhoneNumber { get; set; }
        public IFormFile NewPicture { get; set; }
        public byte[] CurrentPicture { get; set; }
        public bool IsHidden { get; set; }
        public List<BarReview> BarReviews { get; set; } = new List<BarReview>();
        public virtual List<string> Cocktails { get; set; } = new List<string>();
        public double Rating { get; set; }
    }
}
