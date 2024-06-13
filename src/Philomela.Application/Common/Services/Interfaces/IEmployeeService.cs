using Philomela.Application.Queries.Employee.V1.ManyEmployees;

namespace Philomela.Application.Common.Services.Interfaces
{
    /// <summary>
    ///     Сервис работы с данными сотрудников.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        ///     Получение данных всех сотрудников.
        /// </summary>
        /// <param name="params"> Параметры сортировки. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <returns></returns>
        Task<List<GetEmployeeResponse>> GetManyEmployeesAsync(GetManyEmployeesQuery? @params, CancellationToken cancellationToken = default);
    }
}
