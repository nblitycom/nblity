using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nblity.Abp.PermissionManagement;

public interface IPermissionFinder
{
    Task<List<IsGrantedResponse>> IsGrantedAsync(List<IsGrantedRequest> requests);
}
