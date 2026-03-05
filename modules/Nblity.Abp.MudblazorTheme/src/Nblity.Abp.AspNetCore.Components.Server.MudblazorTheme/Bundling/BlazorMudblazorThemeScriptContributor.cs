using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme.Bundling;

public class BlazorMudblazorThemeScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/libs/bootstrap/js/bootstrap.bundle.js");
        context.Files.AddIfNotContains("/_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/js/lepton-x.bundle.min.js");
        context.Files.AddIfNotContains("/_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/libs/jquery/jquery.min.js");
        context.Files.AddIfNotContains("/_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/libs/bootstrap-datepicker/js/bootstrap-datepicker.min.js");
        context.Files.AddIfNotContains("/_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/scripts/style-initializer.js");
    }
}
