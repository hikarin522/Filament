using System;
using System.Collections.Generic;
using System.Collections;

using Photon.Hive.Plugin;

namespace Filament;

public interface IPluginHost
{
    Dictionary<string, object> Environment { get; }

    IList<IActor> GameActors { get; }

    IList<IActor> GameActorsActive { get; }

    IList<IActor> GameActorsInactive { get; }

    string GameId { get; }

    Hashtable GameProperties { get; }

    Dictionary<string, object> CustomGameProperties { get; }

    int MasterClientId { get; }

    bool IsSuspended { get; }

    void BroadcastEvent(IList<int> recieverActors, int senderActor, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default);

    void BroadcastEvent(byte target, int senderActor, byte targetGroup, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default);

    void BroadcastErrorInfoEvent(string message, SendParameters sendParameters = default);

    void BroadcastErrorInfoEvent(string message, ICallInfo info, SendParameters sendParameters = default);

    SerializableGameState GetSerializableGameState();

    bool SetGameState(SerializableGameState state);

    bool SetProperties(int actorNr, Hashtable properties, Hashtable expected, bool broadcast);

    bool RemoveActor(int actorNr, string reasonDetail);

    bool RemoveActor(int actorNr, byte reason, string reasonDetail);

    bool TryRegisterType(Type type, byte typeCode, Func<object, byte[]> serializeFunction, Func<byte[], object> deserializeFunction);

    EnvironmentVersion GetEnvironmentVersion();

    bool ExecuteCacheOperation(CacheOp operation, out string errorMsg);
}

internal class PluginHost: IPluginHost
{
    protected Photon.Hive.Plugin.IPluginHost Host { get; }

    public PluginHost(Photon.Hive.Plugin.IPluginHost host)
    {
        this.Host = host;
    }

    public Dictionary<string, object> Environment => this.Host.Environment;

    public IList<IActor> GameActors => this.Host.GameActors;

    public IList<IActor> GameActorsActive => this.Host.GameActorsActive;

    public IList<IActor> GameActorsInactive => this.Host.GameActorsInactive;

    public string GameId => this.Host.GameId;

    public Hashtable GameProperties => this.Host.GameProperties;

    public Dictionary<string, object> CustomGameProperties => this.Host.CustomGameProperties;

    public int MasterClientId => this.Host.MasterClientId;

    public bool IsSuspended => this.Host.IsSuspended;

    public void BroadcastEvent(IList<int> recieverActors, int senderActor, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default)
        => this.Host.BroadcastEvent(recieverActors, senderActor, cacheOp, data, cacheOp, sendParameters);

    public void BroadcastEvent(byte target, int senderActor, byte targetGroup, byte evCode, Dictionary<byte, object> data, byte cacheOp, SendParameters sendParameters = default)
        => this.Host.BroadcastEvent(target, senderActor, targetGroup, evCode, data, cacheOp, sendParameters);

    public void BroadcastErrorInfoEvent(string message, SendParameters sendParameters = default)
        => this.Host.BroadcastErrorInfoEvent(message, sendParameters);

    public void BroadcastErrorInfoEvent(string message, ICallInfo info, SendParameters sendParameters = default)
        => this.Host.BroadcastErrorInfoEvent(message, info, sendParameters);

    public SerializableGameState GetSerializableGameState()
        => this.Host.GetSerializableGameState();

    public bool SetGameState(SerializableGameState state)
        => this.Host.SetGameState(state);

    public bool SetProperties(int actorNr, Hashtable properties, Hashtable expected, bool broadcast)
        => this.Host.SetProperties(actorNr, properties, expected, broadcast);

    public bool RemoveActor(int actorNr, string reasonDetail)
        => this.Host.RemoveActor(actorNr, reasonDetail);

    public bool RemoveActor(int actorNr, byte reason, string reasonDetail)
        => this.RemoveActor(actorNr, reason, reasonDetail);

    public bool TryRegisterType(Type type, byte typeCode, Func<object, byte[]> serializeFunction, Func<byte[], object> deserializeFunction)
        => this.TryRegisterType(type, typeCode, serializeFunction, deserializeFunction);

    public EnvironmentVersion GetEnvironmentVersion()
        => this.Host.GetEnvironmentVersion();

    public bool ExecuteCacheOperation(CacheOp operation, out string errorMsg)
        => this.Host.ExecuteCacheOperation(operation, out errorMsg);
}
