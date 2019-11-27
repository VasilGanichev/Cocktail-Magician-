using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IApiServices
    {
        Task<string> CallApiAsync(string url);

        string FormQuery(string separator, string[] queryParams);

        string FormatApiTemplate(string apiTemplate, string[] queryParams, string returnFormat = "json", int resultLimit = 10);
    }
}
