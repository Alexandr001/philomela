using System.Text.Json.Serialization;
using MediatR;

namespace Philomela.Application.Commands.Calculate
{
    /// <summary>
    ///     Команда расчета.
    /// </summary>
    public class CalculateCommand : IRequest<CalculateCommandResponse>
    {
        /// <summary>
        ///     Выражение для преобразования и расчета. 
        /// </summary>
        [JsonPropertyName("expression")]
        public string Expression { get; set; } = string.Empty;
    }
}
