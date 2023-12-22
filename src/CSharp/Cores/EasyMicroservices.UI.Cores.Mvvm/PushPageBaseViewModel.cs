using EasyMicroservices.UI.Cores.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMicroservices.UI.Cores;
/// <summary>
/// 
/// </summary>
/// <typeparam name="TData"></typeparam>
public abstract class PushPageBaseViewModel<TData> : PageBaseViewModel, IPushPageViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public TData Data { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void SetResult(object data)
    {
        Data = (TData)data;
        OnPropertyChanged(nameof(Data));
        OnDataInitialized(Data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public abstract void OnDataInitialized(TData data);
}