using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Application.Products;
using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;


namespace BikeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }



        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.Models.Product>>> GetProducts()
            => Ok(_productsRepository.GetListAsync());

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Data.Models.Product>> GetProduct(int id)
             => Ok(_productsRepository.FindAsync(id));

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Data.Models.Product product)
             => Ok(_productsRepository.UpdateAsync(product ,id));

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Data.Models.Product>> PostProduct(Data.Models.Product product)
             => Ok(_productsRepository.AddAsync(product));

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
             => Ok(_productsRepository.RemoveAsync(id));
    }
}
