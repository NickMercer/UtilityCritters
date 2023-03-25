using System;
using UnityEngine;

namespace Natick.SimpleUtility
{
    [Serializable]
    public class AIBlackboard
    {
        [SerializeField, ShowOnly]
        private Animal _animal;
        public Animal Animal => _animal;
        
        public GameObject Self => _animal.gameObject;

        public GlobalBlackboard _globalBlackboard;
        public GlobalBlackboard GlobalBlackboard => _globalBlackboard;

        public AIBlackboard(Animal animal)
        {
            _animal = animal;
        }
    }
}