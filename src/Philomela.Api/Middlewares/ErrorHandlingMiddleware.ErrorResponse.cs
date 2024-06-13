using Microsoft.AspNetCore.Mvc;

namespace Philomela.Api.Middlewares
{
    /// <summary>
    /// Ответ с сообщением об ошибке.
    /// </summary>
    internal class ErrorResponse : ProblemDetails
    {
    }
}
