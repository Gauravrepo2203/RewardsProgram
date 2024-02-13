using Domain.Entities;

namespace Domain.IRepository
{
    public interface ITransactionRepository
    {
        public Task<List<Transaction>> FindAllTransactionByCustomerIdDateBetweenAsync(int customerId, DateTime from, DateTime to);
    }
}
