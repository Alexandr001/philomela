using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Philomela.Application.Options;

namespace Philomela.Application.ActionsFilters.ApiKey
{
    /// <summary>
    ///     Фильтр авторизации по ключу.
    /// </summary>
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        public const string XApiKey = "X-API-Key";

        private readonly IConfiguration _configuration;

        public ApiKeyAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        ///     Метод авторизации по API Key.
        /// </summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? values = context.HttpContext.Request.Headers[XApiKey];
            if (CheckKey(values) == false)
            {
                context.Result = new UnauthorizedResult();
            }
        }

        /// <summary>
        ///     Метод проверки ключа.
        /// </summary>
        /// <param name="key"> Ключ для проверки. </param>
        /// <returns>TRUE - ключ прошел проверку, FALSE - не прошел. </returns>
        /// <exception cref="InvalidOperationException"> Если в файле конфигурации не указан ключ. </exception>
        private bool CheckKey(string? key)
        {
            string? apiKey = _configuration.GetRequiredSection(SectionNameConstants.ApiKey).Value;
            return apiKey == key;
        }
    }
}
