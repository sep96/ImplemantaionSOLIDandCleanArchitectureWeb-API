using BikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Customers
{
    public interface ICustomersRepository : IServiceRespository<Customer>
    {
        Task<Customer> GetCustomerOrders(int customerId);
    }
}
