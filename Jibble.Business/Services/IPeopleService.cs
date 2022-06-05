using Jibble.Business.Models;

namespace Jibble.Business.Services
{
    public interface IPeopleService
    {
        Task<PeopleRowModel> Get(SearchRequest request);
        Task<PeopleRowModel> Get();
        Task<PeopleModel> GetById(string Id);
    }
}