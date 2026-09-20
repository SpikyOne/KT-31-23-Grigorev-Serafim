using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Services;




namespace KT_31_23_Grigorev_Serafim.ServiceExtensions
{
    public static class ServiceExtensions
    {

        // Здесь необходимо регистрировать все сервисы
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            // Сервис № 1. Список групп с фильтрацией
            services.AddScoped<IGroupService, GroupService>();

            // Сервис № 2. Список студентов с фильтрацией
            services.AddScoped<IStudentService, StudentService>();

            // Сервис № 3. Список дисциплин с фильтрацией
            services.AddScoped<IDisciplineService, DisciplineService>();


            return services;

        }

    }

}
