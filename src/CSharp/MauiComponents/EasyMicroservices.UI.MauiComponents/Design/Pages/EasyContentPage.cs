using EasyMicroservices.UI.Cores.Interfaces;

namespace EasyMicroservices.UI.MauiComponents.Design.Pages;

public class EasyContentPage : ContentPage, IPage
{
    public EasyContentPage()
    {
        Loaded += CustomContentPage_Loaded;
    }

    private void CustomContentPage_Loaded(object sender, EventArgs e)
    {
        OnLoadComplete?.Invoke();
    }

    public Func<bool> OnBackButtonPressedAction { get; set; }

    public Action OnLoadComplete { get; set; }

    protected override bool OnBackButtonPressed()
    {
        if (OnBackButtonPressedAction is not null)
            return OnBackButtonPressedAction();
        return base.OnBackButtonPressed();
    }

    public async Task<string> DisplayPrompt(string title, string message)
    {
        return await DisplayPromptAsync(title, message);
    }
}