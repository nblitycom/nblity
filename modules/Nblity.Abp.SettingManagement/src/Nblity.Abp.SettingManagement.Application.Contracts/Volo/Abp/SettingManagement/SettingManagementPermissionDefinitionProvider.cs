using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Nblity.Abp.SettingManagement.Localization;
using Volo.Abp;

namespace Nblity.Abp.SettingManagement;

public class SettingManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var moduleGroup = context.AddGroup(SettingManagementPermissions.GroupName, L("Permission:SettingManagement"));

        var emailPermission = moduleGroup
            .AddPermission(SettingManagementPermissions.Emailing, L("Permission:Emailing"));
        emailPermission.StateCheckers.Add(new AllowChangingEmailSettingsFeatureSimpleStateChecker());

        emailPermission.AddChild(SettingManagementPermissions.EmailingTest, L("Permission:EmailingTest"));

        moduleGroup.AddPermission(SettingManagementPermissions.TimeZone, L("Permission:TimeZone"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpSettingManagementResource>(name);
    }
}
