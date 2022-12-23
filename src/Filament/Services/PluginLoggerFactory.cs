
using Photon.Hive.Plugin;

namespace Filament;

public interface IPluginLoggerFactory
{
    IPluginLogger CreateLogger(string name);
}

internal class PluginLoggerFactory: IPluginLoggerFactory
{
    protected Photon.Hive.Plugin.IPluginHost PluginHost { get; }

    public PluginLoggerFactory(Photon.Hive.Plugin.IPluginHost pluginHost)
    {
        this.PluginHost = pluginHost;
    }

    public IPluginLogger CreateLogger(string name)
        => this.PluginHost.CreateLogger(name);
}
