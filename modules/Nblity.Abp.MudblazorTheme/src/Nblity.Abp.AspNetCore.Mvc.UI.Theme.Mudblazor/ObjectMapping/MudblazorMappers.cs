using Riok.Mapperly.Abstractions;
using Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Menu;
using Volo.Abp.Mapperly;
using Volo.Abp.DependencyInjection;
using Volo.Abp.UI.Navigation;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.ObjectMapping;

[Mapper]
public partial class ApplicationMenuToMenuViewModelMapper : IAbpMapperlyMapper<ApplicationMenu, MenuViewModel>, IAbpMapperlyMapper<ApplicationMenuItem, MenuItemViewModel>, ITransientDependency
{
    [MapPropertyFromSource(nameof(MenuViewModel.Menu))]
    public partial MenuViewModel Map(ApplicationMenu source);
    
    [MapPropertyFromSource(nameof(MenuViewModel.Menu))]
    public partial void Map(ApplicationMenu source, MenuViewModel destination);
    
    [MapperIgnoreTarget(nameof(MenuItemViewModel.IsActive))]
    [MapPropertyFromSource(nameof(MenuItemViewModel.MenuItem))]
    public partial MenuItemViewModel Map(ApplicationMenuItem source);

    [MapperIgnoreTarget(nameof(MenuItemViewModel.IsActive))]
    [MapPropertyFromSource(nameof(MenuItemViewModel.MenuItem))]
    public partial void Map(ApplicationMenuItem source, MenuItemViewModel destination);

    public void BeforeMap(ApplicationMenu source)
    {
    }

    public void AfterMap(ApplicationMenu source, MenuViewModel destination)
    {
    }

    public void BeforeMap(ApplicationMenuItem source)
    {
    }

    public void AfterMap(ApplicationMenuItem source, MenuItemViewModel destination)
    {
    }
}
