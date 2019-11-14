using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CocktailMagicianWeb.Utilities.Middlewares
{

    public static class SeedDatabase
    {
        public static async Task Plant(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var dbContext = scope.ServiceProvider.GetService<CocktailDB>();

                if (!await roleManager.RoleExistsAsync("CocktailMagician"))
                {
                    var cocktailMagicianRole = new IdentityRole("CocktailMagician");
                    await roleManager.CreateAsync(cocktailMagicianRole);
                    var barCrawlerRole = new IdentityRole("BarCrawler");
                    await roleManager.CreateAsync(barCrawlerRole);
                    var vaseto = new User
                    {
                        Email = "Vasil_0016@abv.bg",
                        UserName = "VaskoBatal"
                    };
                    await userManager.CreateAsync(vaseto, "konche123");
                    await userManager.AddToRoleAsync(vaseto, "BarCrawler");
                    await userManager.AddToRoleAsync(vaseto, "CocktailMagician");

                    var vladi = new User
                    {
                        Email = "vladivital@abv.bg",
                        UserName = "VladiVital"
                    };
                    await userManager.CreateAsync(vladi, "vladivital");
                    await userManager.AddToRoleAsync(vladi, "BarCrawler");
                    await userManager.AddToRoleAsync(vladi, "CocktailMagician");
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
