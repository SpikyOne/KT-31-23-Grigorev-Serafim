using NLog;
using NLog.Web;




// Инициализация логгера до сборки хоста
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


try
{

    var builder = WebApplication.CreateBuilder(args);

    // Очищаем стандартное логирование и подключаем NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Добавляем сервисы в контейнер
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Настройка пайплайна обработки запросов
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{

    // Логируем фатальную ошибку, если приложение упало при запуске
    logger.Error(ex, "Stopped program because of exception");
    throw;

}

finally
{

    // Гарантированно освобождаем ресурсы NLog
    NLog.LogManager.Shutdown();

}