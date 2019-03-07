using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupTaskApi.Data;
using GroupTaskApi.Data.Entity;
using GroupTaskApi.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace GroupTaskApi.Repository
{
    public class GroupRepository
    {
        private readonly GroupTaskDbContext _dbContext;

        public GroupRepository(GroupTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<GroupModel> GroupModels =>
            from g in _dbContext.Groups
            join owner in _dbContext.Users on g.OwnerId equals owner.Id
            join creator in _dbContext.Users on g.CreatorId equals creator.Id
            select new GroupModel
            {
                Id = g.Id,
                CreatorId = g.CreatorId,
                CreateTime = g.CreateTime,
                CreatorName = creator.Name,
                LastModifyTime = g.LastModifyTime,
                Name = g.Name,
                OwnerId = g.OwnerId,
                OwnerName = owner.Name,
                Code = g.Code
            };

        public IQueryable<GroupModel.TaskInGroup> TaskModels => 
            from t in _dbContext.Tasks
            join owner in _dbContext.Users on t.OwnerId equals owner.Id
            join lastModifier in _dbContext.Users on t.LastModifierId equals lastModifier.Id
            select new GroupModel.TaskInGroup
            {
                Id = t.Id,
                GroupId = t.GroupId,
                Award = t.Award,
                Content = t.Content,
                CreateTime = t.CreateTime,
                LastModifiedTime = t.LastModifiedTime,
                LastModifierId = lastModifier.Id,
                LastModifierName = lastModifier.Name,
                OwnerId = owner.Id,
                OwnerName = owner.Name,
                State = (GroupModel.TaskState) t.State
            };


    public async Task CreateAsync(GroupModel groupModel)
        {
            var entity = new GroupInfo
            {
                Name = groupModel.Name,
                CreatorId = groupModel.CreatorId,
                OwnerId = groupModel.OwnerId,
                LastModifyTime = DateTime.Now
            };
            await _dbContext.Groups.AddAsync(entity);
            var relation = new UserGroup
            {
                GroupId = entity.Id,
                UserId = entity.OwnerId
            };
            await _dbContext.UsersGroups.AddAsync(relation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateTaskAsync(long groupId, GroupModel.TaskInGroup taskModel)
        {
            var entity = new TaskInfo
            {
                GroupId = groupId,
                OwnerId = taskModel.OwnerId,
                Award = taskModel.Award,
                Content = taskModel.Content,
                LastModifierId = taskModel.OwnerId,
                LastModifiedTime = DateTime.Now,
                State = (int) GroupModel.TaskState.Created
            };
            await _dbContext.Tasks.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeTaskStateAsync(long groupId, long taskId, int state)
        {
            var query = from g in _dbContext.Groups
                join t in _dbContext.Tasks.DefaultIfEmpty() on g.Id equals t.GroupId
                where g.Id == groupId && t.Id == taskId
                select t;
            var task = await query.FirstOrDefaultAsync();
            if (task == null) return;
            task.State = state;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GroupModel> FindByIdAsync(long groupId)
        {
            var entity = await GroupModels.Where(x=> x.Id == groupId).FirstOrDefaultAsync();
            if (entity == null) return null;
            var tasks = await TaskModels.Where(x=>x.GroupId == entity.Id).ToListAsync();
            foreach (var taskInGroup in tasks)
            {
                entity.Tasks.Add(taskInGroup);
            }

            return entity;
        }

        public async Task<GroupModel> FindByCodeAsync(string code)
        {
            var entity = await GroupModels.Where(x => x.Code == code).FirstOrDefaultAsync();
            if (entity == null) return null;
            var tasks = await TaskModels.Where(x => x.GroupId == entity.Id).ToListAsync();
            foreach (var taskInGroup in tasks)
            {
                entity.Tasks.Add(taskInGroup);
            }

            return entity;
        }


        public async Task JoinGroupAsync(long userId, long groupId)
        {
            var entity = new UserGroup
            {
                UserId = userId,
                GroupId = groupId
            };
            await _dbContext.UsersGroups.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
