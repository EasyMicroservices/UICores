using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores;
/// <summary>
/// 
/// </summary>
public class PageBaseViewModel : ApiBaseViewModel
{
    IPage _Page;
    /// <summary>
    /// 
    /// </summary>
    public IPage Page
    {
        get
        {
            return _Page;
        }
        set
        {
            _Page = value;
            Page.OnLoadComplete = OnLoadComplete;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void OnLoadComplete()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancel"></param>
    /// <param name="destruction"></param>
    /// <param name="buttons"></param>
    /// <returns></returns>
    public virtual async Task<(bool IsSelected, string SelectedItem)> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
    {
        var res = await Page.DisplayActionSheet(title, cancel, destruction, buttons);
        if (buttons != null && buttons.Contains(res))
            return (true, res);
        return (false, res);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="title"></param>
    /// <param name="skipItems"></param>
    /// <returns></returns>
    public virtual async Task<(bool IsSelected, TEnum SelectedItem)> DisplayEnumActionSheet<TEnum>(string title, params string[] skipItems)
        where TEnum : struct, Enum
    {
        var unusedEnums = new string[]
        {
                "None","Default","All","Other","Unknown","Nothing"
        };
        if (skipItems != null)
            unusedEnums = unusedEnums.Concat(skipItems).ToArray();
        var items = Enum.GetNames(typeof(TEnum));
        items = items.Where(x => !unusedEnums.Contains(x)).ToArray();
        var res = await Page.DisplayActionSheet(title, "Close", null, items);
        if (items.Contains(res))
            return (true, (TEnum)Enum.Parse(typeof(TEnum),res));
        return (false, default);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public virtual Task DisplayAlert(string title, string message, string cancel)
    {
        return Page.DisplayAlert(title, message, cancel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="accept"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public virtual Task<bool> DisplayQuestion(string title, string message, string accept = "Yes", string cancel = "No")
    {
        return Page.DisplayAlert(title, message, accept, cancel);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual Task<string> DisplayPrompt(string title, string message)
    {
        return Page.DisplayPrompt(title, message);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public override Task DisplayError(string message)
    {
        return Page.DisplayAlert("Error", message, "Ok");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnError(Exception exception)
    {
        return DisplayError(exception.ToString());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorContract"></param>
    /// <returns></returns>
    public override Task DisplayServerError(ErrorContract errorContract)
    {
        return Page.DisplayAlert("Server Error", $"{errorContract?.FailedReasonType}{Environment.NewLine}{errorContract?.Message}{Environment.NewLine}{errorContract?.Details}{Environment.NewLine}{errorContract?.StackTrace}", "Ok");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public virtual Task DisplayValidationAlert(string message)
    {
        return Page.DisplayAlert("Data is not valid", message, "Ok");
    }
}
