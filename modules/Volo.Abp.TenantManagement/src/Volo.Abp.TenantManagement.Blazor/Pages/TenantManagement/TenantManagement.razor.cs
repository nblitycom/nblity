using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.FeatureManagement.Blazor.Components;
using Volo.Abp.ObjectExtending;
using Volo.Abp.TenantManagement.Localization;

namespace Volo.Abp.TenantManagement.Blazor.Pages.TenantManagement;

public partial class TenantManagement
{
    protected const string FeatureProviderName = "T";

    protected bool HasManageFeaturesPermission;
    protected string ManageFeaturesPolicyName;

    protected FeatureManagementModal FeatureManagementModal;

    protected bool ShowPassword { get; set; }

    protected bool CreateDialogVisible;

    protected bool EditDialogVisible;

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> TenantManagementTableColumns => TableColumns.Get<TenantManagement>();

    public TenantManagement()
    {
        LocalizationResource = typeof(AbpTenantManagementResource);
        ObjectMapperContext = typeof(AbpTenantManagementBlazorModule);

        CreatePolicyName = TenantManagementPermissions.Tenants.Create;
        UpdatePolicyName = TenantManagementPermissions.Tenants.Update;
        DeletePolicyName = TenantManagementPermissions.Tenants.Delete;

        ManageFeaturesPolicyName = TenantManagementPermissions.Tenants.ManageFeatures;
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new BlazoriseUI.BreadcrumbItem(LUiNavigation["Menu:Administration"]));
        BreadcrumbItems.Add(new BlazoriseUI.BreadcrumbItem(L["Menu:TenantManagement"]));
        BreadcrumbItems.Add(new BlazoriseUI.BreadcrumbItem(L["Tenants"]));
        return base.SetBreadcrumbItemsAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManageFeaturesPermission = await AuthorizationService.IsGrantedAsync(ManageFeaturesPolicyName);
    }

    protected override string GetDeleteConfirmationMessage(TenantDto entity)
    {
        return string.Format(L["TenantDeletionConfirmationMessage"], entity.Name);
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["NewTenant"],
            OpenCreateModalAsync,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<TenantManagement>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => { await OpenEditModalAsync(data.As<TenantDto>()); }
                    },
                    new EntityAction
                    {
                        Text = L["Features"],
                        Visible = (data) => HasManageFeaturesPermission,
                        Clicked = async (data) =>
                        {
                            var tenant = data.As<TenantDto>();
                            await FeatureManagementModal.OpenAsync(FeatureProviderName, tenant.Id.ToString(), tenant.Name);
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<TenantDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<TenantDto>())
                    }
            });

        return base.SetEntityActionsAsync();
    }

    protected override async ValueTask SetTableColumnsAsync()
    {
        TenantManagementTableColumns
            .AddRange(new TableColumn[]
            {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<TenantManagement>(),
                    },
                    new TableColumn
                    {
                        Title = L["TenantName"],
                        Sortable = true,
                        Data = nameof(TenantDto.Name),
                    },
            });

        TenantManagementTableColumns.AddRange(await GetExtensionTableColumnsAsync(
            TenantManagementModuleExtensionConsts.ModuleName,
            TenantManagementModuleExtensionConsts.EntityNames.Tenant));

        await base.SetTableColumnsAsync();
    }

    protected virtual void TogglePasswordVisibility()
    {
        ShowPassword = !ShowPassword;
    }

    protected override async Task OpenCreateModalAsync()
    {
        try
        {
            await CheckCreatePolicyAsync();
            NewEntity = new TenantCreateDto();
            ShowPassword = false;
            CreateDialogVisible = true;
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override Task CloseCreateModalAsync()
    {
        CreateDialogVisible = false;
        return Task.CompletedTask;
    }

    protected override async Task OpenEditModalAsync(TenantDto entity)
    {
        try
        {
            await CheckUpdatePolicyAsync();
            var entityDto = await AppService.GetAsync(entity.Id);
            EditingEntityId = entity.Id;
            EditingEntity = MapToEditingEntity(entityDto);
            EditDialogVisible = true;
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override Task CloseEditModalAsync()
    {
        EditDialogVisible = false;
        return Task.CompletedTask;
    }

    protected override async Task CreateEntityAsync()
    {
        try
        {
            await OnCreatingEntityAsync();
            await CheckCreatePolicyAsync();
            await AppService.CreateAsync(NewEntity);
            await OnCreatedEntityAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override async Task OnCreatedEntityAsync()
    {
        CreateDialogVisible = false;
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task UpdateEntityAsync()
    {
        try
        {
            await OnUpdatingEntityAsync();
            await CheckUpdatePolicyAsync();
            await AppService.UpdateAsync(EditingEntityId, EditingEntity);
            await OnUpdatedEntityAsync();
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override async Task OnUpdatedEntityAsync()
    {
        EditDialogVisible = false;
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnPageChangedAsync(int newPage)
    {
        // MudPagination is 1-based; CurrentPage is 0-based
        CurrentPage = newPage - 1;
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
    }
}
