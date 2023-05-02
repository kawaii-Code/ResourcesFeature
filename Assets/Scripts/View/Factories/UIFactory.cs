using Resources.Data;
using Resources.Services.ConfigService;
using Resources.Services.ResourceService;
using UnityEngine;

namespace Resources.View
{
    public class UIFactory : IUIFactory
    {
        private readonly IResourceService _resourceService;
        private readonly IConfigService _configService;
        private readonly Transform _addButtonParent;
        private readonly Transform _spendButtonParent;
        private readonly Transform _resourceViewParent;

        public UIFactory(
            IResourceService resourceService,
            IConfigService configService,
            Transform addButtonParent,
            Transform spendButtonParent,
            Transform resourceViewParent)
        {
            _resourceService = resourceService;
            _configService = configService;
            _addButtonParent = addButtonParent;
            _spendButtonParent = spendButtonParent;
            _resourceViewParent = resourceViewParent;
        }

        public AddResourceButton CreateAddButton(ResourceType buyType)
        {
            AddResourceButton buttonPrefab = _configService.GetAddButtonPrefabFor(buyType);
            AddResourceButton button = Object.Instantiate(buttonPrefab, _addButtonParent);
            button.Construct(_resourceService);
            
            return button;
        }

        public SpendResourceButton CreateSpendButton(ResourceType spendType)
        {
            SpendResourceButton buttonPrefab = _configService.GetSpendButtonPrefabFor(spendType);
            SpendResourceButton button = Object.Instantiate(buttonPrefab, _spendButtonParent);
            button.Construct(_resourceService, _resourceService);
            
            return button;
        }

        public ResourceView CreateResourceView(ResourceType type)
        {
            ResourceView viewPrefab = _configService.GetResourceViewPrefabFor(type);
            ResourceView view = Object.Instantiate(viewPrefab, _resourceViewParent).GetComponent<ResourceView>();
            
            return view;
        }
    }
}