﻿using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Alerts;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.PageAlerts;

public class PageAlertsViewComponent : AbpViewComponent
{
    protected IAlertManager AlertManager { get; }

    public PageAlertsViewComponent(IAlertManager alertManager)
    {
        AlertManager = alertManager;
    }

    public virtual IViewComponentResult Invoke(string name)
    {
        return View("~/Themes/Mudblazor/Components/PageAlerts/Default.cshtml", AlertManager.Alerts);
    }
}
