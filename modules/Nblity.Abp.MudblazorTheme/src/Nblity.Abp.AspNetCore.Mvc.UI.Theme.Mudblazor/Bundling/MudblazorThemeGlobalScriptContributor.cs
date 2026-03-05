using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Bundling;

public class MudblazorThemeGlobalScriptContributor : BundleContributor
{
    private const string RootPath = "/Themes/Mudblazor/Global";
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add($"{RootPath}/side-menu/js/lepton-x.bundle.min.js");
        context.Files.Add($"{RootPath}/scripts/leptonx-mvc-ui-initializer.js");
    }
}
