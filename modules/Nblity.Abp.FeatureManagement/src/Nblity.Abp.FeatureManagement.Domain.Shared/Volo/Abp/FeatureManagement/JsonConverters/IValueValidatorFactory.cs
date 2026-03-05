using Volo.Abp;
﻿using Volo.Abp.Validation.StringValues;

namespace Nblity.Abp.FeatureManagement.JsonConverters;

public interface IValueValidatorFactory
{
    bool CanCreate(string name);

    IValueValidator Create();
}
