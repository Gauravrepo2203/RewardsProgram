using Domain.Entities;

namespace Domain.IRepository
{
    public interface ICustomerRepository
    {
        public Task<Customer> FindByCustomerIdAsync(int customerId);
    }
}
