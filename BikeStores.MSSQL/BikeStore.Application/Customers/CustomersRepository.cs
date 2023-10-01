using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Customers
{
    public class CustomersRepository: ICustomersRepository
    {
        private readonly BikestoresContext _context;

        public CustomersRepository(BikestoresContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> FindAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                throw new Exception("NotFound");
            }

            return customer;
        }

        public async Task<Customer> GetCustomerOrders(int customerId)
        {
            var customer = await _context.Customers
                                .Where(c => c.CustomerId == customerId)
                                .Include(c => c.Orders)
                                .FirstOrDefaultAsync();
            return customer;
        }

        public async Task<List<Customer>> GetListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("NotFound");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Customer> UpdateAsync(Customer customer, int id)
        {
            if (id != customer.CustomerId)
            {
                throw new Exception("bad request");
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CustomerExists(id))
                {
                    throw new Exception("NotFound");
                }
                else
                {
                    throw;
                }
            }
            return customer;
        }
        private async Task<bool> CustomerExists(int id)
        {
            return await _context.Customers.AnyAsync(e => e.CustomerId == id);
        }
    }
}
