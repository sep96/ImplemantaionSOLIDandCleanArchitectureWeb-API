using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application
{
    public interface IServiceRespository<T> 
    {
        Task<List<T>> GetListAsync();
        Task<T> FindAsync(int id);
        Task<T> AddAsync(T request);
        Task<T> UpdateAsync(T request , int id);
        Task<int> RemoveAsync(int id);
            
    }
}
