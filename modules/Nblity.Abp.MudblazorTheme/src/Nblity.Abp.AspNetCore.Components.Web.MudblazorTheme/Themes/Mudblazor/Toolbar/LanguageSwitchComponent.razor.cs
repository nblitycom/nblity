using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Themes.Mudblazor.Toolbar;

public partial class LanguageSwitchComponent
{
    [Inject] public LanguageSwitchViewModel ViewModel { get; set; }

    protected override Task OnInitializedAsync()
    {
        ViewModel.OnInitialized += (s, e) => StateHasChanged();

        return Task.CompletedTask;
    }
}
