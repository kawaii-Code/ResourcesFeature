using Resources.Data;
using TMPro;
using UnityEngine;

namespace Resources.View
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void DisplayResource(ResourceData data)
        {
            _text.text = $"{data.Amount}";
        }
    }
}