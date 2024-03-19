using SilentPoker.Models;
using EndpointAPI;

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

    }
}
