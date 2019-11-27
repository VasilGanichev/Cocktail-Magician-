using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Data.Entities
{
    public class BarReview
    {
        public int Id { get; set; }
        [Range(0, 5)]
        public double Rating { get; set; }
        [MaxLength(300)]
        public string Comment { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
