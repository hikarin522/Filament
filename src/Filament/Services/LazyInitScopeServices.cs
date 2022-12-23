
namespace Filament;

internal class LazyInitScopeServices
{
    public string PluginName { get; set; } = string.Empty;

    public Photon.Hive.Plugin.IPluginHost? PluginHost { get; set; }
}
