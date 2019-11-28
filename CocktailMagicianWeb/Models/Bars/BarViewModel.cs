using CocktailMagician.Data.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
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
        public string GeoCoderApiTemplate { get; set; }
        public string GeoCoderApiQuerySeparator { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string ImageAddress { get; set; }

        public BarViewModel()
        {
            GeoCoderApiTemplate = "https://nominatim.openstreetmap.org/search?q={0}&accept-language=en&format={1}&addressdetails=1&limit={2}&polygon_svg=1&email=lssah@protonmail.com";
            GeoCoderApiQuerySeparator = "+";
        }
        public BarViewModel(Bar bar)
        {
            GeoCoderApiTemplate = "https://nominatim.openstreetmap.org/search?q={0}&accept-language=en&format={1}&addressdetails=1&limit={2}&polygon_svg=1&email=lssah@protonmail.com";
            GeoCoderApiQuerySeparator = "+";


            Id = bar.Id;
            IsHidden = bar.IsHidden;
            Name = bar.Name;
            CurrentPicture = bar.Picture == null ? new byte[0] : bar.Picture;
            Address = bar.Address;
            PhoneNumber = bar.PhoneNumber;
            Lat = bar.Lat;
            Long = bar.Long;
            Cocktails = bar.BarCocktails == null ? new List<string>() : bar.BarCocktails.Select(x => x.Cocktail.Name).ToList();
            try
            {
                Rating = Math.Round(bar.BarReviews.Select(b => b.Rating).Average(), 2);
            }
            catch (Exception)
            {
                Rating = 0;
            }
            ImageAddress = ParseStaticGoogleMapQuery();
        }

        public string FormQuery(string[] queryParams)
        {
            return string.Join(GeoCoderApiQuerySeparator, queryParams);
        }

        public string FormatApiTemplate(string[] queryParams)
        {
            var parsedQuery = FormQuery(queryParams);
            return string.Format(GeoCoderApiTemplate, parsedQuery, "json", 1);
        }

        public void ParseApiResult(string apiResultAsString)
        {
            if (string.IsNullOrEmpty(apiResultAsString) || apiResultAsString == "[]")
            {
                this.Lat = 0;
                this.Long = 0;
                return;
            }

            var apiStringToJArr = JsonConvert.DeserializeObject<JArray>(apiResultAsString);
            var firstResultJObj = apiStringToJArr.First().ToObject<JObject>();

            foreach (JProperty item in firstResultJObj.Children())
            {
                if (item.Name == "lat")
                {
                    this.Lat = Convert.ToDecimal(item.Value);
                }
                else if (item.Name == "lon")
                {
                    this.Long = Convert.ToDecimal(item.Value);
                }
            }
        }

        public string ParseStaticGoogleMapQuery()
        {
            return string.Format(@"https://maps.googleapis.com/maps/api/staticmap?center={0},{1}
                   &zoom=13
                   &size=600x300
                   &maptype=roadmap
                   &markers=color:red%7Clabel:S%7C{0},{1}
                   &key=AIzaSyD4TQy6H8TRVuUU7jQvC-mXOw7VPdD_iyk", this.Lat, this.Long);
        }
    }

}