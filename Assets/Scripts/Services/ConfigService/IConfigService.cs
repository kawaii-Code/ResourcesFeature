using System.Collections.Generic;
using Resources.Data;
using Resources.View;

namespace Resources.Services.ConfigService
{
    public interface IConfigService
    {
        Dictionary<ResourceType, ResourceData> LoadInitialResourceValues();
        AddResourceButton GetAddButtonPrefabFor(ResourceType addType);
        SpendResourceButton GetSpendButtonPrefabFor(ResourceType spendType);
        ResourceView GetResourceViewPrefabFor(ResourceType type);
    }
}