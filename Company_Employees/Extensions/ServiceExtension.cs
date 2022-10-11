namespace Company_Employees.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(Options =>
            {

            });
    }
}
