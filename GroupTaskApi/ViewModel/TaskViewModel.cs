using GroupTaskApi.DomainModel;
using System;

namespace GroupTaskApi.ViewModel
{
    public class TaskViewModel
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string Content { get; set; }
        public double Award { get; set; }
        public GroupModel.TaskState State { get; set; }
        public long LastModifierId { get; set; }
        public string LastModifierName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
    }
}
