using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupTaskApi.Data.Entity
{
    public class UserGroup
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
