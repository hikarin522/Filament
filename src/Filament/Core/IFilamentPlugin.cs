using System.Threading.Tasks;

using Photon.Hive.Plugin;

namespace Filament;

public interface IFilamentPlugin
{
    ValueTask OnCreateGame(ICreateGameCallInfo callInfo);

    ValueTask BeforeCloseGame(IBeforeCloseGameCallInfo callInfo);

    ValueTask BeforeJoin(IBeforeJoinGameCallInfo callInfo);

    ValueTask OnJoin(IJoinGameCallInfo callInfo);

    ValueTask OnLeave(ILeaveGameCallInfo callInfo);

    ValueTask OnRaiseEvent(IRaiseEventCallInfo callInfo);

    ValueTask BeforeSetProperties(IBeforeSetPropertiesCallInfo callInfo);

    ValueTask OnSetProperties(ISetPropertiesCallInfo callInfo);
}
