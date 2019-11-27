using CocktailMagician.Services.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class ApiServices : IApiServices
    {

        public ApiServices()
        { }

        public async Task<string> CallApiAsync(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}");
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public string FormQuery(string separator, string[] queryParams)
        {
            return string.Join(separator, queryParams);
        }

        public string FormatApiTemplate(string apiTemplate, string[] queryParams, string returnFormat = "json", int resultLimit = 10)
        {
            var parsedQuery = FormQuery("+", queryParams);
            return string.Format(apiTemplate, parsedQuery, returnFormat, resultLimit);
        }
    }
}
