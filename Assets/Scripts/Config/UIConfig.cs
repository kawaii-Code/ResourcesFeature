using System;
using System.Linq;
using Resources.Data;
using Resources.View;
using UnityEngine;

namespace Resources.Config
{
    [CreateAssetMenu(menuName = "Config/UI", fileName = "UIConfig")]
    public class UIConfig : ScriptableObject
    {
        [Serializable]
        public class ResourceViewConfig<T> where T : Component
        {
            public ResourceType Type;
            public T Prefab;
        }

        public ResourceViewConfig<AddResourceButton>[] AddButtons;
        public ResourceViewConfig<SpendResourceButton>[] SpendButtons;
        public ResourceViewConfig<ResourceView>[] ResourceViews;
        
        private void OnValidate()
        {
            if (CountOfDifferentTypes(AddButtons) != AddButtons.Length)
                throw new ArgumentException("Two resources of the same type found in add buttons!");
            if (CountOfDifferentTypes(SpendButtons) != SpendButtons.Length)
                throw new ArgumentException("Two resources of the same type found in spend buttons!");
            if (CountOfDifferentTypes(ResourceViews) != ResourceViews.Length)
                throw new ArgumentException("Two resources of the same type found in resource views!");
        }
        
        private int CountOfDifferentTypes<T>(ResourceViewConfig<T>[] configs) where T : Component =>
            configs.GroupBy(resource => resource.Type).Count();
    }
}