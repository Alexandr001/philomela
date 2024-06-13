using Mapster;
using Philomela.Application.Queries.Employee.V1.ManyEmployees;

namespace Philomela.Application.ProviderEntities
{
    /// <summary>
    ///     Сущность сотрудника в Ldap.  
    /// </summary>
    public class EmployeeLdap : IRegister
    {
        /// <summary>
        ///     Дата создания УЗ. 
        /// </summary>
        public string WhenCreated { get; set; } = string.Empty;

        /// <summary>
        ///     ФИО.
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        ///     Должность.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Имя пользователя.
        /// </summary>
        public string SamAccountName { get; set; } = string.Empty;

        /// <summary>
        ///     Отдел.
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        ///     Метод конфигруции MAPSTER. 
        /// </summary>
        /// <param name="config"></param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EmployeeLdap, GetEmployeeResponse>()
                .Map(dest => dest.WhenCreated,
                    src => DateTime.Parse(src.WhenCreated));
        }
    }
}
