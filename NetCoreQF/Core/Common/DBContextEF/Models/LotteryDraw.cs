namespace DBContextEF.Models
{
    public partial class LotteryDraw
    {
        public int Id { get; set; }
        public string ContactInfo { get; set; }
        public string Token { get; set; }
        public int RewardType { get; set; }
        public long? CreateTime { get; set; }
        public long? ExpireTime { get; set; }
        public byte[] TimeStamp { get; set; }
        public int Status { get; set; }
        public string Ip { get; set; }
    }
}
