using System;
using System.Linq;
using Resources.Data;
using UnityEngine;

namespace Resources.Config
{
    [CreateAssetMenu(menuName = "Config/Resources", fileName = "ResourcesConfig")]
    public class ResourcesConfig : ScriptableObject
    {
        [Serializable]
        public class ResourceInitialConfig
        {
            public ResourceType Type;
            public int InitialAmount;
        }

        public ResourceInitialConfig[] InitialData;

        private void OnValidate()
        {
            if (CountOfDifferentTypes() != InitialData.Length)
                throw new ArgumentException("Two resources of the same type found!");
        }

        private int CountOfDifferentTypes() =>
            InitialData.GroupBy(resource => resource.Type).Count();
    }
}