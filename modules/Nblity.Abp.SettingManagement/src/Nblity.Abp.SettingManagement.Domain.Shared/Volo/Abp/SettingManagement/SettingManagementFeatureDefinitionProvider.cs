using Volo.Abp.Features;
using Volo.Abp.Localization;
using Nblity.Abp.SettingManagement.Localization;
using Volo.Abp.Validation.StringValues;

namespace Nblity.Abp.SettingManagement;

public class SettingManagementFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {
        var group = context.AddGroup(SettingManagementFeatures.GroupName,
            L("Feature:SettingManagementGroup"));

        var settingEnableFeature = group.AddFeature(
            SettingManagementFeatures.Enable,
            "true",
            L("Feature:SettingManagementEnable"),
            L("Feature:SettingManagementEnableDescription"),
            new ToggleStringValueType(),
            isAvailableToHost: true);

        settingEnableFeature.CreateChild(
            SettingManagementFeatures.AllowChangingEmailSettings,
            "false",
            L("Feature:AllowChangingEmailSettings"),
            null,
            new ToggleStringValueType(),
            isAvailableToHost: true);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpSettingManagementResource>(name);
    }
}
