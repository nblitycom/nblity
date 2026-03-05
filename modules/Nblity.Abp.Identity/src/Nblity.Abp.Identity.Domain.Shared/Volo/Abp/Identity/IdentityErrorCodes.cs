using Volo.Abp;
﻿namespace Nblity.Abp.Identity;

public static class IdentityErrorCodes
{
    public const string UserSelfDeletion = "Nblity.Abp.Identity:010001";
    public const string MaxAllowedOuMembership = "Nblity.Abp.Identity:010002";
    public const string ExternalUserPasswordChange = "Nblity.Abp.Identity:010003";
    public const string DuplicateOrganizationUnitDisplayName = "Nblity.Abp.Identity:010004";
    public const string StaticRoleRenaming = "Nblity.Abp.Identity:010005";
    public const string StaticRoleDeletion = "Nblity.Abp.Identity:010006";
    public const string UsersCanNotChangeTwoFactor = "Nblity.Abp.Identity:010007";
    public const string CanNotChangeTwoFactor = "Nblity.Abp.Identity:010008";
    public const string YouCannotDelegateYourself = "Nblity.Abp.Identity:010009";
    public const string ClaimNameExist = "Nblity.Abp.Identity:010021";
    public const string CanNotUpdateStaticClaimType = "Nblity.Abp.Identity:010022";
    public const string CanNotDeleteStaticClaimType = "Nblity.Abp.Identity:010023";
}
