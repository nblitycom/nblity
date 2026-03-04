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
        // Remove Blazorise CSS files that are injected by the default ABP bundling contributor
        // since this project uses MudBlazor instead of Blazorise.
        // Matches both server-side paths (/_content/...) and WebAssembly paths (_content/...).
        context.Files.RemoveAll(f =>
            f.FileName.TrimStart('/').StartsWith("_content/Blazorise", System.StringComparison.OrdinalIgnoreCase) ||
            f.FileName.TrimStart('/').StartsWith("_content/Volo.Abp.BlazoriseUI/", System.StringComparison.OrdinalIgnoreCase));
    }
}
