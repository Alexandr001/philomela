namespace Philomela.Application.Abstractions
{
    /// <summary>
    ///     Менеджер для взаимодействия с LDAP.
    /// </summary>
    public interface ILdapProvider : IDisposable
    {
        /// <summary>
        ///     Получть все данные по удовлетворяющие условию.
        /// </summary>
        /// <param name="filter"> Условие <see cref="DirectorySearcher.Filter"/>. </param>
        /// <param name="pageSize"> Кол-во элементов <see cref="DirectorySearcher.PageSize"/>. </param>
        /// <typeparam name="T"> Модель в которую дессериализуются данные. </typeparam>
        /// <returns> Список моделей. </returns>
        Task<List<T>> GetAllDataAsync<T>(string? filter = null, int pageSize = default)
                where T : class, new();
    }
}
