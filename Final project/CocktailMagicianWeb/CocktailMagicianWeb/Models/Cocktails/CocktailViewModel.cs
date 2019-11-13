using System.Collections.Generic;

namespace CocktailMagicianWeb.Models.Cocktails
{
    public class CocktailViewModel
    {
        public int ID { get; set; }
        public byte[] Picture { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>(10);
        public List<string> Bars { get; set; } = new List<string>(20);
        public List<int> Quantities { get; set; } = new List<int>(10);
    }
}
