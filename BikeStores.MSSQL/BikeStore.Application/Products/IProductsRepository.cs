using BikeStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Products
{
    public interface IProductsRepository : IServiceRespository<Product>
    {
    }
}
