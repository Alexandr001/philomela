using Microsoft.AspNetCore.Mvc;

namespace Philomela.Application.ActionsFilters.ApiKey
{
    /// <summary>
    ///     Атрибут авторизации по ключу.
    /// </summary>
    public class ApiKeyAttribute : ServiceFilterAttribute<ApiKeyAuthorizationFilter>;
}
