using System.Text.Json.Serialization;

namespace Philomela.Application.Commands.Calculate
{
    /// <summary>
    ///     Команда ответа для расчета.
    /// </summary>
    public class CalculateCommandResponse
    {
        /// <summary>
        ///     Результат расчета выражения.
        /// </summary>
        [JsonPropertyName("result")]
        public double Result { get; set; }

        /// <summary>
        ///     Выражение в постфиксной (обратная польская нотация) форме.
        /// </summary>
        [JsonPropertyName("postfix")]
        public string Postfix { get; set; }
    }
}
