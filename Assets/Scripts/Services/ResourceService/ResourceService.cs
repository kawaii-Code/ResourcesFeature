using System;
using System.Collections.Generic;
using Resources.Data;
using Resources.Services.ConfigService;
using UnityEngine;

namespace Resources.Services.ResourceService
{
    public class ResourceService : IResourceService
    {
        private readonly IConfigService _configService;
        private Dictionary<ResourceType, ResourceData> _resources;

        public ResourceService(IConfigService configService)
        {
            _configService = configService;
        }

        public event Action<ResourceType, ResourceData> ResourceChanged;

        public void LoadInitialValues() =>
            _resources = _configService.LoadInitialResourceValues();

        public ResourceData GetResource(ResourceType type)
        {
            if (!_resources.ContainsKey(type))
            {
                Debug.LogError($"Trying to get a resource {type} that does not exist!");
                return null;
            }
            
            return _resources[type];
        }

        public void AddResource(ResourceType type, int amount)
        {
            if (amount < 0)
            {
                Debug.LogError($"Can't add a negative amount of resources! The amount was: {amount}.");
                return;
            }
            
            if (!_resources.TryGetValue(type, out ResourceData data))
            {
                data = new ResourceData(amount);
                _resources.Add(type, data);
            }
            else
            {
                data.Amount += amount;
            }
            
            ResourceChanged?.Invoke(type, data);
        }

        public void SpendResource(ResourceType type, int amount)
        {
            if (!_resources.TryGetValue(type, out ResourceData data))
            {
                Debug.LogWarning($"Trying to spend a resource {type} that was not created yet!");
                return;
            }
            
            if (amount < 0)
            {
                Debug.LogError($"Can't spend a negative amount of resources! The amount was: {amount}.");
                return;
            }

            if (data.Amount - amount < 0)
            {
                Debug.LogError($"Spent more resource {type} than what was available! {type} left: {data.Amount}.");
                return;
            }

            data.Amount -= amount;
            ResourceChanged?.Invoke(type, data);
        }

        public bool IsEnoughOf(ResourceType type, int neededAmount)
        {
            if (!_resources.TryGetValue(type, out ResourceData data))
            {
                Debug.LogWarning($"Trying to check whether there is enough of resource {type} that does not exist!");
                return false;
            }

            return data.Amount >= neededAmount;
        }
    }
}