using EasyMicroservices.UI.Cores.Interfaces;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores.Navigations;
/// <summary>
/// 
/// </summary>
public abstract class NavigationManagerBase
{
    /// <summary>
    /// 
    /// </summary>
    public static NavigationManagerBase Current { get; set; }
    /// <summary>
    /// 
    /// </summary>
    protected ConcurrentDictionary<string, Type> Pages { get; set; } = new ConcurrentDictionary<string, Type>();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="pageName"></param>
    /// <param name="doClear"></param>
    /// <returns></returns>
    public abstract Task<TResponseData> PushAsync<TResponseData>(string pageName, bool doClear = false);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TResponseData"></typeparam>
    /// <param name="data"></param>
    /// <param name="pageName"></param>
    /// <param name="doClear"></param>
    /// <returns></returns>
    public abstract Task<TResponseData> PushDataAsync<TData, TResponseData>(TData data, string pageName, bool doClear = false);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="data"></param>
    /// <param name="pageName"></param>
    /// <param name="doClear"></param>
    /// <returns></returns>
    public abstract Task PushDataAsync<TData>(TData data, string pageName, bool doClear = false);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageName"></param>
    /// <param name="doClear"></param>
    /// <returns></returns>
    public abstract Task PushAsync(string pageName, bool doClear = false);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract Task PopAsync();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract Task<(bool IsSusccess, Stream Stream, string FileName, string ContentType)> PickFile();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public abstract Task<bool> OpenBrowser(string url);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageName"></param>
    public void RegisterPage<T>(string pageName)
        where T : IPage, new()
    {
        Pages.TryAdd(pageName, typeof(T));
    }
}
