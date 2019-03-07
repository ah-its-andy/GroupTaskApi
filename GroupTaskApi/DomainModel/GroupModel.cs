using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GroupTaskApi.DomainModel
{
    public class GroupModel : IAggregate
    {
        public GroupModel()
        {
            Tasks = new List<TaskInGroup>();
        }
        public long Id { get; set; }
        public long CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Code { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifyTime { get; set; }

        public IList<TaskInGroup> Tasks { get; set; }
        public class TaskInGroup
        {
            public long Id { get; set; }
            public long GroupId { get; set; }
            public long OwnerId { get; set; }
            public string OwnerName { get; set; }
            public string Content { get; set; }
            public uint Award { get; set; }
            public TaskState State { get; set; }
            public long LastModifierId { get; set; }
            public string LastModifierName { get; set; }
            public DateTime CreateTime { get; set; }
            public DateTime LastModifiedTime { get; set; }
        }

        public enum TaskState : int
        {
            Created = 100,
            Completed = 110,
            Confirmed = 120
        }

        
    }
}
