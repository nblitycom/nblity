using Microsoft.Extensions.DependencyInjection;
using Nblity.Abp.FeatureManagement.JsonConverters;
using Nblity.Abp.FeatureManagement.Localization;
using Volo.Abp.Json.SystemTextJson;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Nblity.Abp.FeatureManagement;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpJsonSystemTextJsonModule)
)]
public class AbpFeatureManagementDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpFeatureManagementDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<AbpFeatureManagementResource>("en")
                .AddBaseTypes(
                    typeof(AbpValidationResource)
                ).AddVirtualJson("Volo/Abp/FeatureManagement/Localization/Domain");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("Nblity.Abp.FeatureManagement", typeof(AbpFeatureManagementResource));
        });

        var valueValidatorFactoryOptions = context.Services.GetPreConfigureActions<ValueValidatorFactoryOptions>();
        Configure<ValueValidatorFactoryOptions>(options =>
        {
            valueValidatorFactoryOptions.Configure(options);
        });

        Configure<AbpSystemTextJsonSerializerOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new StringValueTypeJsonConverter(valueValidatorFactoryOptions.Configure()));
        });
    }
}
