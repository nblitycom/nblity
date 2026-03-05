using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Nblity.Abp.TenantManagement.Application.Nblity.Abp.TenantManagement;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties]
public partial class TenantToTenantDtoMapper
    : MapperBase<Tenant, TenantDto>
{
    public override partial TenantDto Map(Tenant source);
    public override partial void Map(Tenant source, TenantDto destination);
}