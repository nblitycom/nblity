using Volo.Abp;
﻿using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme.Bundling;

public class BlazorMudblazorThemeStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
       // All custom CSS removed – UI is 100% MudBlazor.
       // MudBlazor.min.css is loaded directly in App.razor.
    }
}
