using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FootballQuizAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
         .UseUrls("http://*:5000")
         .UseContentRoot(Directory.GetCurrentDirectory())
         .UseIISIntegration()
         .UseStartup<Startup>();
    }
}
