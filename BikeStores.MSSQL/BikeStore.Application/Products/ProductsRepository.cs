using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Products
{
    public class ProductsRepository: IProductsRepository
    {
        private readonly BikestoresContext _context;

        public ProductsRepository(BikestoresContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product request)
        {
            _context.Products.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<Product> FindAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new Exception("Not Found");
            }

            return product;
        }

        public async Task<List<Product>> GetListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Not Found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Product> UpdateAsync(Product product, int id)
        {
            if (id != product.ProductId)
            {
                throw new Exception("Not Found");
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                {
                    throw new Exception("Not Found");
                }
                else
                {
                    throw;
                }
            }
            return product;
        }
        private async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.ProductId == id);
        }
    }
}
