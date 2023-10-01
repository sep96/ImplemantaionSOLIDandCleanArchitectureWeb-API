using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly BikestoresContext _context;

        public CategoriesRepository(BikestoresContext context)
        {
            _context = context;
        }

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> FindAsync(int id)
        {

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("NotFound");
            }

            return category;
        }

        public async Task<List<Category>> GetListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new Exception("NotFound");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Category> UpdateAsync(Category category, int id)
        {
            if (id != category.CategoryId)
            {
               throw new Exception("Bad Request");
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
                {
                    throw new Exception("NotFound");
                }
                else
                {
                    throw;
                }
            }
            return category;
        }
        private async Task<bool> CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
