using System;
using Volo.Abp.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp;

namespace Nblity.Abp.Identity;

[Serializable]
public class OrganizationUnitEto : IMultiTenant, IHasEntityVersion
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }
    
    public virtual Guid? ParentId { get; set; }

    public string Code { get; set; }

    public string DisplayName { get; set; }

    public int EntityVersion { get; set; }
}
