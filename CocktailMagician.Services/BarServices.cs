using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CocktailMagician.Services.Utilities;
using System.Linq;

namespace CocktailMagician.Services
{
    public class BarServices : IBarServices
    {
        private readonly CocktailDB context;

        public BarServices(CocktailDB context)
        {
            this.context = context;
        }
        public async Task<Bar> GetBarAsync(int id)
        {
            var bar = await this.context.Bars.FirstOrDefaultAsync(b => b.Id == id);
            bar.EnsureNotNull();
            return bar;
        }
        public async Task<IReadOnlyCollection<Bar>> GetCollectionAsync()
        {
            return await this.context.Bars.ToListAsync();
        }
        public async Task<IReadOnlyCollection<Bar>> GetVisibleCollectionAsync()
        {
            return await this.context.Bars.Where(b => b.IsHidden == false).ToListAsync();
        }
        public async Task<IReadOnlyCollection<Bar>> GetHiddenCollectionAsync()
        {
            return await this.context.Bars.Where(b => b.IsHidden == true).ToListAsync();
        }
        public async Task CreateBarAsync(Bar bar)
        {
            bar.EnsureNotNull();
            await this.context.Bars.AddAsync(bar);
            await this.context.SaveChangesAsync();
        }
        public async Task CreateBarAsync(string name, string adress,string phoneNumber, byte[] picture)
        {
            var bar = new Bar
            {
                Name = name,
                Address = adress,
                PhoneNumber = phoneNumber,
                Picture = picture,
                IsHidden = false,
            };
            bar.EnsureNotNull();
            await this.context.Bars.AddAsync(bar);
            await this.context.SaveChangesAsync();
        }
        public async Task EditBarAsync(Bar bar, string newName, string newAdress, string newPhoneNumber, byte[] newPicture, bool newIsHidden)
        {
            bar.EnsureNotNull();
            if (bar.Name != newName || bar.Address != newAdress || bar.PhoneNumber != newPhoneNumber || bar.Picture != newPicture || bar.IsHidden != newIsHidden)
            {
                if (newName != null )
                    bar.Name = newName;

                if (newAdress != null )
                    bar.Address = newAdress;

                if (newPhoneNumber != null )
                    bar.PhoneNumber = newPhoneNumber;

                if (newPicture != null)
                    bar.Picture = newPicture;

                    bar.IsHidden = newIsHidden;
                await this.context.SaveChangesAsync();
            }


        }
        public async Task<IReadOnlyCollection<Bar>> SearchBooksByMultipleCriteriaAsync(string name, string adress, string phonenumber)
        {
            var barsResult = await this.context.Bars.Include(r => r.BarReviews).Where(b => ((name == null) || (b.Name.Contains(name)))
              && ((adress == null) || (b.Address.Contains(adress)))
              && ((phonenumber == null) || (b.PhoneNumber.Contains(phonenumber))))
                .ToListAsync();
            return barsResult;
        }
    }
}
