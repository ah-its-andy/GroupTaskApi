using GroupTaskApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Snowflake.Core;
using System;
using GroupTaskApi.Repository;
using GroupTaskApi.Security;
using GroupTaskApi.Service;

namespace GroupTaskApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton(new IdWorker(1, 1))
                .AddSingleton<ValueGenerator<long>, SnowFlakeIdValueGenerator>()
                .AddSingleton<ValueGenerator<DateTime>, DateTimeValueGenerator>();

            services.AddDbContext<GroupTaskDbContext>(x => x.UseMySql(Configuration.GetConnectionString("default")));

            services.AddScoped<UserRepository>();
            services.AddScoped<GroupRepository>();
            services.AddScoped<GroupService>();
            services.AddScoped<IdentityService>();
            services.AddScoped<TaskService>();
            services.AddScoped<UserService>();
            services.AddScoped<WechatOpenService>();
            services.AddScoped(provider => new TokenUtil(provider));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<GroupTaskDbContext>().Database.EnsureCreated();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
