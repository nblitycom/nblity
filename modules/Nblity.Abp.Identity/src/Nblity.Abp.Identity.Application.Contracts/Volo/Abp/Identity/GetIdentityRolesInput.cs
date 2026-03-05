using Volo.Abp.Application.Dtos;

namespace Nblity.Abp.Identity;

public class GetIdentityRolesInput : ExtensiblePagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
