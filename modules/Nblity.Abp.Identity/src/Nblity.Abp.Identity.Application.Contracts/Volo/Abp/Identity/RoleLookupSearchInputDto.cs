using Volo.Abp.Application.Dtos;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public class RoleLookupSearchInputDto : ExtensiblePagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
