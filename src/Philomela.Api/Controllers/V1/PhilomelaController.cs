using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Philomela.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhilomelaController : ControllerBase
    {
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
    }
}
