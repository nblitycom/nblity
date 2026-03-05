using Volo.Abp;
﻿using Volo.Abp.Application.Dtos;

namespace Nblity.Abp.Identity;

public class GetIdentityUsersInput : ExtensiblePagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
