using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Nblity.Abp.PermissionManagement.Localization;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

[DependsOn(
    typeof(AbpValidationModule)
    )]
public class AbpPermissionManagementDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpPermissionManagementDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpPermissionManagementResource>("en")
                .AddBaseTypes(
                    typeof(AbpValidationResource)
                ).AddVirtualJson("/Volo/Abp/PermissionManagement/Localization/Domain");
        });
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        // Remove any IConfigureOptions<AbpLocalizationOptions> registrations from other modules
        // (e.g., the original Volo.Abp.PermissionManagement.Domain.Shared loaded transitively) that
        // try to register a localization resource under a name already registered by this module.
        var services = context.Services;
        var descriptors = services
            .Where(d => d.ServiceType == typeof(IConfigureOptions<AbpLocalizationOptions>))
            .ToList();

        var testOptions = new AbpLocalizationOptions();
        var toRemove = new List<ServiceDescriptor>();

        foreach (var descriptor in descriptors)
        {
            var configurer = descriptor.ImplementationInstance as IConfigureOptions<AbpLocalizationOptions>;
            if (configurer == null) continue;

            try
            {
                configurer.Configure(testOptions);
            }
            catch (AbpException ex) when (ex.Message.Contains("already added before"))
            {
                toRemove.Add(descriptor);
            }
        }

        foreach (var descriptor in toRemove)
            services.Remove(descriptor);
    }
}
