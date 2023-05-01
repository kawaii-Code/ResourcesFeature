using System;
using System.Collections.Generic;
using Resources.Config;
using Resources.Data;
using Resources.Services.ConfigService;
using Resources.Services.ResourceService;
using Resources.View;
using UnityEngine;

namespace Resources
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ResourcesConfig _resourceConfig;
        [SerializeField] private UIConfig _uiConfig;
        [SerializeField] private Transform _resourceViewRoot;
        [SerializeField] private Transform _addButtonRoot;
        [SerializeField] private Transform _spendButtonRoot;

        private IResourceService _resourceService;
        private IConfigService _configService;
        private List<IDisposable> _disposables;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            _disposables = new List<IDisposable>();
            RegisterServices();
            InitializeGame();
            InitializeUI();
        }

        private void OnDestroy()
        {
            foreach (IDisposable disposable in _disposables)
                disposable.Dispose();
        }

        private void RegisterServices()
        {
            IConfigService configService = new ConfigService(_resourceConfig, _uiConfig);
            IResourceService resourceService = new ResourceService(configService);

            _resourceService = resourceService;
            _configService = configService;
        }

        private void InitializeGame()
        {
            _resourceService.LoadInitialValues();
        }

        private void InitializeUI()
        {
            IUIFactory uiFactory = new UIFactory(
                _resourceService, 
                _configService,
                _addButtonRoot,
                _spendButtonRoot,
                _resourceViewRoot);
            AllResourcesView resourcesView = new(_resourceService, uiFactory);
            
            CreateTestUI(uiFactory, resourcesView);

            _disposables.Add(resourcesView);
        }

        private static void CreateTestUI(IUIFactory uiFactory, AllResourcesView resourcesView)
        {
            uiFactory.CreateAddButton(ResourceType.Gold);
            uiFactory.CreateAddButton(ResourceType.Key);
            
            uiFactory.CreateSpendButton(ResourceType.Gold);
            uiFactory.CreateSpendButton(ResourceType.Gem);
            
            resourcesView.Display(ResourceType.Gold, ResourceType.Gem);
        }
    }
}