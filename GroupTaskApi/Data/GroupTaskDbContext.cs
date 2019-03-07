using GroupTaskApi.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace GroupTaskApi.Data
{
    public class GroupTaskDbContext : DbContext
    {
        public GroupTaskDbContext(DbContextOptions<GroupTaskDbContext> dbContextOptions) 
            : base(dbContextOptions)
        {
        }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<TaskInfo> Tasks { get; set; }
        public DbSet<GroupInfo> Groups { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInfo>()
                .ToTable("gt_user")
                .HasKey(e => e.Id);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Id)
                .HasValueGenerator<SnowFlakeIdValueGenerator>();

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.CreateTime)
                .HasValueGenerator<DateTimeValueGenerator>();

            modelBuilder.Entity<TaskInfo>()
                .ToTable("gt_task")
                .HasKey(e => e.Id);

            modelBuilder.Entity<TaskInfo>()
                .Property(e => e.Id)
                .HasValueGenerator<SnowFlakeIdValueGenerator>();

            modelBuilder.Entity<TaskInfo>()
                .Property(e => e.CreateTime)
                .HasValueGenerator<DateTimeValueGenerator>();

            modelBuilder.Entity<GroupInfo>()
                .ToTable("gt_group")
                .HasKey(e => e.Id);

            modelBuilder.Entity<GroupInfo>()
                .Property(e => e.Id)
                .HasValueGenerator<SnowFlakeIdValueGenerator>();

            modelBuilder.Entity<GroupInfo>()
                .Property(e => e.CreateTime)
                .HasValueGenerator<DateTimeValueGenerator>();

            modelBuilder.Entity<GroupInfo>()
                .Property(e => e.Code)
                .HasValueGenerator<RandomCodeValueGenerator>();

            modelBuilder.Entity<UserGroup>()
                .ToTable("gt_user_group")
                .HasKey(e => e.Id);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.Id)
                .HasValueGenerator<SnowFlakeIdValueGenerator>();

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.CreateTime)
                .HasValueGenerator<DateTimeValueGenerator>();
        }
    }
}
