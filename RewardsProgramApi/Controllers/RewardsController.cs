using Application.Services;
using Domain.IRepository;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace RewardsProgramApi.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class RewardsController : ControllerBase
    {
        private readonly IRewardService _service;

        public RewardsController(IRewardService service)
        {
            _service = service;
        }

        [HttpGet("{customerId}/rewards")]
        public async Task<IActionResult> GetRewardsByCustomerId(int customerId)
        {
            var rewardPoints = await _service.GetRewardsByCustomerIdAsync(customerId);
            return this.Ok(rewardPoints);
        }
    }
}
