﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Layout;
using Volo.Abp;

namespace Nblity.Abp.AspNetCore.Mvc.UI.Theme.Mudblazor.Themes.Mudblazor.Components.BreadCrumbs
{
    public class BreadCrumbsViewComponent : AbpViewComponent
    {
        protected IPageLayout PageLayout { get; }

        public BreadCrumbsViewComponent(IPageLayout pageLayout)
        {
            PageLayout = pageLayout;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Themes/Mudblazor/Components/BreadCrumbs/Default.cshtml", PageLayout.Content);
        }
    }
}
