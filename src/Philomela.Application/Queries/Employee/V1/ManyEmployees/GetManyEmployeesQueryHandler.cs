using MediatR;
using Philomela.Application.Common.Services.Interfaces;

namespace Philomela.Application.Queries.Employee.V1.ManyEmployees
{
    /// <summary>
    ///     Обработчик запросов <see cref="GetManyEmployeesQuery"/>.
    /// </summary>
    public class GetManyEmployeesQueryHandler : IRequestHandler<GetManyEmployeesQuery, List<GetEmployeeResponse>>
    {
        private readonly IEmployeeService _employeeService;

        public GetManyEmployeesQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Task<List<GetEmployeeResponse>> Handle(GetManyEmployeesQuery? request, CancellationToken cancellationToken)
        {
            return _employeeService.GetManyEmployeesAsync(request, cancellationToken);
        }
    }
}
