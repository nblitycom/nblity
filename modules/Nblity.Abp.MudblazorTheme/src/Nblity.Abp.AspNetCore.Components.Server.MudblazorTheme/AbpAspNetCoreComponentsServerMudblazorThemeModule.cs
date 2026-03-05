using Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme.Bundling;
using Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.AspNetCore.Components.Server.Theming.Bundling;
using Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Components.Server.MudblazorTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebMudblazorThemeModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class AbpAspNetCoreComponentsServerMudblazorThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new MudblazorThemeBlazorServerToolbarContributor());
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(BlazorMudblazorThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                        .AddContributors(typeof(BlazorMudblazorThemeStyleContributor));
                });

            options
                .ScriptBundles
                .Add(BlazorMudblazorThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                        .AddContributors(typeof(BlazorMudblazorThemeScriptContributor));
                });
        });
    }
}
