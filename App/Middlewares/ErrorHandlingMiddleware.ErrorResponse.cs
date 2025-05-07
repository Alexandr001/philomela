using Microsoft.AspNetCore.Mvc;

namespace App.Middlewares
{
    /// <summary>
    /// Ответ с сообщением об ошибке.
    /// </summary>
    internal class ErrorResponse : ProblemDetails
    {
    }
}
