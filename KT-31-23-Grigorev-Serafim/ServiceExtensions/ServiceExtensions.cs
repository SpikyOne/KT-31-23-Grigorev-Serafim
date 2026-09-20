using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Services;




namespace KT_31_23_Grigorev_Serafim.ServiceExtensions
{
    public static class ServiceExtensions
    {

        // Здесь необходимо регистрировать все сервисы
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            
            // Сервис № 1
            services.AddScoped<IGroupService, GroupService>();

            // Сервис № 2
            services.AddScoped<IStudentService, StudentService>();

            return services;

        }

    }

}
