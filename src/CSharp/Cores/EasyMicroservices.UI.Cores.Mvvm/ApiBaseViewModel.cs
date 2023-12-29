using EasyMicroservices.ServiceContracts;
using System;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores;

/// <summary>
/// 
/// </summary>
public class ApiBaseViewModel : BaseViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public static Func<ErrorContract, Task<bool>> OnGlobalServiceErrorHandler { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="getServerResult"></param>
    /// <param name="onSuccess"></param>
    /// <param name="onError"></param>
    /// <returns></returns>
    public async virtual Task ExecuteApi<TResult>(Func<Task<TResult>> getServerResult, Func<MessageContract<TResult>, Task> onSuccess, Func<ErrorContract, Task> onError = default)
    {
        try
        {
            Busy();
            var result = await getServerResult();

            var response = result.ToContract<TResult>();

            if (response.IsSuccess)
                await onSuccess(response);
            else
                await InternalDisplayServerError(response.Error);
        }
        catch (Exception ex)
        {
            if (onError != null)
                await onError(ex);
            else
                await OnError(ex);
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
    public virtual async Task ExecuteApi(Func<Task<object>> getServerResult, Func<Task> onSuccess = default, Func<ErrorContract, Task> onError = default)
    {
        try
        {
            Busy();
            var result = await getServerResult();

            var response = result.ToContract();

            if (response.IsSuccess)
                await onSuccess?.Invoke();
            else
                await InternalDisplayServerError(response.Error);
        }
        catch (Exception ex)
        {
            if (onError != null)
                await onError(ex);
            else
                await OnError(ex);
        }
        finally
        {
            UnBusy();
        }
    }

    async Task InternalDisplayServerError(ErrorContract error)
    {
        if (OnGlobalServiceErrorHandler == null || !await OnGlobalServiceErrorHandler(error))
            await OnServerError(error);
    }
}
