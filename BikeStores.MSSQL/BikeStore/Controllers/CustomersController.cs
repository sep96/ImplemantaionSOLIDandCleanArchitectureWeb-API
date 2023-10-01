using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeStore.Application.Customers;
using BikeStore.Data;
using BikeStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customers;

        public CustomersController(ICustomersRepository customers)
        {
            _customers = customers;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
            => Ok(await _customers.GetListAsync());

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
              => Ok(await _customers.FindAsync(id));

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
               => Ok(await _customers.UpdateAsync(customer , id));

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
         => Ok(await _customers.AddAsync(customer));
        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
         => Ok(await _customers.RemoveAsync(id));

        //Loading related data
        [HttpGet("GetCustomerOrders")]
        public async Task<ActionResult<Customer>> GetCustomerOrders(int customerId)
            => Ok(await _customers.GetCustomerOrders(customerId));
    }
}
