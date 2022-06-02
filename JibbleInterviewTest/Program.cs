// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");





using JibbleInterviewTest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>

             services.AddTransient<IPeopleService, PeopleService>()
            .AddHttpClient<IPeopleService, PeopleService>(c =>
            {
                c.BaseAddress = new Uri("https://services.odata.org/TripPinRESTierService/");
            }))
    .Build();




//required using Microsoft.Extensions.DependencyInjection;
// required using Microsoft.AspNetCore.Identity;
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {

        var peopleService = services.GetRequiredService<IPeopleService>();



        await Get();
        await GetFiltered("Scott", "FirstName");
        await GetById("Scott");

        async Task GetFiltered(string value, string col)
        {
            Console.WriteLine($"____________________GetFiltered____________________");
            //Search
            var peopleService = services.GetRequiredService<IPeopleService>();
            People people = new People(peopleService);

            SearchRequest searchRequest = new SearchRequest
            {
                Value = value
            };

            var data = await people.GetPeople(searchRequest);
            DisplayList(data);
        }

        async Task Get()
        {
            Console.WriteLine($"____________________Get____________________");
            var peopleService = services.GetRequiredService<IPeopleService>();
            People people = new People(peopleService);
            var data = await people.GetPeople();
            DisplayList(data);
        }

        async Task GetById(string id)
        {
            Console.WriteLine($"____________________GetById____________________");
            var peopleService = services.GetRequiredService<IPeopleService>();
            People people = new People(peopleService);
            var data = await people.GetPeopleById(id);
            Display(data);

        }

        void Display(PeopleModel model)
        {
            Console.WriteLine($" First Name: {model.FirstName} " +
               $" Last Name: {model.LastName} " +
               $" User Name: {model.UserName} ");
        }

        void DisplayList(PeopleRowModel model)
        {
            foreach (var item in model.Value)
                Console.WriteLine($"User Name: {item.UserName} First Name: {item.FirstName} Last Name: {item.LastName}");
        }


        Console.ReadLine();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}



