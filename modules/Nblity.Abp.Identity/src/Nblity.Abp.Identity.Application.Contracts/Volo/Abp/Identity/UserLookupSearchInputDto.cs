using Volo.Abp.Application.Dtos;

namespace Nblity.Abp.Identity;

public class UserLookupSearchInputDto : ExtensiblePagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
