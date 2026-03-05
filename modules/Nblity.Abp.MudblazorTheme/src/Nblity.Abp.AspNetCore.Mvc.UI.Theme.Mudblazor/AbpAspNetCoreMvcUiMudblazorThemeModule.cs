using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Bundling;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.ObjectMapping;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor;

[DependsOn(
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpMapperlyModule)
    )]
public class AbpAspNetCoreMvcUiMudblazorThemeModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAspNetCoreMvcUiMudblazorThemeModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<AbpAspNetCoreMvcUiMudblazorThemeModule>();

        Configure<AbpThemingOptions>(options =>
        {
            options.Themes.Add<MudblazorTheme>();

            if (options.DefaultThemeName == null)
            {
                options.DefaultThemeName = MudblazorTheme.Name;
            }
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAspNetCoreMvcUiMudblazorThemeModule>("Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor");
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new MudblazorThemeMainTopToolbarContributor());
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options
                .StyleBundles
                .Add(MudblazorThemeBundles.Styles.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Styles.Global)
                        .AddContributors(typeof(MudblazorThemeGlobalStyleContributor));
                });

            options
                .ScriptBundles
                .Add(MudblazorThemeBundles.Scripts.Global, bundle =>
                {
                    bundle
                        .AddBaseBundles(StandardBundles.Scripts.Global)
                        .AddContributors(typeof(MudblazorThemeGlobalScriptContributor));
                });
        });
    }
}
