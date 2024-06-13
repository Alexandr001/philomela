namespace Philomela.Application.Queries.Employee.V1.ManyEmployees.Enums
{
    /// <summary>
    ///     Перечисление для сортировки.
    /// </summary>
    public enum SortingEmployeesField
    {
        /// <summary>
        ///     По умолчанию
        /// </summary>
        None = 0,

        /// <summary>
        ///     Поле - <see cref="GetEmployeeResponse.WhenCreated"/>
        /// </summary>
        Data = 1,

        /// <summary>
        ///     Поле - <see cref="GetEmployeeResponse.DisplayName"/>
        /// </summary>
        Name = 2,
    
        /// <summary>
        ///     Поле - <see cref="GetEmployeeResponse.SamAccountName"/>
        /// </summary>
        Login = 3,
    
        /// <summary>
        ///     Поле - <see cref="GetEmployeeResponse.Department"/>
        /// </summary>
        Department = 4,
    }
}
