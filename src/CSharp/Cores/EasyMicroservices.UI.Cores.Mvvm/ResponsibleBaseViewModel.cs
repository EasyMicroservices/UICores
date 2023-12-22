using EasyMicroservices.UI.Cores.Interfaces;
using EasyMicroservices.UI.Cores.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TData"></typeparam>
public class ResponsibleBaseViewModel<TData> : PageBaseViewModel, IResponsibleViewModel
{
    TaskCompletionSource<TData> TaskCompletionSource { get; set; } = new TaskCompletionSource<TData>();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task<TData> WaitForResult()
    {
        return TaskCompletionSource.Task;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void SetData(TData data)
    {
        TaskCompletionSource.TrySetResult(data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetResult()
    {
        return await TaskCompletionSource.Task;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Close()
    {
        if (TaskCompletionSource.Task.IsCompleted)
            return;
        TaskCompletionSource.TrySetResult(default);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task PopAsync()
    {
        SetData(default);
        return NavigationManagerBase.Current.PopAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task PopDataAsync(TData data)
    {
        SetData(data);
        return NavigationManagerBase.Current.PopAsync();
    }
}
