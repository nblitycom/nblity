using Volo.Abp;
﻿using Volo.Abp.Application.Dtos;

namespace Nblity.Abp.TenantManagement;

public class GetTenantsInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
