using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupTaskApi.Data.Entity
{
    public class TaskInfo
    {
        public long Id { get; set; }
        public long GroupId { get; set; }
        public long OwnerId { get; set; }
        public string Content { get; set; }
        public uint Award { get; set; }
        public int State { get; set; }
        public long LastModifierId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}
