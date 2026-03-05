using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Nblity.Blazor;

public class NblityStyleBundleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.Add(new BundleFile("main.css", true));
    }

    public override void PostConfigureBundle(BundleConfigurationContext context)
    {
        // Remove all CSS files that are not needed when using MudBlazor,
        // including Blazorise, Bootstrap, FontAwesome, and other third-party CSS
        // injected by the default ABP bundling contributors.
        // Matches both server-side paths (/_content/...) and WebAssembly paths (_content/...).
        context.Files.RemoveAll(f =>
        {
            var fileName = f.FileName.TrimStart('/');

            // Remove Blazorise and ABP BlazoriseUI CSS
            if (fileName.StartsWith("_content/Blazorise", System.StringComparison.OrdinalIgnoreCase) ||
                fileName.StartsWith("_content/Volo.Abp.BlazoriseUI/", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Remove Bootstrap full CSS served from /libs/ (from ABP StandardBundles base)
            if (fileName.StartsWith("libs/bootstrap/", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Remove FontAwesome CSS (all.css and v4-shims.css)
            if (fileName.StartsWith("libs/@fortawesome/", System.StringComparison.OrdinalIgnoreCase) ||
                fileName.StartsWith("libs/fontawesome", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Remove Bootstrap Icons, Bootstrap Datepicker, and Chart.js CSS
            // bundled by BlazorLeptonXLiteThemeStyleContributor (not needed with MudBlazor)
            if (fileName.Contains("/bootstrap-icons/", System.StringComparison.OrdinalIgnoreCase) ||
                fileName.Contains("/bootstrap-datepicker/", System.StringComparison.OrdinalIgnoreCase) ||
                fileName.Contains("/chart.js/", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Remove Blazorise-specific bundle (b-table styles, etc.)
            if (fileName.Contains("/blazor-bundle", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Remove Bootstrap dimension/variable CSS (bootstrap-dim.css) –
            // MudBlazor supplies its own design tokens
            if (fileName.Contains("/bootstrap-dim", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        });
    }
}
