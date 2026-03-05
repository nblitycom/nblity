using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using Volo.Abp;

namespace Nblity.Abp.TenantManagement.Blazor;

[Mapper]
[MapExtraProperties]
public partial class TenantDtoToTenantUpdateDtoMapper
    : MapperBase<TenantDto, TenantUpdateDto>
{
    [MapperIgnoreSource(nameof(TenantDto.Id))]
    public override partial TenantUpdateDto Map(TenantDto source);

    [MapperIgnoreSource(nameof(TenantDto.Id))]
    public override partial void Map(TenantDto source, TenantUpdateDto destination);
}