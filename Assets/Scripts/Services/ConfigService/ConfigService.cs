using System.Collections.Generic;
using System.Linq;
using Resources.Config;
using Resources.Data;
using Resources.View;

namespace Resources.Services.ConfigService
{
    public class ConfigService : IConfigService
    {
        private readonly ResourcesConfig _resourcesConfig;
        private readonly UIConfig _uiConfig;

        public ConfigService(ResourcesConfig resourcesConfig, UIConfig uiConfig)
        {
            _resourcesConfig = resourcesConfig;
            _uiConfig = uiConfig;
        }

        public Dictionary<ResourceType, ResourceData> LoadInitialResourceValues() =>
            _resourcesConfig.InitialData.ToDictionary(resource => resource.Type, resource => new ResourceData(resource.InitialAmount));
        
        public AddResourceButton GetAddButtonPrefabFor(ResourceType addType) =>
            _uiConfig.AddButtons.First(button => button.Type == addType).Prefab;
        public SpendResourceButton GetSpendButtonPrefabFor(ResourceType spendType) =>
            _uiConfig.SpendButtons.First(button => button.Type == spendType).Prefab;
        public ResourceView GetResourceViewPrefabFor(ResourceType type) =>
            _uiConfig.ResourceViews.First(view => view.Type == type).Prefab;
    }
}