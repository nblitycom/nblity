using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp;

namespace Nblity.Abp.TenantManagement;

public class TenantDto : ExtensibleEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; }

    public string ConcurrencyStamp { get; set; }
}
