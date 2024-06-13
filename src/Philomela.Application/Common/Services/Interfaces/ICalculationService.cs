namespace Philomela.Application.Common.Services.Interfaces
{
    /// <summary>
    ///     Сервис вычислений.
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        ///     Метод преобразования выражения в обратную польскую запись.
        /// </summary>
        /// <param name="infix"> Исходное выражение.</param>
        /// <returns></returns>
        string GetReversePolishEntry(string infix);

        /// <summary>
        ///     Метод вычисления результата по обратной польской записи.
        /// </summary>
        /// <param name="postfix"> Обратная польская запись.</param>
        /// <returns></returns>
        double CalculateExpression(string postfix);
    }
}
