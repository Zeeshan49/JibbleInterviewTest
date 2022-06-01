// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");





using JibbleInterviewTest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        //var peopleService = services.GetRequiredService<IPeopleService>();
        //People people = new People(peopleService);
        //var data = await people.GetPeople();
        //foreach (var item in data)
        //    Console.WriteLine(item.UserName);



        //Get By Id
        var peopleService = services.GetRequiredService<IPeopleService>();
        People people = new People(peopleService);
        var data = await people.GetPeopleById("Abc");
        Console.WriteLine($" First Name: {data.FirstName} " +
            $" Last Name: {data.LastName} " +
            $" User Name: {data.UserName} ");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}



