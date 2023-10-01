using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.OrderItems
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly BikestoresContext _context;

        public OrderItemsRepository(BikestoresContext context)
        {
            _context = context;
        }
        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (await OrderItemExists(orderItem.OrderId))
                {
                    throw new Exception("Conflict");
                }
                else
                {
                    throw;
                }
            }

            return orderItem;
        }

        public async Task<OrderItem> FindAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                throw new Exception("Not Found");
            }

            return orderItem;
        }

        public async Task<List<OrderItem>> GetListAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<int> RemoveAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new Exception("Not Found");
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<OrderItem> UpdateAsync(OrderItem orderItem, int id)
        {
            if (id != orderItem.OrderId)
            {
                throw new Exception("Not Found");
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await OrderItemExists(id))
                {
                    throw new Exception("Not Found");
                }
                else
                {
                    throw;
                }
            }
            return orderItem;
        }
        private async Task<bool> OrderItemExists(int id)
        {
            return await _context.OrderItems.AnyAsync(e => e.OrderId == id);
        }
    }
}
