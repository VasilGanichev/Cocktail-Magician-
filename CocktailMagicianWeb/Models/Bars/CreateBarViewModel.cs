using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Models.Bars
{
    public class CreateBarViewModel
    {
        public BarViewModel BarResult { get; set; }
        public IFormFile PictureBar { get; set; }
    }
}
