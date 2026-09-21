using KT_31_23_Grigorev_Serafim.Database;
using KT_31_23_Grigorev_Serafim.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using System.Reflection;




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
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Project Practicum. KT-31-23. Grigorev Serafim. Project API", Version = "v1" });

        // Подключение XML-комментариев
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        options.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddServices();
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