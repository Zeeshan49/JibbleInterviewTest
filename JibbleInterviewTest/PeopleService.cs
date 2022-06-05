using JibbleInterviewTest.Models;

namespace JibbleInterviewTest
{
    public interface IPeopleService
    {
        Task<PeopleRowModel> Get(SearchRequest request);
        Task<PeopleRowModel> Get();
        Task<PeopleModel> GetById(string Id);
    }

    public class PeopleService : IPeopleService
    {
        private readonly HttpClient _httpClient;
        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PeopleRowModel> Get(SearchRequest request)
        {
            string? apiurl = $"{Api.People}?$filter={request.Col} eq '{request.Value}'";
            HttpResponseMessage? response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleRowModel> Get()
        {          
            HttpResponseMessage? response = await _httpClient.GetAsync($"{Api.People}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleModel> GetById(string id)
        {            
            HttpResponseMessage? response = await _httpClient.GetAsync($"{Api.People}('{id}')");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleModel>();
        }

    }
}