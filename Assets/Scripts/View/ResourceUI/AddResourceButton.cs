using Resources.Data;
using Resources.Services.ResourceService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.View
{
    public class AddResourceButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private string _addText;
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;
        
        private IResourceStorage _resourceStorage;

        public void Construct(IResourceStorage resourceStorage)
        {
            _resourceStorage = resourceStorage;
            _buttonText.text = $"{_addText}{_amount}";
            _button.onClick.AddListener(Add);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Add);
        }

        private void Add()
        {
            _resourceStorage.AddResource(_type, _amount);
        }
    }
}