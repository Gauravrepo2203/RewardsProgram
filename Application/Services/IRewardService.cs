using Domain.ResponseModel;

namespace Application.Services
{
    public interface IRewardService
    {
        public Task<Rewards> GetRewardsByCustomerIdAsync(int customerId);
    }
}
