﻿using Resources.Data;
using Resources.Services.ResourceService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.View
{
    public class SpendResourceButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private string _spendText;
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;

        private IResourceStorage _resourceStorage;
        private IResourceUpdater _resourceUpdater;

        public void Construct(IResourceStorage resourceStorage, IResourceUpdater resourceUpdater)
        {
            _resourceStorage = resourceStorage;
            _resourceUpdater = resourceUpdater;

            _resourceUpdater.ResourceChanged += CheckIfEnough;
            _button.interactable = resourceStorage.IsEnoughOf(_type, _amount);
            _button.onClick.AddListener(Spend);
            _buttonText.text = $"{_spendText}{_amount}";
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