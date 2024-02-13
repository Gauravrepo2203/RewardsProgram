namespace Domain.ResponseModel
{
    public class Rewards
    {
        public int CustomerId { get; set; }
        public long LastMonthRewardPoints { get; set; }
        public long LastSecondMonthRewardPoints { get; set; }
        public long LastThirdMonthRewardPoints { get; set; }
        public long TotalRewards { get; set; }
    }
}
