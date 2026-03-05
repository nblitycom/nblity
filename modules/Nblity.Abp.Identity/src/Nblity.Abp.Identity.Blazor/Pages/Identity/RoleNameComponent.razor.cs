using Microsoft.AspNetCore.Components;
using Volo.Abp;

namespace Nblity.Abp.Identity.Blazor.Pages.Identity;

public partial class RoleNameComponent : ComponentBase
{
    [Parameter] public object Data { get; set; }
}
