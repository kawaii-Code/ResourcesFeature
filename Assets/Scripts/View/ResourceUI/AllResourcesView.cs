using System;
using System.Collections.Generic;
using Resources.Data;
using Resources.Services.ResourceService;

namespace Resources.View
{
    public class AllResourcesView : IDisposable
    {
        private readonly Dictionary<ResourceType, ResourceView> _views = new();
        private readonly IResourceService _resourceService;
        private readonly IUIFactory _uiFactory;

        public AllResourcesView(IResourceService resourceService, IUIFactory uiFactory)
        {
            _resourceService = resourceService;
            _uiFactory = uiFactory;
            _resourceService.ResourceChanged += UpdateView;
        }

        public void Dispose()
        {
            _resourceService.ResourceChanged -= UpdateView;
        }

        public void Display(params ResourceType[] types)
        {
            foreach (ResourceType type in types)
                UpdateView(type, _resourceService.GetResource(type));
        }

        private void UpdateView(ResourceType type, ResourceData newData)
        {
            if (!_views.TryGetValue(type, out ResourceView view))
            {
                view = _uiFactory.CreateResourceView(type);
                _views.Add(type, view);
            }
            
            view.DisplayResource(newData);
        }
    }
}