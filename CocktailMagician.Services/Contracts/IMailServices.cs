using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface IMailServices
    {
        Task<bool> SendEmailToGroup(string from, string[] to, string subject, string body, List<byte[]> attachments = null);

        Task<bool> SendEmailToGroup(string from, string[] to, string subject, string body, byte[] attachment = null);
    }
}
