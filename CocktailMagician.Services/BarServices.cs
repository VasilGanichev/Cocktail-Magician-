﻿using CocktailMagician.Data;
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
            var bar = await this.context.Bars
                .Include(b => b.BarReviews)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .FirstOrDefaultAsync(b => b.Id == id);
            bar.EnsureNotNull();
            return bar;
        }
        public async Task<IReadOnlyCollection<Bar>> GetCollectionAsync()
        {
            return await this.context.Bars.ToListAsync();
        }
        public async Task CreateBarAsync(Bar bar)
        {
            bar.EnsureNotNull();
            await this.context.Bars.AddAsync(bar);
            await this.context.SaveChangesAsync();
        }
        public async Task CreateBarAsync(string name, string adress, string phoneNumber, byte[] picture)
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
        public async Task EditBarAsync(Bar bar)
        {
            var dbBar = await GetBarAsync(bar.Id);
            dbBar.Name = bar.Name;
            dbBar.IsHidden = bar.IsHidden;
            dbBar.PhoneNumber = bar.PhoneNumber;
            dbBar.Address = bar.Address;
            if (bar.Picture != null)
            {
                dbBar.Picture = bar.Picture;
            }

            await this.context.SaveChangesAsync();

        }
        public async Task<IReadOnlyCollection<Bar>> SearchBarsByMultipleCriteriaAsync(string name, string adress, string phonenumber, bool displayOnlyHiddenFiles)
        {
            var barsResult = await this.context.Bars
              .Include(r => r.BarReviews)
              .Include(b => b.BarCocktails)
              .ThenInclude(b => b.Cocktail)
              .Where(b => ((name == null) || (b.Name.Contains(name)))
              && ((adress == null) || (b.Address.Contains(adress)))
              && ((phonenumber == null) || (b.PhoneNumber.Contains(phonenumber)))
              && (b.IsHidden == displayOnlyHiddenFiles)
              )
                .ToListAsync();
            return barsResult;
        }

        public async Task<Bar> GetAsync(string barName)
        {
            barName.EnsureNotNull();
            var bar = await context.Bars.FirstOrDefaultAsync(b => b.Name == barName);
            return bar;
        }
        public async Task<List<string>> LoadMoreCocktails(int alreadyLoaded, int barId)
        {
            var bar = await GetBarAsync(barId);
            var cocktails = bar.BarCocktails.Select(bc => bc.Cocktail.Name).Skip(alreadyLoaded).Take(10).ToList();
            return cocktails;
        }
        public async Task<List<Bar>> LoadNewestBars()
        {
            var bars = await context.Bars.Include(b => b.BarReviews).Where(c => c.IsHidden == false).OrderBy(b => b.CreatedOn).Take(10).ToListAsync();
            return bars;
        }
        public async Task<List<Bar>> GetMultipleBarsByNameAsync(string input)
        {
            var bars = await context.Bars.Where(c => c.Name.Contains(input)).ToListAsync();
            return bars;
        }

        public async Task<bool> BarWithThatNameExists(string name)
        {
            var boolCheck = await context.Bars.AnyAsync(b => b.Name == name);

            return boolCheck;
        }
    }
}
