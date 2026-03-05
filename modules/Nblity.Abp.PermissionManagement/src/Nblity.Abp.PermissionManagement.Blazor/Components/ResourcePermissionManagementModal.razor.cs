using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Volo.Abp.AspNetCore.Components.Messages;
using Nblity.Abp.PermissionManagement.Localization;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement.Blazor.Components;

public partial class ResourcePermissionManagementModal
{
    [Inject] protected IPermissionAppService PermissionAppService { get; set; }

    [Inject] protected IUiMessageService UiMessageService { get; set; }

    protected bool _mainVisible;
    protected int _mainTableCurrentPage;

    public bool HasAnyResourcePermission { get; set; }
    public bool HasAnyResourceProviderKeyLookupService { get; set; }
    protected string ResourceName { get; set; }
    protected string ResourceKey { get; set; }
    protected string ResourceDisplayName { get; set; }
    protected int PageSize { get; set; } = 10;

    protected bool _createVisible;
    protected EditContext _createEditContext;
    protected CreateModel CreateEntity { get; set; } = new CreateModel
    {
        Permissions = []
    };
    protected SearchProviderKeyInfo _selectedProviderKeyInfo;
    protected bool _showProviderKeyValidationError;
    public GetResourcePermissionDefinitionListResultDto ResourcePermissionDefinitions { get; set; } = new()
    {
        Permissions = []
    };
    protected string CurrentLookupService { get; set; }
    protected string ProviderKey { get; set; }
    protected string ProviderDisplayName { get; set; }
    protected List<ResourceProviderDto> ResourceProviderKeyLookupServices { get; set; } = new();
    protected List<SearchProviderKeyInfo> ProviderKeys { get; set; } = new();
    protected GetResourcePermissionListResultDto ResourcePermissionList = new()
    {
        Permissions = []
    };

    protected bool _editVisible;
    protected EditContext _editEditContext;
    protected EditModel EditEntity { get; set; } = new EditModel
    {
        Permissions = []
    };

    public ResourcePermissionManagementModal()
    {
        LocalizationResource = typeof(AbpPermissionManagementResource);
    }

    public virtual async Task OpenAsync(string resourceName, string resourceKey, string resourceDisplayName)
    {
        try
        {
            ResourceName = resourceName;
            ResourceKey = resourceKey;
            ResourceDisplayName = resourceDisplayName;

            ResourcePermissionDefinitions = await PermissionAppService.GetResourceDefinitionsAsync(ResourceName);
            ResourceProviderKeyLookupServices = (await PermissionAppService.GetResourceProviderKeyLookupServicesAsync(ResourceName)).Providers;

            HasAnyResourcePermission = ResourcePermissionDefinitions.Permissions.Any();
            if (HasAnyResourcePermission)
            {
                HasAnyResourceProviderKeyLookupService = ResourceProviderKeyLookupServices.Count > 0;
            }

            await InvokeAsync(StateHasChanged);

            ResourcePermissionList = await PermissionAppService.GetResourceAsync(ResourceName, ResourceKey);

            _mainVisible = true;
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    protected virtual async Task CloseModal()
    {
        _mainVisible = false;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OpenCreateModalAsync()
    {
        CurrentLookupService = ResourceProviderKeyLookupServices.FirstOrDefault()?.Name;

        ProviderKey = null;
        ProviderDisplayName = null;
        ProviderKeys = new List<SearchProviderKeyInfo>();
        _selectedProviderKeyInfo = null;
        _showProviderKeyValidationError = false;

        CreateEntity = new CreateModel
        {
            Permissions = ResourcePermissionDefinitions.Permissions.Select(x => new ResourcePermissionModel
            {
                Name = x.Name,
                DisplayName = x.DisplayName,
                IsGranted = false
            }).ToList()
        };

        _createEditContext = new EditContext(CreateEntity);

        _createVisible = true;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OnProviderKeyInfoSelectedAsync(SearchProviderKeyInfo value)
    {
        _selectedProviderKeyInfo = value;
        if (value != null)
        {
            await SelectedProviderKeyAsync(value.ProviderKey);
        }
        else
        {
            ProviderKey = null;
            ProviderDisplayName = null;
        }
    }

    protected virtual async Task SelectedProviderKeyAsync(string value)
    {
        ProviderKey = value;
        ProviderDisplayName = ProviderKeys.FirstOrDefault(p => p.ProviderKey == value)?.ProviderDisplayName;

        var permissionGrants = await PermissionAppService.GetResourceByProviderAsync(ResourceName, ResourceKey, CurrentLookupService, ProviderKey);
        foreach (var permission in CreateEntity.Permissions)
        {
            permission.IsGranted = permissionGrants.Permissions.Any(p => p.Name == permission.Name && p.Providers.Contains(CurrentLookupService) && p.IsGranted);
        }

        _showProviderKeyValidationError = false;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task<IEnumerable<SearchProviderKeyInfo>> SearchProviderKeyAsync(string searchText, System.Threading.CancellationToken cancellationToken)
    {
        if (searchText.IsNullOrWhiteSpace())
        {
            ProviderKeys = new List<SearchProviderKeyInfo>();
            return ProviderKeys;
        }

        ProviderKeys = (await PermissionAppService.SearchResourceProviderKeyAsync(ResourceName, CurrentLookupService, searchText, 1)).Keys;
        return ProviderKeys;
    }

    protected virtual async Task OnPermissionCheckedChanged(ResourcePermissionModel permission, bool value)
    {
        permission.IsGranted = value;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task GrantAllAsync(bool value)
    {
        foreach (var permission in CreateEntity.Permissions)
        {
            permission.IsGranted = value;
        }

        foreach (var permission in EditEntity.Permissions)
        {
            permission.IsGranted = value;
        }

        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OpenEditModalAsync(ResourcePermissionGrantInfoDto permission)
    {
        var resourcePermissions = await PermissionAppService.GetResourceByProviderAsync(ResourceName, ResourceKey, permission.ProviderName, permission.ProviderKey);
        EditEntity = new EditModel
        {
            ProviderName = permission.ProviderName,
            ProviderKey = permission.ProviderKey,
            Permissions = resourcePermissions.Permissions.Select(x => new ResourcePermissionModel
            {
                Name = x.Name,
                DisplayName = x.DisplayName,
                IsGranted = x.IsGranted
            }).ToList()
        };

        _editEditContext = new EditContext(EditEntity);

        _editVisible = true;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task CloseCreateModalAsync()
    {
        _createVisible = false;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task CloseEditModalAsync()
    {
        _editVisible = false;
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task OnLookupServiceCheckedValueChanged(string value)
    {
        CurrentLookupService = value;
        ProviderKey = null;
        ProviderDisplayName = null;
        _selectedProviderKeyInfo = null;
        _showProviderKeyValidationError = false;
        _createEditContext = new EditContext(CreateEntity);
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task CreateResourcePermissionAsync()
    {
        if (ProviderKey.IsNullOrWhiteSpace())
        {
            _showProviderKeyValidationError = true;
            await InvokeAsync(StateHasChanged);
            return;
        }

        await PermissionAppService.UpdateResourceAsync(
            ResourceName,
            ResourceKey,
            new UpdateResourcePermissionsDto
            {
                ProviderName = CurrentLookupService,
                ProviderKey = ProviderKey,
                Permissions = CreateEntity.Permissions.Where(p => p.IsGranted).Select(p => p.Name).ToList()
            }
        );

        await CloseCreateModalAsync();
        ResourcePermissionList = await PermissionAppService.GetResourceAsync(ResourceName, ResourceKey);
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task UpdateResourcePermissionAsync()
    {
        await PermissionAppService.UpdateResourceAsync(
            ResourceName,
            ResourceKey,
            new UpdateResourcePermissionsDto
            {
                ProviderName = EditEntity.ProviderName,
                ProviderKey = EditEntity.ProviderKey,
                Permissions = EditEntity.Permissions.Where(p => p.IsGranted).Select(p => p.Name).ToList()
            }
        );

        await CloseEditModalAsync();
        ResourcePermissionList = await PermissionAppService.GetResourceAsync(ResourceName, ResourceKey);
        await InvokeAsync(StateHasChanged);
    }

    protected virtual async Task DeleteResourcePermissionAsync(ResourcePermissionGrantInfoDto permission)
    {
        if(await UiMessageService.Confirm(L["ResourcePermissionDeletionConfirmationMessage"]))
        {
            await PermissionAppService.DeleteResourceAsync(
                ResourceName,
                ResourceKey,
                permission.ProviderName,
                permission.ProviderKey
            );

            ResourcePermissionList = await PermissionAppService.GetResourceAsync(ResourceName, ResourceKey);
            await Notify.Success(L["DeletedSuccessfully"]);
            await InvokeAsync(StateHasChanged);
        }
    }

    public class CreateModel
    {
        public List<ResourcePermissionModel> Permissions { get; set; }
    }

    public class EditModel
    {
        public string ProviderName { get; set; }

        public string ProviderKey { get; set; }

        public List<ResourcePermissionModel> Permissions { get; set; }
    }

    public class ResourcePermissionModel
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsGranted { get; set; }
    }
}
