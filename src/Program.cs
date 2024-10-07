using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ContosoCrafts.WebSite
{
    /// <summary>
    /// The program class configures all of the services that are required by the app
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the code base
        /// </summary>
        /// <param name="args"> A string array to be used as the input for building the host </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Helper method to build the host, the host encapsulates all of the apps resources
        /// </summary>
        /// <param name="args"></param>
        /// <returns> IHostBuilder </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

}