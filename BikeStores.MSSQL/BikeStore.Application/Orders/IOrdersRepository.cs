using BikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Orders
{
    public interface IOrdersRepository : IServiceRespository<Order>
    {
    }
}
