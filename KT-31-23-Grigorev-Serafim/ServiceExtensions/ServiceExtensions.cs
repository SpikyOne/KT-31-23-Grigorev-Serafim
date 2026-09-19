using KT_31_23_Grigorev_Serafim.Interfaces;
using KT_31_23_Grigorev_Serafim.Services;




namespace KT_31_23_Grigorev_Serafim.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Здесь будешь регистрировать все новые сервисы
            
            // Сервис № 1
            services.AddScoped<IGroupService, GroupService>();
            

            return services;
        }
    }
}
