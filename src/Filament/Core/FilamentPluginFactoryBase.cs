using System;
using System.IO;
using System.Reflection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using Photon.Hive.Plugin;

namespace Filament;

public abstract class FilamentPluginFactoryBase: PluginFactoryBase, IPluginFactory2
{
    protected IServiceProvider? Provider { get; set; }

    public new void SetFactoryHost(IFactoryHost factoryHost, FactoryParams factoryParams)
    {
        base.SetFactoryHost(factoryHost, factoryParams);

        var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var fileProvider = new PhysicalFileProvider(rootPath);

        var builder = new ConfigurationBuilder();
        this.Configure(fileProvider, factoryParams, builder);
        var config = builder.Build();

        var services = new ServiceCollection();
        this.ConfigureServices(fileProvider, config, services);
        this.Provider = services.BuildServiceProvider();
    }

    protected virtual void Configure(IFileProvider fileProvider, FactoryParams factoryParams, IConfigurationBuilder builder)
    {
        builder.AddJsonFile(fileProvider, "appsettings.json", true, false);
        builder.AddInMemoryCollection(factoryParams.PluginConfig);
    }

    protected virtual void ConfigureServices(IFileProvider fileProvider, IConfiguration config, IServiceCollection services)
    {
        services.AddSingleton(fileProvider);
        services.AddSingleton(config);
        services.AddFilamentServices();
    }
}
