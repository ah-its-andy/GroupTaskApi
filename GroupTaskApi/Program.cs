using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GroupTaskApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureKestrel(opts =>
                {
                    opts.Listen(IPAddress.Parse("::"), 5100, listenOpts =>
                        {
                            listenOpts.UseHttps(
                                Path.Combine(Directory.GetCurrentDirectory(), "1899676_gt.standardcore.io.pfx"),
                                "7X7Mb37e");
                        });
                    opts.Listen(IPAddress.Parse("::"), 5000);
                })
                .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
