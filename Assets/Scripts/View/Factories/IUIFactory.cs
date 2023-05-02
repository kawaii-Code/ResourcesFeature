using Resources.Data;

namespace Resources.View
{
    public interface IUIFactory
    {
        AddResourceButton CreateAddButton(ResourceType buyType);
        SpendResourceButton CreateSpendButton(ResourceType spendType);
        ResourceView CreateResourceView(ResourceType type);
    }
}