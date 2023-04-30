using System;
using Resources.Data;

namespace Resources.Services.ResourceService
{
    public interface IResourceUpdater
    {
        event Action<ResourceType, ResourceData> ResourceChanged;
    }
}