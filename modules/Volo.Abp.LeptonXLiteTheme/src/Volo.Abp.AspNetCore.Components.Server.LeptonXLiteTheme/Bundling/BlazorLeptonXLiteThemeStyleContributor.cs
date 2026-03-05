using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Localization;

namespace Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme.Bundling;

public class BlazorLeptonXLiteThemeStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
       // chart.js, bootstrap-datepicker, bootstrap-icons, blazor-bundle (Blazorise), and
       // bootstrap-dim are intentionally omitted because this project uses MudBlazor,
       // which does not depend on Bootstrap or FontAwesome.

       var rtlPostfix = CultureHelper.IsRtl ? ".rtl" : string.Empty;
       
       context.Files.AddIfNotContains($"/_content/Volo.Abp.AspNetCore.Components.Web.LeptonXLiteTheme/side-menu/css/abp-bundle{rtlPostfix}.css");
       context.Files.AddIfNotContains($"/_content/Volo.Abp.AspNetCore.Components.Web.LeptonXLiteTheme/side-menu/css/layout-bundle{rtlPostfix}.css");
       context.Files.AddIfNotContains($"/_content/Volo.Abp.AspNetCore.Components.Web.LeptonXLiteTheme/side-menu/css/font-bundle{rtlPostfix}.css");
    }
}
