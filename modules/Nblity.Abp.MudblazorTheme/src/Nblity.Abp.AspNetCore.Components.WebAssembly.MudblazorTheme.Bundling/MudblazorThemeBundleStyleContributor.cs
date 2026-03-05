using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Nblity.Abp.AspNetCore.Components.WebAssembly.MudblazorTheme.Bundling;

public class MudblazorThemeBundleStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        // chart.js, bootstrap-datepicker, and bootstrap-icons are intentionally omitted
        // because this project uses MudBlazor, which does not depend on Bootstrap or FontAwesome.
    }
}
