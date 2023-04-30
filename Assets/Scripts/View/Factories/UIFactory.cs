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
        private readonly Transform _buyButtonParent;
        private readonly Transform _spendButtonParent;
        private readonly Transform _resourceViewParent;

        public UIFactory(
            IResourceService resourceService,
            IConfigService configService,
            Transform buyButtonParent,
            Transform spendButtonParent,
            Transform resourceViewParent)
        {
            _resourceService = resourceService;
            _configService = configService;
            _buyButtonParent = buyButtonParent;
            _spendButtonParent = spendButtonParent;
            _resourceViewParent = resourceViewParent;
        }

        public ResourceAddButton CreateAddButton(ResourceType buyType)
        {
            GameObject buttonPrefab = _configService.GetAddButtonPrefabFor(buyType);
            ResourceAddButton button = Object.Instantiate(buttonPrefab, _buyButtonParent).GetComponent<ResourceAddButton>();
            button.Construct(_resourceService);
            
            return button;
        }

        public ResourceSpendButton CreateSpendButton(ResourceType spendType)
        {
            GameObject buttonPrefab = _configService.GetSpendButtonPrefabFor(spendType);
            ResourceSpendButton button = Object.Instantiate(buttonPrefab, _spendButtonParent).GetComponent<ResourceSpendButton>();
            button.Construct(_resourceService, _resourceService);
            
            return button;
        }

        public ResourceView CreateResourceView(ResourceType type)
        {
            GameObject viewPrefab = _configService.GetResourceViewPrefabFor(type);
            ResourceView view = Object.Instantiate(viewPrefab, _resourceViewParent).GetComponent<ResourceView>();
            
            return view;
        }
    }
}