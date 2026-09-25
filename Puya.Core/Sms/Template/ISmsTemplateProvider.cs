using System.Collections.Generic;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public interface ISmsTemplateProvider
    {
        Task<List<SmsTemplate>> GetAllAsync();
        Task AddAsync(SmsTemplate template);
        Task DeleteByCodeAsync(string code);
        Task<SmsTemplate> GetByCodeAsync(string code);
        Task<string> RenderAsync(SmsTemplate template, Dictionary<string, string> parameters);
    }
}
