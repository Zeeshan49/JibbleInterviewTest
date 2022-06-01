using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JibbleInterviewTest
{
    public class People
    {
        private readonly IPeopleService _peopleService;

        public People()
        {

        }
        public People(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public async Task<List<PeopleModel>> GetPeople()
        {
            return await _peopleService.Get();
        }


        public async Task<PeopleModel> GetPeopleById(string Id)
        {
            return await _peopleService.GetById(Id);
        }

    }
}
