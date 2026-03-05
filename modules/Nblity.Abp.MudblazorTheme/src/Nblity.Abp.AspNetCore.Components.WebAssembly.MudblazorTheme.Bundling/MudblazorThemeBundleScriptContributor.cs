using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.WebAssembly.MudblazorTheme.Bundling;

public class MudblazorThemeBundleScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        if (!context.Parameters.InteractiveAuto)
        {
            context.Files.AddIfNotContains("_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/libs/bootstrap/js/bootstrap.bundle.js");
        }

        context.Files.AddIfNotContains("_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/js/lepton-x.bundle.min.js");
        context.Files.AddIfNotContains("_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/libs/jquery/jquery.min.js");
        context.Files.AddIfNotContains("_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/side-menu/libs/bootstrap-datepicker/js/bootstrap-datepicker.min.js");
        context.Files.AddIfNotContains("_content/Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme/scripts/style-initializer.js");
        context.Files.AddIfNotContains("_content/Nblity.Abp.AspNetCore.Components.WebAssembly.MudblazorTheme/scripts/style-initializer.js");
    }
}
