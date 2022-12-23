
using Microsoft.Extensions.DependencyInjection;

namespace Filament;

internal static class ServicesRegister
{
    public static IServiceCollection AddFilamentServices(this IServiceCollection services)
    {
        services.AddScoped<LazyInitScopeServices>();
        services.AddScoped(static provider => provider.GetRequiredService<LazyInitScopeServices>().PluginHost!);
        services.AddScoped<IPluginHost, PluginHost>();

        services.AddScoped(static provider => provider.GetRequiredService<Photon.Hive.Plugin.IPluginHost>().GetRoomFiber());
        services.AddScoped<IPluginFiber, PluginFiber>();

        services.AddScoped<IPluginLoggerFactory, PluginLoggerFactory>();
        services.AddScoped(static provider => {
            return provider.GetRequiredService<IPluginLoggerFactory>()
                .CreateLogger(provider.GetRequiredService<LazyInitScopeServices>().PluginName);
        });

        services.AddScoped<IHttpClient, HttpClient>();

        return services;
    }
}
