using System;

namespace GroupTaskApi.ViewModel
{
    public class GroupViewModel
    {
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifyTime { get; set; }
    }
}
