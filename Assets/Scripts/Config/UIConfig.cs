using System;
using System.Linq;
using Resources.Data;
using UnityEngine;

namespace Resources.Config
{
    [CreateAssetMenu(menuName = "Config/UI", fileName = "UIConfig")]
    public class UIConfig : ScriptableObject
    {
        [Serializable]
        public class ResourceViewConfig
        {
            public ResourceType Type;
            public GameObject Prefab;
        }

        public ResourceViewConfig[] AddButtons;
        public ResourceViewConfig[] SpendButtons;
        public ResourceViewConfig[] ResourceViews;
        
        private void OnValidate()
        {
            if (AddButtons.GroupBy(resource => resource.Type).Count() != AddButtons.Length)
                throw new ArgumentException("Two resources of the same type found in add buttons!");
            if (SpendButtons.GroupBy(resource => resource.Type).Count() != SpendButtons.Length)
                throw new ArgumentException("Two resources of the same type found in spend buttons!");
            if (ResourceViews.GroupBy(resource => resource.Type).Count() != ResourceViews.Length)
                throw new ArgumentException("Two resources of the same type found in spend buttons!");
        }
    }
}