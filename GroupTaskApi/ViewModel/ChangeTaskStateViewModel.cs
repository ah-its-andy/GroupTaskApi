using GroupTaskApi.DomainModel;

namespace GroupTaskApi.ViewModel
{
    public class ChangeTaskStateViewModel
    {
        public long GroupId { get; set; }
        public long TaskId { get; set; }
        public GroupModel.TaskState State { get; set; }
    }
}
