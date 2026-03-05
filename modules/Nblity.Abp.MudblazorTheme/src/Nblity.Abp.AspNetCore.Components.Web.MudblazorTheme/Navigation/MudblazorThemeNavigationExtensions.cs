using JetBrains.Annotations;
using System;
using Volo.Abp.UI.Navigation;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Navigation;
public static class MudblazorThemeNavigationExtensions
{
    public const string CustomDataComponentKey = "MudblazorTheme.CustomComponent";

    public static ApplicationMenuItem UseComponent(this ApplicationMenuItem applicationMenuItem, Type componentType)
    {
        return applicationMenuItem.WithCustomData(CustomDataComponentKey, componentType);
    }

    [CanBeNull]
    public static Type GetComponentTypeOrDefault(this ApplicationMenuItem applicationMenuItem)
    {
        if (applicationMenuItem.CustomData.TryGetValue(CustomDataComponentKey, out object componentType))
        {
            return componentType as Type;
        }

        return default;
    }
}
