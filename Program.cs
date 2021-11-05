using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Emergent.Code.Test
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>()).Build().Run();
        }
    }
}