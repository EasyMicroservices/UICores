using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores.Interfaces;
/// <summary>
/// 
/// </summary>
public interface IPage
{
    /// <summary>
    /// 
    /// </summary>
    Action OnLoadComplete { get; set; }
    /// <summary>
    /// 
    /// </summary>
    Func<bool> OnBackButtonPressedAction { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancel"></param>
    /// <param name="destruction"></param>
    /// <param name="buttons"></param>
    /// <returns></returns>
    Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DisplayAlert(string title, string message, string cancel);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="accept"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task<string> DisplayPrompt(string title, string message);
}
