using System;
using Volo.Abp.Domain.Repositories;

namespace Nblity.Abp.PermissionManagement;

public interface IPermissionGroupDefinitionRecordRepository : IBasicRepository<PermissionGroupDefinitionRecord, Guid>
{
    
}