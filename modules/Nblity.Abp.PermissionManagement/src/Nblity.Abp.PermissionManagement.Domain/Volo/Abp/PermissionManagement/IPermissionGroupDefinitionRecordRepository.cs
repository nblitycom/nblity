using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace Nblity.Abp.PermissionManagement;

public interface IPermissionGroupDefinitionRecordRepository : IBasicRepository<PermissionGroupDefinitionRecord, Guid>
{
    
}