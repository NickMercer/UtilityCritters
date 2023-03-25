using System.Collections.Generic;
using UnityEngine;

namespace SimpleUtilityFramework.Environment
{
    [CreateAssetMenu(menuName = "Runtime Lists/Water Sources", fileName = "Water Sources", order = 0)]
    public class WaterSourceList : ScriptableObject
    {
        [SerializeField]
        private List<WaterSource> _waterSources;
        public List<WaterSource> WaterSources => _waterSources;

        public void Register(WaterSource source) => _waterSources.Add(source);

        public void Unregister(WaterSource source) => _waterSources.Remove(source);
    }
}