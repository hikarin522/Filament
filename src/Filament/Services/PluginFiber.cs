using System;
using System.Reactive.Disposables;
using System.Threading;

namespace Filament;

public interface IPluginFiber
{
    int Enqueue(Action action);

    IDisposable CreateTimer(Action action, int firstInMs, int regularInMs);

    IDisposable CreateOneTimeTimer(Action action, int firstInMs);
}

internal class PluginFiber: IPluginFiber
{
    protected Photon.Hive.Plugin.IPluginFiber Fiber { get; }

    public PluginFiber(Photon.Hive.Plugin.IPluginFiber fiber)
    {
        this.Fiber = fiber;
    }

    public int Enqueue(Action action) => this.Fiber.Enqueue(action);

    public IDisposable CreateTimer(Action action, int firstInMs, int regularInMs)
        => Disposable.Create(this.Fiber.CreateTimer(action, firstInMs, regularInMs), this.Fiber.StopTimer);

    public IDisposable CreateOneTimeTimer(Action action, int firstInMs)
        => Disposable.Create(this.Fiber.CreateOneTimeTimer(action, firstInMs), this.Fiber.StopTimer);
}

public static class PluginFiberExtensions
{
    public static IDisposable CreateTimer(this IPluginFiber fiber, Action callback, int firstInMs, int regularInMs, CancellationToken ct)
    {
        var timer = fiber.CreateTimer(callback, firstInMs, regularInMs);
        return new CompositeDisposable(ct.Register(static d => ((IDisposable)d).Dispose(), timer), timer);
    }

    public static IDisposable CreateOneTimeTimer(this IPluginFiber fiber, Action callback, int firstInMs, CancellationToken ct)
    {
        var timer = fiber.CreateOneTimeTimer(callback, firstInMs);
        return new CompositeDisposable(ct.Register(static d => ((IDisposable)d).Dispose(), timer), timer);
    }
}
