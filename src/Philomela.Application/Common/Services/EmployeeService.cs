using Mapster;
using Philomela.Application.Abstractions;
using Philomela.Application.Common.Services.Interfaces;
using Philomela.Application.ProviderEntities;
using Philomela.Application.Queries.Employee.V1.ManyEmployees;
using Philomela.Application.Queries.Employee.V1.ManyEmployees.Enums;

namespace Philomela.Application.Common.Services
{
    /// <inheritdoc />
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly ILdapProvider _ldapProvider;

        public EmployeeService(ILdapProvider ldapProvider)
        {
            _ldapProvider = ldapProvider;
        }

        /// <inheritdoc/>
        public async Task<List<GetEmployeeResponse>> GetManyEmployeesAsync(GetManyEmployeesQuery? @params, CancellationToken cancellationToken)
        {
            List<EmployeeLdap> employees = await _ldapProvider.GetAllDataAsync<EmployeeLdap>("(objectCategory=User)");

            List<GetEmployeeResponse> list = GetValidData(employees)
                                             .Select(e => e.Adapt<GetEmployeeResponse>())
                                             .ToList();

            return SortData(list, @params).ToList();
        }

        /// <summary>
        ///     Получть существующие записи сотрудников.
        /// </summary>
        /// <param name="employees"> Все записи из LDAP</param>
        /// <returns></returns>
        private IEnumerable<EmployeeLdap> GetValidData(IEnumerable<EmployeeLdap> employees)
        {
            const int NAME_WORDS_COUNT_FACTOR = 2;
            return employees.Where(e => !string.IsNullOrEmpty(e.DisplayName) && 
                                        !string.IsNullOrEmpty(e.WhenCreated) && 
                                        e.DisplayName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length >= NAME_WORDS_COUNT_FACTOR);
        }

        /// <summary>
        ///     Сортировка данных.
        /// </summary>
        /// <param name="employees"> Лист для сортировки. </param>
        /// <param name="params"> Параметры для сортировки. </param>
        /// <returns> Отсортированный список. </returns>
        private IEnumerable<GetEmployeeResponse> SortData(List<GetEmployeeResponse> employees, GetManyEmployeesQuery? @params)
        {
            if (@params is null)
            {
                return employees;
            }

            return @params.SortingField switch
            {
                    SortingEmployeesField.None => employees,
                    SortingEmployeesField.Data => @params.IsDescending
                                                          ? employees.OrderByDescending(e => e.WhenCreated)
                                                          : employees.OrderBy(e => e.WhenCreated),
                    SortingEmployeesField.Name => @params.IsDescending
                                                          ? employees.OrderByDescending(e => e.DisplayName)
                                                          : employees.OrderBy(e => e.DisplayName),
                    SortingEmployeesField.Login => @params.IsDescending
                                                           ? employees.OrderByDescending(e => e.SamAccountName)
                                                           : employees.OrderBy(e => e.SamAccountName),
                    SortingEmployeesField.Department => @params.IsDescending
                                                                ? employees.OrderByDescending(e => e.Department)
                                                                : employees.OrderBy(e => e.Department),
                    _ => employees,
            };
        }

    }
}
