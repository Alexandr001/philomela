namespace Philomela.Application.Exceptions
{
    /// <summary>
    /// Исключение слоя Application.
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public AppException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        /// <param name="message">Сообщение.</param>
        /// <param name="innerException"><see cref="Exception"/>.</param>
        public AppException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppException"/> class.
        /// </summary>
        public AppException()
        {
        }
    }
}
