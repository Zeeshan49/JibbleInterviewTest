using JibbleInterviewTest;
using JibbleInterviewTest.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
     services.AddTransient<IPeople, People>()
             .AddTransient<IPeopleService, PeopleService>()
             .AddHttpClient<IPeopleService, PeopleService>(c =>
            {
                c.BaseAddress = new Uri(Api.ODATA_URL);
            }))
    .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var people = services.GetRequiredService<IPeople>();

    await Get();
    Console.WriteLine($"\r\n\r\n Please Choose" +
    $"\r\n\r\n 1. To Filter/Search People " +
    $"\r\n\r\n 2. Detail of a specific people");

    var key = Console.ReadLine();

    switch (key)
    {
        case "1":
            Console.WriteLine("Please type column name (without space)");
            var col = Console.ReadLine();
            Console.WriteLine("Please type to search");
            var value = Console.ReadLine();
            await GetFiltered(value, col);
            break;
        case "2":
            Console.WriteLine($"Enter user name for details");
            string id = Console.ReadLine();
            await GetById(id ?? "");
            break;
        default:
            break;
    }


    async Task GetFiltered(string value, string col)
    {
        SearchRequest searchRequest = new SearchRequest
        {
            Value = value,
            Col = col
        };

        var data = await people.GetPeople(searchRequest);
        data.Display();
    }

    async Task Get()
    {
        Console.WriteLine($"____________________{nameof(Get)}____________________");
        var data = await people.GetPeople();
        data.Display();
    }

    async Task GetById(string id)
    {
        Console.WriteLine($"____________________{nameof(GetById)}____________________");
        var data = await people.GetPeopleById(id);
        data.Display();
    }



    Console.ReadLine();
}