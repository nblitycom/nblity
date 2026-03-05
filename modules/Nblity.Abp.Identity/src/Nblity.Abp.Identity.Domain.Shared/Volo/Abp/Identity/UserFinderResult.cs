using System;
using Volo.Abp;

namespace Nblity.Abp.Identity;

public class UserFinderResult
{
    public Guid Id { get; set; }

    public string UserName { get; set; }
}
