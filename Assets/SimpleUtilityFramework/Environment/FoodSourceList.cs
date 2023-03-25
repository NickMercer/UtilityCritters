using System.Collections.Generic;
using UnityEngine;

namespace SimpleUtilityFramework.Environment
{
    [CreateAssetMenu(menuName = "Runtime Lists/Food Sources", fileName = "Food Sources", order = 0)]
    public class FoodSourceList : ScriptableObject
    {
        [SerializeField]
        private List<FoodSource> _foodSources;
        public List<FoodSource> FoodSources => _foodSources;

        public void Register(FoodSource source) => _foodSources.Add(source);

        public void Unregister(FoodSource source) => _foodSources.Remove(source);
    }
}