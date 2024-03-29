﻿using Newtonsoft.Json.Linq;

namespace EndpointAPI
{
    public class EndpointLibrary
    {
        private readonly HttpClient _httpClient;

        public EndpointLibrary(string BaseAddress, string AuthorizationHeader)
        {
            try {
                _httpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
                _httpClient.DefaultRequestHeaders.Add("Authorization", AuthorizationHeader);
            } 
            catch { 
                // Do something here.
            }
            
        }
        public static string Endpoint(string table, string query, string fields)
        {
            string endpoint = $"/api/now/table/{table}?sysparm_query={query}&sysparm_display_value=true&sysparm_exclude_reference_link=true&sysparm_fields={fields}";
            return endpoint;
        }
        public async Task<List<T>?> CallAPI<T>(string endpoint)
        {
            if (_httpClient == null) return new List<T>();

            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                JObject root = JObject.Parse(content);
                if (root != null)
                    if (root.ContainsKey("result"))
                    {
                        var result = root["result"];
                        if (result != null)
                        {
                            return result.ToObject<List<T>>();
                        }
                    }
                return null;
            }
            else
            {
                // Handle error responses here
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
        }
    }
}
