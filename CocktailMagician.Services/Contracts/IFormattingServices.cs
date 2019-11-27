using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IFormattingService
    {
        Byte[] HtmlStringToPDF(string html);

        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
