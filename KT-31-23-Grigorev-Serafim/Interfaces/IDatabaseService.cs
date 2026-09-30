using KT_31_23_Grigorev_Serafim.DTOs.Database;
using System.Threading;
using System.Threading.Tasks;




namespace KT_31_23_Grigorev_Serafim.Interfaces
{

    /// <summary>
    /// Интерфейс сервиса для глобального обслуживания базы данных
    /// </summary>
    public interface IDatabaseService
    {

        /// <summary>
        /// Выполняет полную очистку всех таблиц в базе данных
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        Task ClearDatabaseAsync(CancellationToken cancellationToken = default);


        /// <summary>
        /// Формирует и возвращает полный дамп данных всех таблиц
        /// </summary>
        Task<DatabaseDumpDto> GetDatabaseDumpAsync(CancellationToken cancellationToken = default);


        /// <summary>
        /// Загружает дамп данных в базу данных
        /// </summary>
        Task RestoreDatabaseDumpAsync(DatabaseDumpDto dump, CancellationToken cancellationToken = default);

    }

}
