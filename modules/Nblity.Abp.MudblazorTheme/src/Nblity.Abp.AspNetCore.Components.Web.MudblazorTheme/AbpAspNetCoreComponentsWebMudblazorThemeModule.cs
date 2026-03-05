using Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Toolbars;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Layout;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.Web.Theming.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.Modularity;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebThemingModule)
    )]
public class AbpAspNetCoreComponentsWebMudblazorThemeModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureToolbarOptions();
        ConfigureRouterOptions();
        ConfigurePageHeaderOptions();
        ConfigureMudblazorTheme();
    }

    private void ConfigureToolbarOptions()
    {
        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new MudblazorThemeBlazorToolbarContributor());
        });
    }

    private void ConfigureRouterOptions()
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpAspNetCoreComponentsWebMudblazorThemeModule).Assembly);
        });
    }

    private void ConfigurePageHeaderOptions()
    {
        Configure<PageHeaderOptions>(options =>
        {
            options.RenderBreadcrumbs = false;
        });
    }
    
    private void ConfigureMudblazorTheme()
    {
        Configure<AbpThemingOptions>(options =>
        {
            options.Themes.Add<MudblazorTheme>();
            
            if (options.DefaultThemeName == null)
            {
                options.DefaultThemeName = MudblazorTheme.Name;
            }
        });
    }
}
