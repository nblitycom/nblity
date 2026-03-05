using System;
using Volo.Abp.Bundling;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.WebAssembly.MudblazorTheme;

[Obsolete("This class is obsolete and will be removed in the future versions. Use GlobalAssets instead.")]
public class MudblazorThemeBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {
    }

    public void AddStyles(BundleContext context)
    {
        // All custom CSS removed – UI is 100% MudBlazor.
    }
}
