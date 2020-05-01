using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ComicBookAPI.Classes;
using MongoDB.Driver;
using MongoDB.Entities;

namespace ComicBookAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new DB("MyDatabase", "localhost", 27017);
            CreateWebHostBuilder(args).Build().Run();
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
