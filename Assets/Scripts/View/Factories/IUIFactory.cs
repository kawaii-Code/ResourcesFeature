using Resources.Data;

namespace Resources.View
{
    public interface IUIFactory
    {
        ResourceAddButton CreateAddButton(ResourceType buyType);
        ResourceSpendButton CreateSpendButton(ResourceType spendType);
        ResourceView CreateResourceView(ResourceType type);
    }
}