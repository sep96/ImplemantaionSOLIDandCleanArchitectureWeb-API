using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Brands
{
    public class BrandsRepository: IBrandsRepository
    {
        private readonly BikestoresContext _context;

        public BrandsRepository(BikestoresContext context)
        {
            _context = context;
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand> FindAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                throw new Exception($"Brands with Id = {id} not found");
            }

            return brand;
        }

        public async Task<List<Brand>> GetListAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                throw new Exception($"Brands with Id = {id} not found");
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Brand> UpdateAsync(Brand brand, int id)
        {
            if (id != brand.BrandId)
            {
                throw new Exception("Baad request");
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BrandExists(id))
                {
                    throw new Exception($"Brands with Id = {id} not found");
                }
                else
                {
                    throw new DbUpdateConcurrencyException( "Error updating data");
                }
            }

            return brand;
        }
        private async  Task<bool> BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }
    }
}
