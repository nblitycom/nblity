using Microsoft.Extensions.DependencyInjection;
using Nblity.Abp.Account.Localization;
using Nblity.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using Nblity.Abp.Account.Web.ProfileManagement;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.Mapperly;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http.ProxyScripting.Generators.JQuery;
using Nblity.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;

namespace Nblity.Abp.Account.Web;

[DependsOn(
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpIdentityAspNetCoreModule),
    typeof(AbpMapperlyModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpExceptionHandlingModule)
    )]
public class AbpAccountWebModule : AbpModule
{
    private readonly static OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(AccountResource), 
                typeof(AbpAccountWebModule).Assembly, 
                typeof(AbpAccountApplicationContractsModule).Assembly
            );
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpAccountWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAccountWebModule>();
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new AbpAccountUserMenuContributor());
        });

        Configure<ProfileManagementPageOptions>(options =>
        {
            options.Contributors.Add(new AccountProfileManagementPageContributor());
        });

        context.Services.AddMapperlyObjectMapper<AbpAccountWebModule>();

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(AccountRemoteServiceConsts.ModuleName);
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper
                .ApplyEntityConfigurationToUi(
                    IdentityModuleExtensionConsts.ModuleName,
                    IdentityModuleExtensionConsts.EntityNames.User,
                    editFormTypes: new[] { typeof(AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel) }
                );
        });
    }
}
