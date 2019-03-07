using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupTaskApi.DomainModel;
using GroupTaskApi.Repository;
using GroupTaskApi.ViewModel;

namespace GroupTaskApi.Service
{
    public class TaskService
    {
        private readonly GroupRepository _groupRepository;

        public TaskService(GroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }


        public async Task CreateTaskAsync(CreateTaskViewModel viewModel)
        {
            var model = new GroupModel.TaskInGroup
            {
                Award = (uint)Math.Round(viewModel.Award * 1000),
                Content = viewModel.Content,
                OwnerId = viewModel.OwnerId
            };
            await _groupRepository.CreateTaskAsync(viewModel.GroupId, model);
        }

        public async Task ChangeStateAsync(ChangeTaskStateViewModel viewModel)
        {
            await _groupRepository.ChangeTaskStateAsync(viewModel.GroupId, viewModel.TaskId, (int) viewModel.State);
        }

        public async Task<List<TaskViewModel>> GetTaskList(long groupId)
        {
            var model = await _groupRepository.FindByIdAsync(groupId);
            return model.Tasks.Select(x => new TaskViewModel
            {
                Id = x.Id,
                OwnerId = x.OwnerId,
                OwnerName = x.OwnerName,
                Award = Math.Round(x.Award / 1000.0, 2),
                Content = x.Content,
                CreateTime = x.CreateTime,
                LastModifiedTime = x.LastModifiedTime,
                LastModifierId = x.LastModifierId,
                LastModifierName = x.LastModifierName,
                State = x.State
            }).ToList();
        }
    }
}
