using System;

namespace GroupTaskApi.Data.Entity
{
    public class GroupInfo
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public string Code { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifyTime { get; set; }
        
    }
}
