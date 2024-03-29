﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CocktailMagician.Data.Entities
{
    public class Bar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [RegularExpression("([0-9]+)")]
        public string PhoneNumber { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public byte[] Picture { get; set; }
        public bool IsHidden { get; set; }
        public int CocktailID { get; set; }
        public List<BarCocktail> BarCocktails {get; set;}
        public int BarReviewID { get; set; }
        public List<BarReview> BarReviews { get; set; }
        public DateTime CreatedOn { get; set; }


    }
}
