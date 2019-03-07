using GroupTaskApi.DomainModel;
using GroupTaskApi.Repository;
using GroupTaskApi.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupTaskApi.Service
{
    public class GroupService
    {
        private readonly UserRepository _userRepository;
        private readonly GroupRepository _groupRepository;

        public GroupService(UserRepository userRepository, GroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task CreateGroupAsync(CreateGroupViewModel viewModel)
        {
            var model = new GroupModel
            {
                Name = viewModel.GroupName,
                CreatorId = viewModel.OwnerId,
                OwnerId = viewModel.OwnerId
            };
            await _groupRepository.CreateAsync(model);
        }

        public async Task<List<GroupViewModel>> GetGroupListAsync(long userId)
        {
            var model = await _userRepository.FindByIdAsync(userId);
            return model.JoinedGroups.Select(x => new GroupViewModel
            {
                Id = x.Id,
                Name = x.Name,
                OwnerId = x.OwnerId,
                OwnerName = x.OwnerName,
                CreatorId = x.CreatorId,
                CreateTime = x.CreateTime,
                CreatorName = x.CreatorName,
                LastModifyTime = x.LastModifyTime
            }).ToList();
        }
        
        public async Task<GroupModel> FindByCodeAsync(string code)
        {
            return await _groupRepository.FindByCodeAsync(code.ToUpper());
        }

        public async Task JoinGroupAsync(JoinGroupViewModel viewModel)
        {
            await _groupRepository.JoinGroupAsync(viewModel.UserId, viewModel.GroupId);
        }
    }
}
