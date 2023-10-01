using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Orders
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly BikestoresContext _context;

        public OrdersRepository(BikestoresContext context)
        {
            _context = context;
        }

        public Task<Order> AddAsync(Order request)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> FindAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new Exception("NotFound");
            }

            return order;
        }

        public async Task<List<Order>> GetListAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateAsync(Order order, int id)
        {
            if (id != order.OrderId)
            {
                throw new Exception("badRequest");
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderExists(id))
                {
                    throw new Exception("NotFound");
                }
                else
                {
                    throw;
                }
            }
            return order;
        }
        private async Task< bool> OrderExists(int id)
        {
            return await _context.Orders.AnyAsync(e => e.OrderId == id);
        }
    }
}
