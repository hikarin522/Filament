using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using Photon.Hive.Plugin;

namespace Filament;

public struct HttpRequest
{
    public string Accept;

    public string ContentType;

    public MemoryStream DataStream;

    public IDictionary<HttpRequestHeader, string> Headers;

    public IDictionary<string, string> CustomHeaders;

    public string Method;

    public string Url;
}

public interface IHttpClient
{
    [Obsolete]
    void HttpRequest(in Photon.Hive.Plugin.HttpRequest request);

    public Task<IHttpResponse> HttpRequestAsync(in HttpRequest request, ICallInfo callInfo);
}

internal class HttpClient: IHttpClient
{
    protected Photon.Hive.Plugin.IPluginHost PluginHost { get; }

    public HttpClient(Photon.Hive.Plugin.IPluginHost pluginHost)
    {
        this.PluginHost = pluginHost;
    }

    [Obsolete]
    public void HttpRequest(in Photon.Hive.Plugin.HttpRequest request)
        => this.PluginHost.HttpRequest(request);

    public Task<IHttpResponse> HttpRequestAsync(in HttpRequest request, ICallInfo callInfo)
    {
        var tcs = new TaskCompletionSource<IHttpResponse>();

        this.PluginHost.HttpRequest(new() {
            Accept = request.Accept,
            Callback = static (response, userState) => {
                ((TaskCompletionSource<IHttpResponse>)userState).SetResult(response);
            },
            ContentType = request.ContentType,
            DataStream = request.DataStream,
            Headers = request.Headers,
            CustomHeaders = request.CustomHeaders,
            Method = request.Method,
            Url = request.Url,
            UserState = tcs,
            Async = callInfo is IRaiseEventCallInfo or IBeforeSetPropertiesCallInfo,
        }, callInfo);

        return tcs.Task;
    }
}
