using System;

namespace DBContextEF.Models
{
    public partial class WxUserMessageTb
    {
        public int Id { get; set; }
        public string WxOpenId { get; set; }
        public string WxToken { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string Message { get; set; }
        public DateTime ExpireTime { get; set; }
        public DateTime? SendTime { get; set; }
    }
}
