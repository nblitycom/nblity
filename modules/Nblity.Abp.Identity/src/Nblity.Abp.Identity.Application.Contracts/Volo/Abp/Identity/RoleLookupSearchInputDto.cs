using Volo.Abp.Application.Dtos;

namespace Nblity.Abp.Identity;

public class RoleLookupSearchInputDto : ExtensiblePagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
