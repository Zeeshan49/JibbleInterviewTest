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



//required using Microsoft.Extensions.DependencyInjection;
//required using Microsoft.AspNetCore.Identity;
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var people = services.GetRequiredService<IPeople>();

    await GetFiltered("Scott", "FirstName");
    await Get();
    await GetById("russellwhyte");

    async Task GetFiltered(string value, string col)
    {
        Console.WriteLine($"____________________{nameof(GetFiltered)}____________________");
        //Search              
        SearchRequest searchRequest = new SearchRequest
        {
            Value = value
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