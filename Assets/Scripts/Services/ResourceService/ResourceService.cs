using System;
using System.Collections.Generic;
using Resources.Data;
using Resources.Services.ConfigService;

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

        public ResourceData GetResource(ResourceType type) =>
            _resources[type];

        public void AddResource(ResourceType type, int amount)
        {
            if (_resources.TryGetValue(type, out ResourceData data))
            {
                data.Amount += amount;
                ResourceChanged?.Invoke(type, data);
            }
            else
            {
                ResourceData newData = new(amount);
                _resources.Add(type, newData);
                ResourceChanged?.Invoke(type, newData);
            }
        }

        public void SpendResource(ResourceType type, int amount)
        {
            if (_resources.TryGetValue(type, out ResourceData data))
            {
                data.Amount -= amount;
                if (data.Amount < 0)
                    throw new InvalidOperationException($"Spent more resource '{type}' than what was available! '{type}' left: {data.Amount}.");
                ResourceChanged?.Invoke(type, data);
            }
            else
            {
                throw new ArgumentException($"Trying to spend a resource '{type}' that does not exist!");
            }
        }

        public bool IsEnoughOf(ResourceType type, int neededAmount)
        {
            if (_resources.TryGetValue(type, out ResourceData data))
                return data.Amount >= neededAmount;
            
            throw new ArgumentException($"Trying to check whether there is enough of resource '{type}' that does not exist!");
        }
    }
}