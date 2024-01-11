using EasyMicroservices.UI.BlazorComponents;
using EasyMicroservices.UI.BlazorComponents.UITests;
using EasyMicroservices.UI.Cores;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

LoadLanguage("en-US");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<DialogBaseViewModel>();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
});
await builder.Build().RunAsync();
void LoadLanguage(string languageShortName)
{
    BaseViewModel.CurrentApplicationLanguage = languageShortName;
    BaseViewModel.AppendLanguage("Save", "Save");
    BaseViewModel.AppendLanguage("SaveDialog_Title", "Save");
    BaseViewModel.AppendLanguage("Saving", "Saving");
    BaseViewModel.AppendLanguage("Delete", "Delete");
    BaseViewModel.AppendLanguage("Delete_Title", "Delete");
    BaseViewModel.AppendLanguage("Deleting", "Deleting");
    BaseViewModel.AppendLanguage("Cancel", "Cancel");
    BaseViewModel.AppendLanguage("DeleteQuestion_Content", "Do you?");
}