using Jibble.App;
using Jibble.Business.Models;
using Jibble.Business.Services;
using Jibble.Business.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
     services.AddTransient<IPeople, People>()
             .AddTransient<IPeopleService, PeopleService>()
             .AddHttpClient<IPeopleService, PeopleService>(c =>
            {
                c.BaseAddress = new Uri(ApiConst.ODATA_URL);
            }))
    .Build();

using (IServiceScope? scope = host.Services.CreateScope())
{
    IServiceProvider? services = scope.ServiceProvider;
    IPeople? people = services.GetRequiredService<IPeople>();

    await people.GetPeople();
    var isStop = false;
    do
    {
        Console.WriteLine($"\r\n\r\n Please Choose" +
        $"\r\n\r\n 1. To Filter/Search People " +
        $"\r\n\r\n 2. Detail of a specific people");

        string? key = Console.ReadLine();
        RouterConfig routerConfig = new RouterConfig(people);
        await routerConfig.Route(key);

        Console.WriteLine($"to stop application press (S) or press any other key to continue");
        string value = Console.ReadLine();
        isStop = value.ToLower() == "s" ? false : true;
    } while (isStop);
}