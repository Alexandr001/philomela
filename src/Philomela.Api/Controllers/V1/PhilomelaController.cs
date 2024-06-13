using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Philomela.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PhilomelaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PhilomelaController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        ///     Получение всех птичек
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPhilomelsAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        ///     Получение птички по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> GetPhilomelaByIdAsync(int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        ///     Создание птички
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreatePhilomelaAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        ///     Изменение параметров птички
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditPhilomelaAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }

        /// <summary>
        ///     Удаление птички по id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhilomelaByIdAsync(int id, CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
