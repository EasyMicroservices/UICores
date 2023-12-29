using EasyMicroservices.Domain.Contracts.Common;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.UI.Cores.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMicroservices.UI.Cores;

/// <summary>
/// 
/// </summary>
public abstract class BaseViewModel : IBusyViewModel, INotifyPropertyChanged, IDisposable
{
    /// <summary>
    /// 
    /// </summary>
    public static Func<ErrorContract, Task<bool>> OnGlobalServiceErrorHandler { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public BaseViewModel()
    {
        PropertyChanged += BaseViewModel_PropertyChanged;
    }
    /// <summary>
    /// 
    /// </summary>
    public static string CurrentApplicationLanguage { get; set; } = "fa-IR";
    /// <summary>
    /// 
    /// </summary>
    public static bool IsRightToLeft { get; set; } = false;
    static ConcurrentDictionary<string, List<LanguageContract>> Languages { get; set; } = new ConcurrentDictionary<string, List<LanguageContract>>();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="languages"></param>
    public static void AppendLanguage(string key, params LanguageContract[] languages)
    {
        if (string.IsNullOrEmpty(key))
            throw new Exception($"Key cannot be empty!");
        if (languages == null || languages.Length == 0)
            throw new Exception($"languages cannot be empty!");
        if (languages.Any(x => string.IsNullOrEmpty(x.ShortName)))
            throw new Exception($"language ShortName null or empty detected!");

        if (Languages.TryGetValue(key, out List<LanguageContract> items))
        {
            items.AddRange(languages.Where(x => !items.Any(i => i.ShortName == x.ShortName)));
        }
        else
        {
            Languages[key] = new List<LanguageContract>(languages);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="languageShortName"></param>
    /// <returns></returns>
    public string GetLanguage(string key, string languageShortName)
    {
        if (Languages.TryGetValue(key, out List<LanguageContract> items))
        {
            var find = items.FirstOrDefault(x => x.ShortName.Equals(languageShortName, StringComparison.OrdinalIgnoreCase));
            if (find != null)
                return find.Value;
        }
        return $"Key {key} not found!";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetLanguage(string key)
    {
        return GetLanguage(key, CurrentApplicationLanguage);
    }

    /// <summary>
    /// 
    /// </summary>
    public Action<bool> OnBusyChanged { get; set; }
    Action _OnBindPropertyChanged;

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
            OnBusyChanged?.Invoke(value);
            OnPropertyChanged(nameof(IsBusy));
            OnPropertyChanged(nameof(IsNotBusy));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual bool IsNotBusy
    {
        get => !IsBusy;
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
    /// <param name="exception"></param>
    /// <returns></returns>
    public virtual Task OnError(Exception exception)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="errorContract"></param>
    /// <returns></returns>
    public virtual Task OnServerError(ErrorContract errorContract)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public virtual async Task OnServerErrorHandling(ErrorContract error)
    {
        if (OnGlobalServiceErrorHandler == null || !await OnGlobalServiceErrorHandler(error))
            await OnServerError(error);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="action"></param>
    public virtual void BindPropertyChanged(Action action)
    {
        _OnBindPropertyChanged = action;
    }

    private void BaseViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        _OnBindPropertyChanged?.Invoke();
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Dispose()
    {
        PropertyChanged -= BaseViewModel_PropertyChanged;
    }
}
