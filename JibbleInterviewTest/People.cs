using Jibble.Business.Models;
using Jibble.Business.Services;
using Jibble.Business.Utilities;

namespace Jibble.App
{
    public interface IPeople
    {
        Task GetPeople(SearchRequest request);
        Task GetPeople();
        Task GetPeopleById(string id);
    }
    public class People : IPeople
    {
        private readonly IPeopleService _peopleService;
        public People(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task GetPeople(SearchRequest request)
        {
            var result = await _peopleService.Get(request);
            result.Display();
        }

        public async Task GetPeople()
        {
            var result = await _peopleService.Get();
            result.Display();
        }

        public async Task GetPeopleById(string id)
        {
            var result = await _peopleService.GetById(id);
            result.Display();
        }
    }
}
