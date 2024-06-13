using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Philomela.Application.Queries.Employee.V1.ManyEmployees;

namespace Philomela.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Метод получения сотрудников.
        /// </summary>
        /// <param name="params"> Параметры. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetManyEmployeesAsync([FromQuery] GetManyEmployeesQuery? @params, CancellationToken cancellationToken)
        {
            List<GetEmployeeResponse> employees = await _mediator.Send(@params, cancellationToken);
            return Ok(employees);
        }
    }
}
