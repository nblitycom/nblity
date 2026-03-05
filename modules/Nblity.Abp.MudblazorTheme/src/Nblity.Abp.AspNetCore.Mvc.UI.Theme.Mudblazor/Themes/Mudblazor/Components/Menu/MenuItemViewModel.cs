using System.Collections.Generic;
using Volo.Abp.UI.Navigation;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.Menu;
public class MenuItemViewModel
{
	public ApplicationMenuItem MenuItem { get; set; }

	public IList<MenuItemViewModel> Items { get; set; }

	public bool IsActive { get; set; }
}
