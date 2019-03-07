using GroupTaskApi.Data;
using GroupTaskApi.Data.Entity;
using GroupTaskApi.DomainModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util;

namespace GroupTaskApi.Repository
{
    public class UserRepository
    {
        private readonly GroupTaskDbContext _dbContext;

        public UserRepository(GroupTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(UserModel userModel)
        {
            var user = new UserInfo
            {
                OpenId = userModel.OpenId,
                Name = userModel.Name
            };
            await _dbContext.Users.AddAsync(user);
            if (!userModel.JoinedGroups.IsEmpty())
            {
                var userGroups = userModel.JoinedGroups.Select(x =>
                    new UserGroup
                    {
                        UserId = user.Id,
                        GroupId = x.Id
                    });
                await _dbContext.AddRangeAsync(userGroups);
            }

            userModel.Id = user.Id;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserModel> FindByIdAsync(long id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return null;
            var model = new UserModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                OpenId = entity.OpenId,
                CreateTime = entity.CreateTime
            };
            model.JoinedGroups.AddRange(await FindJoinedGroupsAsync(entity.Id));
            return model;
        }

        public async Task<UserModel> FindByOpenIdAsync(string openId)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.OpenId == openId);
            if (entity == null) return null;
            var model = new UserModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                OpenId = entity.OpenId,
                CreateTime = entity.CreateTime
            };
            model.JoinedGroups.AddRange(await FindJoinedGroupsAsync(entity.Id));
            return model;
        }

        private async Task<IEnumerable<UserModel.JoinedGroup>> FindJoinedGroupsAsync(long userId)
        {
            var groupsQuery =
                from ug in _dbContext.UsersGroups
                join g in _dbContext.Groups on ug.GroupId equals g.Id
                join creator in _dbContext.Users on g.CreatorId equals creator.Id
                join owner in _dbContext.Users on g.OwnerId equals owner.Id
                where ug.UserId == userId
                select new UserModel.JoinedGroup()
                {
                    Id = g.Id,
                    CreatorId = creator.Id,
                    CreatorName = creator.Name,
                    Name = g.Name,
                    OwnerId = owner.Id,
                    OwnerName = owner.Name,
                    CreateTime = g.CreateTime,
                    LastModifyTime = g.LastModifyTime
                };
            return await groupsQuery.ToListAsync();
        }
    }
}
