using System;
using System.Collections.Generic;
using GroupTaskApi.Data.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GroupTaskApi.DomainModel
{
    public class UserModel : IAggregate
    {
        public UserModel()
        {
            JoinedGroups = new List<JoinedGroup>();
        }
        public long Id { get; set; }
        public string OpenId { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public List<JoinedGroup> JoinedGroups { get; }

        public class JoinedGroup
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
}
