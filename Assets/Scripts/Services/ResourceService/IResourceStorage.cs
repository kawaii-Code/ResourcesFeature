using Resources.Data;

namespace Resources.Services.ResourceService
{
    public interface IResourceStorage
    {
        ResourceData GetResource(ResourceType type);
        void AddResource(ResourceType type, int amount);
        void SpendResource(ResourceType type, int amount);
        bool IsEnoughOf(ResourceType type, int neededAmount);
    }
}