using KT_31_23_Grigorev_Serafim.DTOs.Database;
using KT_31_23_Grigorev_Serafim.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KT_31_23_Grigorev_Serafim.Controllers
{

    /// <summary>
    /// Контроллер для обслуживания, экспорта и очистки базы данных
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Tags("Сервис 0. Обслуживание базы данных")]
    public class DatabaseController : ControllerBase
    {
        
        private readonly IDatabaseService _databaseService;


        /// <summary>
        /// Инициализирует новый экземпляр контроллера обслуживания БД
        /// </summary>
        /// <param name="databaseService">Сервис работы с базой данных</param>
        public DatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }


        /// <summary>
        /// Выполняет полную очистку всех таблиц базы данных с каскадным удалением
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">База данных успешно очищена</response>
        [HttpDelete("ClearDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ClearDatabaseAsync(CancellationToken cancellationToken)
        {
            await _databaseService.ClearDatabaseAsync(cancellationToken);
            return Ok(new { Message = "База данных успешно очищена" });
        }


        /// <summary>
        /// Получает полный дамп всех таблиц базы данных в формате JSON
        /// </summary>
        [HttpGet("GetDump")]
        [ProducesResponseType(typeof(DatabaseDumpDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDatabaseDumpAsync(CancellationToken cancellationToken)
        {

            var dump = await _databaseService.GetDatabaseDumpAsync(cancellationToken);
            return Ok(dump);

        }


        /// <summary>
        /// Восстанавливает данные в базе данных из переданного дампа
        /// </summary>
        [HttpPost("RestoreDump")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RestoreDatabaseDumpAsync([FromBody] DatabaseDumpDto dump, CancellationToken cancellationToken)
        {

            await _databaseService.RestoreDatabaseDumpAsync(dump, cancellationToken);
            return Ok(new { Message = "База данных успешно восстановлена из дампа" });

        }

    }

}
