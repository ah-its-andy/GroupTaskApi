using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupTaskApi.DomainModel;
using GroupTaskApi.Repository;

namespace GroupTaskApi.Service
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserModel.JoinedGroup>> GetJoinedGroups(long userId)
        {
            var user = await _userRepository.FindByIdAsync(userId);
            return user.JoinedGroups;
        }

    }
}
