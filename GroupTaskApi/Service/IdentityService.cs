using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupTaskApi.DomainModel;
using GroupTaskApi.Repository;

namespace GroupTaskApi.Service
{
    public class IdentityService
    {
        private readonly UserRepository _userRepository;

        public IdentityService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> SignInAsync(string openId, string username)
        {
            var user = await _userRepository.FindByOpenIdAsync(openId);
            if (user == null)
            {
                user = new UserModel()
                {
                    OpenId = openId,
                    Name = username
                };
                await _userRepository.CreateAsync(user);
                return await SignInAsync(openId, username);
            }
            var result = new IdentityResult(true);
            result.Claims.Add("sub", user.Id);
            result.Claims.Add("name", user.Name);
            return result;
        }

        public class IdentityResult
        {
            public IdentityResult(bool succeeded)
            {
                Succeeded = succeeded;
                Errors = new List<string>();
                Claims = new Dictionary<string, object>();
            }
            public bool Succeeded { get; }
            public IList<string> Errors { get; }
            public IDictionary<string, object> Claims { get; }

        }
    }
}
