using Jibble.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jibble.App
{
    public interface IRouterConfig
    {
        Task Route(string key);
    }
    public class RouterConfig : IRouterConfig
    {
        private readonly IPeople _people;
        public RouterConfig(IPeople people)
        {
            _people = people;
        }

        public async Task Route(string key)
        {
            try
            {
                switch (key)
                {
                    case "1":
                        Console.WriteLine("Please type column name");
                        string? col = Console.ReadLine();
                        Console.WriteLine("Please type to search");
                        string? value = Console.ReadLine();
                        SearchRequest searchRequest = new SearchRequest
                        {
                            Value = value,
                            Col = col
                        };
                        await _people.GetPeople(searchRequest);
                        break;
                    case "2":
                        Console.WriteLine($"Enter user name for details");
                        string id = Console.ReadLine();
                        await _people.GetPeopleById(id);
                        break;
                    default:
                        Console.WriteLine($"your input is not Correct");
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured please try again /r/n { ex.Message}");
            }

        }
    }
}
