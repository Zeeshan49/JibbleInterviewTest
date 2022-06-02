using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibbleInterviewTest
{
    public interface IPeople { }
    public class People : IPeople
    {
        private readonly IPeopleService _peopleService;       
        public People(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<PeopleRowModel> GetPeople(SearchRequest request)
        {
            return await _peopleService.Get(request);
        }

        public async Task<PeopleRowModel> GetPeople()
        {
            return await _peopleService.Get();
        }       

        public async Task<PeopleModel> GetPeopleById(string id)
        {
            return await _peopleService.GetById(id);
        }

    }
}
