using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Nblity.Abp.Identity.Localization;
using Nblity.Abp.PermissionManagement.Blazor.Components;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Data;
using Volo.Abp;

namespace Nblity.Abp.Identity.Blazor.Pages.Identity;

public partial class RoleManagement
{
    protected const string PermissionProviderName = "R";

    protected PermissionManagementModal PermissionManagementModal;

    protected string ManagePermissionsPolicyName;

    protected bool HasManagePermissionsPermission { get; set; }

    protected bool CreateDialogVisible;

    protected bool EditDialogVisible;

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> RoleManagementTableColumns => TableColumns.Get<RoleManagement>();

    public RoleManagement()
    {
        ObjectMapperContext = typeof(AbpIdentityBlazorModule);
        LocalizationResource = typeof(IdentityResource);

        CreatePolicyName = IdentityPermissions.Roles.Create;
        UpdatePolicyName = IdentityPermissions.Roles.Update;
        DeletePolicyName = IdentityPermissions.Roles.Delete;
        ManagePermissionsPolicyName = IdentityPermissions.Roles.ManagePermissions;
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(LUiNavigation["Menu:Administration"].Value));
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:IdentityManagement"].Value));
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Roles"].Value));
        return base.SetBreadcrumbItemsAsync();
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<RoleManagement>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => { await OpenEditModalAsync(data.As<IdentityRoleDto>()); }
                    },
                    new EntityAction
                    {
                        Text = L["Permissions"],
                        Visible = (data) => HasManagePermissionsPermission,
                        Clicked = async (data) =>
                        {
                            await PermissionManagementModal.OpenAsync(PermissionProviderName,
                                data.As<IdentityRoleDto>().Name);
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission && !data.As<IdentityRoleDto>().IsStatic,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<IdentityRoleDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<IdentityRoleDto>())
                    }
            });

        return base.SetEntityActionsAsync();
    }

    protected override async ValueTask SetTableColumnsAsync()
    {
        RoleManagementTableColumns
            .AddRange(new TableColumn[]
            {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<RoleManagement>(),
                    },
                    new TableColumn
                    {
                        Title = L["RoleName"],
                        Sortable = true,
                        Data = nameof(IdentityRoleDto.Name),
                        Component = typeof(RoleNameComponent)
                    },
            });

        RoleManagementTableColumns.AddRange(await GetExtensionTableColumnsAsync(IdentityModuleExtensionConsts.ModuleName,
            IdentityModuleExtensionConsts.EntityNames.Role));

        await base.SetTableColumnsAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManagePermissionsPermission =
            await AuthorizationService.IsGrantedAsync(IdentityPermissions.Roles.ManagePermissions);
    }

    protected override string GetDeleteConfirmationMessage(IdentityRoleDto entity)
    {
        return string.Format(L["RoleDeletionConfirmationMessage"], entity.Name);
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["NewRole"],
            OpenCreateModalAsync,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }

    protected override async Task OpenCreateModalAsync()
    {
        try
        {
            await CheckCreatePolicyAsync();
            NewEntity = new IdentityRoleCreateDto();
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

    protected override async Task OpenEditModalAsync(IdentityRoleDto entity)
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
        CurrentPage = newPage;
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
    }
}
