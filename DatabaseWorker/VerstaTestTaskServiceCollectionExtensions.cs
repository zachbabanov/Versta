using DatabaseWorker.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseWorker;

public static class VerstaTestTaskServiceCollectionExtensions
{
    public static IServiceCollection AddVerstaTestTaskContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<VerstaTestTaskContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IVerstaTestTaskRepository, VerstaTestTaskRepository>();
        return services;
    }
    public static void EnsureDBCreated(this WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<VerstaTestTaskContext>();
            dbContext.EnsureCreated();
        }
    }
}