using System.Collections.Generic;
using Resources.Data;
using UnityEngine;

namespace Resources.Services.ConfigService
{
    public interface IConfigService
    {
        Dictionary<ResourceType, ResourceData> LoadInitialResourceValues();
        GameObject GetAddButtonPrefabFor(ResourceType addType);
        GameObject GetSpendButtonPrefabFor(ResourceType spendType);
        GameObject GetResourceViewPrefabFor(ResourceType type);
    }
}