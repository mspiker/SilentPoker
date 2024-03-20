using SilentPoker.Models;
using EndpointAPI;
using System.Net.NetworkInformation;

namespace SilentPoker.Services
{
    public class ServiceNowAPI
    {
        private readonly EndpointLibrary _endpointLibrary;

        public ServiceNowAPI(string BaseAddress, string AuthorizationHeader)
        {
            _endpointLibrary = new EndpointLibrary(BaseAddress, AuthorizationHeader);
        }

        public async Task<List<Sprint>?> GetSprints()
        {
            var result = await _endpointLibrary.CallAPI<Sprint>(
                EndpointLibrary.Endpoint("rm_sprint", "active=true", "number%2Cshort_description%2Csys_id"));
            return result;
        }

        public async Task<List<Story>?> GetStories(string sprintId, string filter)
        {
            var query = $"sprint={sprintId}";
            if (filter != string.Empty)
            {
                query += $"^{filter}";
            }
            var result = await _endpointLibrary.CallAPI<Story>(
                EndpointLibrary.Endpoint("rm_story", $"{query}", "number%2Cshort_description%2Csys_id%2Cproduct%2Cstory_points%2Cdescription%2Cpriority"));
            return result;
        }

    }
}
