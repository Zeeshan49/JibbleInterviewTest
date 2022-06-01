using Newtonsoft.Json;

namespace JibbleInterviewTest
{
    public interface IPeopleService
    {
        Task<List<PeopleModel>> Get();
        Task<PeopleModel> GetById(string Id);
    }

    public class PeopleService : IPeopleService
    {
        private HttpClient _httpClient;

        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PeopleModel>> Get()
        {
            var apiurl = $"people";
            var response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response.StatusCode}");

            var str = await response.Content.ReadAsStringAsync();

            if (str.IsValidJson())
                return str.Deserialize<PeopleModel>();

            else
                return null;

        }


        public async Task<PeopleModel> GetById(string Id)
        {
            var apiurl = $"People('russellwhyte')";
            var response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response.StatusCode}");

            var str = await response.Content.ReadAsStringAsync();

            if (str.IsValidJson())
                return str.DeserializeObject<PeopleModel>();

            else
                return null;

        }


    }

    public class PeopleModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

