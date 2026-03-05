using Volo.Abp;
﻿using System.Threading.Tasks;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Themes.Mudblazor.Toolbar;

public partial class MobileLanguageSwitchComponent
{
    public LanguageSwitchViewModel ViewModel { get; }

    public MobileLanguageSwitchComponent(LanguageSwitchViewModel viewModel)
    {
        ViewModel = viewModel;
    }
}