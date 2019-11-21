﻿using CocktailMagician.Data;
using CocktailMagician.Data.Entities;
using CocktailMagician.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class IngredientServices : IIngredientServices
    {
        private readonly CocktailDB _context;
        public IngredientServices(CocktailDB context)
        {
            this._context = context;
        }

        public async Task<Ingredient> AddAsync(string name, string type)
        {
            var ingredient = new Ingredient
            {
                Name = name,
                Type = type
            };
            await this._context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
            return ingredient;
        }

        public async Task<Ingredient> GetAsync(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            return ingredient;
        }

        private async Task<Ingredient> GetIngredientByNameAsync(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);

            return ingredient;
        }

        public async Task<List<Ingredient>> GetIngedientsByTypeAsync(string type)
        {
            var ingredients = await _context.Ingredients.Where(i => i.Type == type).ToListAsync();
            return ingredients;
        }

        public async Task<List<Ingredient>> GetMultipleIngredientsByNameAsync(List<string> names)
        {
            var ingredients = new List<Ingredient>(10);
            foreach (var name in names)
            {
                var ingredient = await GetIngredientByNameAsync(name);
                ingredients.Add(ingredient);
            }
            return ingredients;
        }

        public async Task UpdateAsync(int id, string name)
        {
            var ingredient = await GetAsync(id);
            ingredient.Name = name;
            await _context.SaveChangesAsync(); ;
        }

        public async Task DeleteAsync(int id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingredient>> SearchIngredientsAsync(string input)
        {
            var ingredients = await _context.Ingredients.Where(i => i.Name.Contains(input)).ToListAsync();
            return ingredients;
        }

        // TODO: Test "TRUE" scenario when we have cocktails
        public async Task<bool> IsIngredientUsedAsync(int id)
        {
            bool contained = await _context.Ingredients.Include(i => i.CocktailIngredients).AnyAsync(i => i.CocktailIngredients.Any(c => c.IngredientID == id));
            return contained;
        }

    }
}
