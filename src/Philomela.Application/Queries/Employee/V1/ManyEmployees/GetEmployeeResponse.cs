using System.Text.Json.Serialization;

namespace Philomela.Application.Queries.Employee.V1.ManyEmployees
{
    /// <summary>
    ///     Модель сотрудника.
    /// </summary>
    public class GetEmployeeResponse
    {
        /// <summary>
        ///     Дата создания УЗ. 
        /// </summary>
        [JsonPropertyName("result")]
        public DateTime WhenCreated { get; set; }

        /// <summary>
        ///     ФИО.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; } = string.Empty;
    
        /// <summary>
        ///     Должность.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Имя пользователя.
        /// </summary>
        [JsonPropertyName("samAccountName")]
        public string SamAccountName { get; set; } = string.Empty;
    
        /// <summary>
        ///     Отдел.
        /// </summary>
        [JsonPropertyName("department")]
        public string Department { get; set; } = string.Empty;
    }
}
