using Jibble.Business.Models;
using Jibble.Business.Utilities;

namespace Jibble.Business.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly HttpClient _httpClient;
        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PeopleRowModel> Get(SearchRequest request)
        {
            string? apiurl = $"{ApiConst.People}?$filter={request.Col.ReplaceWhitespace()} eq '{request.Value}'";
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
            HttpResponseMessage? response = await _httpClient.GetAsync($"{ApiConst.People}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleModel> GetById(string id)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"{ApiConst.People}('{id}')");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleModel>();
        }

    }
}