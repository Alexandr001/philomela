namespace Philomela.Domain.Entities.Philomela
{
    /// <summary>
    ///     Репозиторий филомелы. :)
    /// </summary>
    public interface IPhilomelaRepository
    {
        /// <summary>
        ///     Создание птички.
        /// </summary>
        /// <param name="philomela"> Модель птички. </param>
        /// <param name="cancellationToken"></param>
        Task CreatePhilomelaAsync(Philomela philomela, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Обновление птички.
        /// </summary>
        /// <param name="philomela"> Модель птички. </param>
        /// <param name="cancellationToken"></param>
        Task UpdatePhilomelaAsync(Philomela philomela, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Удаление птички по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор птички. </param>
        /// <param name="cancellationToken"></param>
        Task DeletePhilomelsAsync(int id, CancellationToken cancellationToken = default);
    }
}
