using System;

namespace GroupTaskApi.Data.Entity
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string OpenId { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
