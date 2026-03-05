using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Emailing;
using Nblity.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Nblity.Abp.Account;

[DependsOn(
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpUiNavigationModule),
    typeof(AbpEmailingModule),
    typeof(AbpMapperlyModule)
)]
public class AbpAccountApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpAccountApplicationModule>();
        });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].Urls[AccountUrlNames.PasswordReset] = "Account/ResetPassword";
        });

        context.Services.AddMapperlyObjectMapper<AbpAccountApplicationModule>();
    }
}
