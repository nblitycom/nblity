using Volo.Abp;
﻿using Volo.Abp.Domain.Entities;

namespace Nblity.Abp.Identity;

public class IdentityRoleUpdateDto : IdentityRoleCreateOrUpdateDtoBase, IHasConcurrencyStamp
{
    public string ConcurrencyStamp { get; set; }
}
