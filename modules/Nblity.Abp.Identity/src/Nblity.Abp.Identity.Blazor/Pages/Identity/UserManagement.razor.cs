using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.Authorization.Permissions;
using Nblity.Abp.Identity.Localization;
using Volo.Abp.ObjectExtending;
using Nblity.Abp.PermissionManagement.Blazor.Components;
using Volo.Abp.Users;
using Volo.Abp;

namespace Nblity.Abp.Identity.Blazor.Pages.Identity;

public partial class UserManagement
{
    protected const string PermissionProviderName = "U";

    protected PermissionManagementModal PermissionManagementModal;

    protected IReadOnlyList<IdentityRoleDto> Roles;

    protected AssignedRoleViewModel[] NewUserRoles;

    protected AssignedRoleViewModel[] EditUserRoles;

    protected string ManagePermissionsPolicyName;

    protected bool HasManagePermissionsPermission { get; set; }

    protected bool CreateDialogVisible;

    protected bool EditDialogVisible;

    protected bool ShowPassword;

    protected bool IsEditCurrentUser { get; set; }

    protected PageToolbar Toolbar { get; } = new();

    protected List<TableColumn> UserManagementTableColumns => TableColumns.Get<UserManagement>();

    [Inject]
    protected IPermissionChecker PermissionChecker { get; set; }

    public UserManagement()
    {
        ObjectMapperContext = typeof(AbpIdentityBlazorModule);
        LocalizationResource = typeof(IdentityResource);

        CreatePolicyName = IdentityPermissions.Users.Create;
        UpdatePolicyName = IdentityPermissions.Users.Update;
        DeletePolicyName = IdentityPermissions.Users.Delete;
        ManagePermissionsPolicyName = IdentityPermissions.Users.ManagePermissions;
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            Roles = (await AppService.GetAssignableRolesAsync()).Items;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected override ValueTask SetBreadcrumbItemsAsync()
    {
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(LUiNavigation["Menu:Administration"].Value));
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:IdentityManagement"].Value));
        BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Users"].Value));
        return base.SetBreadcrumbItemsAsync();
    }

    protected virtual async Task OnSearchTextChanged(string value)
    {
        GetListInput.Filter = value;
        CurrentPage = 1;
        await GetEntitiesAsync();
    }

    protected override async Task SetPermissionsAsync()
    {
        await base.SetPermissionsAsync();

        HasManagePermissionsPermission =
            await AuthorizationService.IsGrantedAsync(IdentityPermissions.Users.ManagePermissions);
    }

    protected override async Task OpenCreateModalAsync()
    {
        try
        {
            await CheckCreatePolicyAsync();

            NewUserRoles = Roles.Select(x => new AssignedRoleViewModel
            {
                Name = x.Name,
                IsAssigned = x.IsDefault
            }).ToArray();

            ShowPassword = false;
            NewEntity = new IdentityUserCreateDto();
            NewEntity.IsActive = true;
            NewEntity.LockoutEnabled = true;

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

    protected override Task OnCreatingEntityAsync()
    {
        NewEntity.RoleNames = NewUserRoles.Where(x => x.IsAssigned).Select(x => x.Name).ToArray();
        return base.OnCreatingEntityAsync();
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

    protected override async Task OpenEditModalAsync(IdentityUserDto entity)
    {
        try
        {
            await CheckUpdatePolicyAsync();

            IsEditCurrentUser = entity.Id == CurrentUser.Id;
            ShowPassword = false;

            if (await PermissionChecker.IsGrantedAsync(IdentityPermissions.Users.ManageRoles))
            {
                var userRoleIds = (await AppService.GetRolesAsync(entity.Id)).Items.Select(r => r.Id).ToList();

                EditUserRoles = Roles.Select(x => new AssignedRoleViewModel
                {
                    Name = x.Name,
                    IsAssigned = userRoleIds.Contains(x.Id)
                }).ToArray();
            }

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

    protected override Task OnUpdatingEntityAsync()
    {
        if (EditUserRoles != null)
        {
            EditingEntity.RoleNames = EditUserRoles.Where(x => x.IsAssigned).Select(x => x.Name).ToArray();
        }
        return base.OnUpdatingEntityAsync();
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

    protected override string GetDeleteConfirmationMessage(IdentityUserDto entity)
    {
        return string.Format(L["UserDeletionConfirmationMessage"], entity.UserName);
    }

    protected override ValueTask SetEntityActionsAsync()
    {
        EntityActions
            .Get<UserManagement>()
            .AddRange(new EntityAction[]
            {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => await OpenEditModalAsync(data.As<IdentityUserDto>())
                    },
                    new EntityAction
                    {
                        Text = L["Permissions"],
                        Visible = (data) => HasManagePermissionsPermission,
                        Clicked = async (data) =>
                        {
                            await PermissionManagementModal.OpenAsync(PermissionProviderName,
                                data.As<IdentityUserDto>().Id.ToString(),
                                data.As<IdentityUserDto>().UserName);
                        }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission && CurrentUser.GetId() != data.As<IdentityUserDto>().Id,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<IdentityUserDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<IdentityUserDto>())
                    }
            });

        return base.SetEntityActionsAsync();
    }

    protected override async ValueTask SetTableColumnsAsync()
    {
        UserManagementTableColumns
            .AddRange(new TableColumn[]
            {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<UserManagement>(),
                    },
                    new TableColumn
                    {
                        Title = L["UserName"],
                        Data = nameof(IdentityUserDto.UserName),
                        Sortable = true,
                    },
                    new TableColumn
                    {
                        Title = L["EmailAddress"],
                        Data = nameof(IdentityUserDto.Email),
                        Sortable = true,
                    },
                    new TableColumn
                    {
                        Title = L["PhoneNumber"],
                        Data = nameof(IdentityUserDto.PhoneNumber),
                        Sortable = true,
                    }
            });

        UserManagementTableColumns.AddRange(await GetExtensionTableColumnsAsync(IdentityModuleExtensionConsts.ModuleName,
            IdentityModuleExtensionConsts.EntityNames.User));
        await base.SetTableColumnsAsync();
    }

    protected override ValueTask SetToolbarItemsAsync()
    {
        Toolbar.AddButton(L["NewUser"],
            OpenCreateModalAsync,
            requiredPolicyName: CreatePolicyName);

        return base.SetToolbarItemsAsync();
    }

    protected virtual void TogglePasswordVisibility()
    {
        ShowPassword = !ShowPassword;
    }

    protected async Task OnPageChangedAsync(int newPage)
    {
        CurrentPage = newPage;
        await GetEntitiesAsync();
        await InvokeAsync(StateHasChanged);
    }
}

public class AssignedRoleViewModel
{
    public string Name { get; set; }

    public bool IsAssigned { get; set; }
}
