using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Para.Data.UnitOfWork;
using Serilog;

namespace Para.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
        CreateHostBuilder(args).Build().Run();

    }

   
    public static IWebHostBuilder CreateHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
}