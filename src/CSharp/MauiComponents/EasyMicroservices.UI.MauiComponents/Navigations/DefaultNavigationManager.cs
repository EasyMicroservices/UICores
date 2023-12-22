﻿using EasyMicroservices.UI.Cores;
using EasyMicroservices.UI.Cores.Interfaces;
using System.Collections.Concurrent;

namespace EasyMicroservices.UI.MauiComponents.Navigations;
public class DefaultNavigationManager : NavigationManagerBase
{
    readonly INavigation _navigation;
    public DefaultNavigationManager(INavigation navigation)
    {
        _navigation = navigation;
    }

    ConcurrentDictionary<string, IPage> Pages { get; set; } = new ConcurrentDictionary<string, IPage>();
    public override Task PopAsync()
    {
        return _navigation.PopAsync();
    }

    public override Task<TResponseData> PushAsync<TResponseData>(string pageName, bool doClear = false)
    {
        return PushDataAsync<object, TResponseData>(default, pageName, doClear);
    }

    public override async Task<TResponseData> PushDataAsync<TData, TResponseData>(TData data, string pageName, bool doClear = false)
    {
        if (!Pages.TryGetValue(pageName, out IPage findPage))
            throw new Exception($"Page {pageName} not found, did you register it?");
        Page page = findPage as Page;
        if (page == null)
            throw new NotImplementedException($"Page {pageName} is not inherit Page!");
        var ipage = page as IPage;
        if (page.BindingContext is PageBaseViewModel pageBaseViewModel && ipage != null)
            pageBaseViewModel.Page = ipage;
        if (doClear)
            await _navigation.PopAsync();
        await _navigation.PushAsync(page);
        if (page.BindingContext is IPushPageViewModel pushViewModel)
            pushViewModel.SetResult(data);
        if (page.BindingContext is IResponsibleViewModel responsibleViewModel)
        {
            if (ipage != null)
                ipage.OnBackBottonPresssed = responsibleViewModel.Close;
            var result = await responsibleViewModel.GetResult();
            if (result != null && result is TResponseData responseData)
                return responseData;
            return default;
        }
        return default;
    }

    public override Task PushDataAsync<TData>(TData data, string pageName, bool doClear = false)
    {
        return PushDataAsync<TData, int>(data, pageName, doClear);
    }

    public override async Task PushAsync(string pageName, bool doClear = false)
    {
        await PushAsync<string>(pageName, doClear);
    }

    public override async Task<(bool IsSusccess, Stream Stream, string FileName, string ContentType)> PickFile()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions()
            {
                PickerTitle = "Please select a Word file",
            });
            if (result != null)
            {
                return (true, await result.OpenReadAsync(), result.FileName, result.ContentType);
            }
        }
        catch (Exception ex)
        {
            // The user canceled or something went wrong
        }

        return default;
    }

    public override Task<bool> OpenBrowser(string url)
    {
        return Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
    }
}