using EasyMicroservices.ServiceContracts;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Core;

/// <summary>
/// 
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    bool _IsBusy;
    /// <summary>
    /// 
    /// </summary>
    public virtual bool IsBusy
    {
        get => _IsBusy;
        set
        {
            _IsBusy = value;
            OnPropertyChanged(nameof(IsBusy));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(propertyName, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Busy()
    {
        IsBusy = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void UnBusy()
    {
        IsBusy = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="getServerResult"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onError"></param>
    /// <returns></returns>
    public async virtual Task ExecuteApi<TResult>(Func<Task<TResult>> getServerResult, Func<TResult, Task> onSuccess, Func<Exception, Task> onError = default)
    {
        try
        {
            Busy();
            var result = await getServerResult();
            var response = result.ToContract<TResult>();

            if (response.IsSuccess)
                await onSuccess(response);
            else
                await DisplayFetchError(response.Error);
        }
        catch (Exception ex)
        {
            if (onError != null)
                await onError(ex);
            else
                await DisplayError(ex.ToString());
        }
        finally
        {
            UnBusy();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="getServerResult"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onError"></param>
    /// <returns></returns>
    public virtual Task ExecuteApi(Func<Task<ErrorContract>> getServerResult, Func<Task> onSuccess, Func<Exception, Task> onError = default)
    {
        return ExecuteApi(getServerResult, async (x) =>
        {
            await onSuccess();
        }, onError);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public virtual Task DisplayError(Exception exception)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorContract"></param>
    /// <returns></returns>
    public virtual Task DisplayFetchError(ErrorContract errorContract)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual Task DisplayError(string message)
    {
        return Task.CompletedTask;
    }
}
