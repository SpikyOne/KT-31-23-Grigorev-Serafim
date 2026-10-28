using Microsoft.AspNetCore.Mvc;

namespace KT_31_23_Grigorev_Serafim.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExceptionTestController : ControllerBase
    {
        // Вызовет ArgumentException -> статус 400 Bad Request
        [HttpGet("bad-request")]
        public IActionResult ThrowArgumentException()
        {
            throw new ArgumentException("Передан некорректный аргумент.");
        }

        // Вызовет KeyNotFoundException -> статус 404 Not Found
        [HttpGet("not-found")]
        public IActionResult ThrowKeyNotFoundException()
        {
            throw new KeyNotFoundException("Сущность по заданному ключу не найдена.");
        }

        // Вызовет базовое Exception -> статус 500 Internal Server Error
        [HttpGet("server-error")]
        public IActionResult ThrowStandardException()
        {
            throw new Exception("Произошла непредвиденная ошибка сервера.");
        }
    }
}
