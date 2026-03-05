using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp;

namespace Nblity.Abp.Account;

public interface IDynamicClaimsAppService : IApplicationService
{
    Task RefreshAsync();
}
