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

        [SerializeField]
        private GlobalBlackboard _globalBlackboard;
        public GlobalBlackboard GlobalBlackboard => _globalBlackboard;
        
        [ShowOnly]
        public ActionSelection LastSelection;

        public AIBlackboard(Animal animal)
        {
            _animal = animal;
        }
    }
}