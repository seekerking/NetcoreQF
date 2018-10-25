using System;

namespace DBContextEF.Models
{
    public partial class WxUserMessageSendLogTb
    {
        public int Id { get; set; }
        public string WxOpenId { get; set; }
        public int Status { get; set; }
        public DateTime SendDate { get; set; }
    }
}
