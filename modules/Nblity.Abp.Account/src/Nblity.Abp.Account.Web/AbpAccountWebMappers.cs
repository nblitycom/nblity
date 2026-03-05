using Riok.Mapperly.Abstractions;
using Nblity.Abp.Account.Web.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using Volo.Abp.Mapperly;
using Volo.Abp;

namespace Nblity.Abp.Account.Web;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties]
public partial class ProfileDtoToPersonalInfoModelMapper : MapperBase<ProfileDto, AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel>
{
    public override partial AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel Map(ProfileDto source);
    public override partial void Map(ProfileDto source, AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel destination);
}