using Domain.Entities;
using Domain.IRepository;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> FindByCustomerIdAsync(int customerId)
        {
            var res = await _context.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
            return res;
        }
    }
}
