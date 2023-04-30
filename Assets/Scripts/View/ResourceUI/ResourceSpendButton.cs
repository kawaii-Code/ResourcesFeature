using Resources.Data;
using Resources.Services.ResourceService;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.View
{
    public class ResourceSpendButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;

        private IResourceStorage _resourceStorage;
        private IResourceUpdater _resourceUpdater;
        private bool _isButtonActive;

        public void Construct(IResourceStorage resourceStorage, IResourceUpdater resourceUpdater)
        {
            _resourceStorage = resourceStorage;
            _resourceUpdater = resourceUpdater;

            _resourceUpdater.ResourceChanged += CheckIfEnough;
            _button.interactable = resourceStorage.IsEnoughOf(_type, _amount);
            _button.onClick.AddListener(Spend);
        }

        private void OnDestroy()
        {
            _resourceUpdater.ResourceChanged -= CheckIfEnough;
            _button.onClick.RemoveListener(Spend);
        }

        private void Spend()
        {
            _resourceStorage.SpendResource(_type, _amount);
        }

        private void CheckIfEnough(ResourceType type, ResourceData data)
        {
            if (_type == type) 
                _button.interactable = data.Amount >= _amount;
        }
    }
}