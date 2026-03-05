using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Bundling;

public class MudblazorThemeGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        // All custom CSS removed – UI is 100% MudBlazor.
        // Remove any CSS files added by other contributors.
        context.Files.RemoveAll(x => x.FileName.EndsWith(".css", System.StringComparison.OrdinalIgnoreCase));
    }
}
