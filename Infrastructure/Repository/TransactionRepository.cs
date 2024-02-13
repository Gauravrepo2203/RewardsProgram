using Domain.Entities;
using Domain.IRepository;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Transaction>> FindAllTransactionByCustomerIdDateBetweenAsync(int customerId, DateTime from, DateTime to)
        {
            var transactions = await _context.Transactions
                                              .Where(t => t.CustomerId == customerId && t.TransactionDate >= from && t.TransactionDate <= to)
                                              .ToListAsync();

            return transactions;
        }
    }
}
