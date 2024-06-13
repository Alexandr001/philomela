using MediatR;
using Philomela.Application.Queries.Employee.V1.ManyEmployees.Enums;

namespace Philomela.Application.Queries.Employee.V1.ManyEmployees
{
    /// <summary>
    ///     Запрос получения данных всех сотрудников.
    /// </summary>
    public class GetManyEmployeesQuery : IRequest<List<GetEmployeeResponse>>
    {
        /// <summary>
        ///     Сортировка по возрастанию или убыванию.
        /// </summary>
        public bool IsDescending { get; set; } = true;

        /// <summary>
        ///     Фильтр для сортировки.
        /// </summary>
        public SortingEmployeesField SortingField { get; set; }
    }
}
