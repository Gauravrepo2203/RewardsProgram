using Domain;
using Domain.Entities;
using Domain.IRepository;
using Domain.ResponseModel;

namespace Application.Services
{
    public class RewardService : IRewardService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITransactionRepository _transactionRepository;

        public RewardService(ICustomerRepository customerRepository, ITransactionRepository transactionRepository)
        {
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task<Rewards> GetRewardsByCustomerIdAsync(int customerId)
        {
            var customer = await _customerRepository.FindByCustomerIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Invalid customer Id");
            }
            var lastMonthTimestamp = GetDateBasedOnOffSetDays(Constants.RewardLimits.DaysInMonths);
            var lastSecondMonthTimestamp = GetDateBasedOnOffSetDays(2 * Constants.RewardLimits.DaysInMonths);
            var lastThirdMonthTimestamp = GetDateBasedOnOffSetDays(3 * Constants.RewardLimits.DaysInMonths);

           var lastMonthTransactions = await _transactionRepository.FindAllTransactionByCustomerIdDateBetweenAsync(customerId, lastMonthTimestamp, DateTime.Today);
            var lastSecondMonthTransactions = await _transactionRepository.FindAllTransactionByCustomerIdDateBetweenAsync(customerId, lastSecondMonthTimestamp, lastMonthTimestamp);
            var lastThirdMonthTransactions = await _transactionRepository.FindAllTransactionByCustomerIdDateBetweenAsync(customerId, lastThirdMonthTimestamp, lastSecondMonthTimestamp);

            var lastMonthRewardPoints = GetRewardsPerMonth(lastMonthTransactions);
            var lastSecondMonthRewardPoints = GetRewardsPerMonth(lastSecondMonthTransactions);
            var lastThirdMonthRewardPoints = GetRewardsPerMonth(lastThirdMonthTransactions);

            Rewards customerRewards = new Rewards()
            {
                CustomerId = customerId,
                LastMonthRewardPoints = lastMonthRewardPoints,
                LastSecondMonthRewardPoints = lastSecondMonthRewardPoints,
                LastThirdMonthRewardPoints= lastThirdMonthRewardPoints,
                TotalRewards = lastMonthRewardPoints + lastSecondMonthRewardPoints + lastThirdMonthRewardPoints
            };

            return customerRewards;

        }

        private long GetRewardsPerMonth(List<Transaction> transactions)
        {
            return transactions.Select(transaction => CalculateRewards(transaction))
                               .Sum();
        }

        private long CalculateRewards(Transaction t)
        {
            if (t.Amount > Constants.RewardLimits.FirstRewardLimit && t.Amount <= Constants.RewardLimits.SecondRewardLimit)
            {
                return t.Amount - Constants.RewardLimits.FirstRewardLimit;
            }
            else if (t.Amount > Constants.RewardLimits.SecondRewardLimit)
            {
                return (t.Amount - Constants.RewardLimits.SecondRewardLimit) * 2
                       + (Constants.RewardLimits.SecondRewardLimit - Constants.RewardLimits.FirstRewardLimit);
            }
            else
            {
                return 0;
            }

        }

        public DateTime GetDateBasedOnOffSetDays(int days)
        {
            return DateTime.Now.AddDays(-days);
        }

    }
}
