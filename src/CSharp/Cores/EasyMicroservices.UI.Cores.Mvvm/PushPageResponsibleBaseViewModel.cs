using EasyMicroservices.UI.Cores.Interfaces;

namespace EasyMicroservices.UI.Cores;
/// <summary>
/// 
/// </summary>
public abstract class PushPageResponsibleBaseViewModel<TRequestData, TResponseData> : ResponsibleBaseViewModel<TResponseData>, IPushPageViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public TRequestData Data { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public void SetResult(object data)
    {
        Data = (TRequestData)data;
        OnPropertyChanged(nameof(Data));
        OnDataInitialized(Data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public abstract void OnDataInitialized(TRequestData data);
}
