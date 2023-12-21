using Circus.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Circus.Server.Extensions;

public static class HostBuilderExtensions
{
    public static async Task<IHost> MigrateDatabase(this IHost webHost)
    {
        using var serviceScope = webHost.Services.CreateScope();
        await using var context = serviceScope.ServiceProvider.GetService<CircusContext>()!;
        
        await context.Database.MigrateAsync();

        return webHost;
    }
}