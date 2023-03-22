using UnityEngine;

namespace SimpleUtilityFramework.Animals
{
    [CreateAssetMenu(fileName = "Animal Config", menuName = "New Animal Config", order = 0)]
    public class AnimalConfig : ScriptableObject
    {
        [SerializeField]
        private Sprite _sprite;
        public Sprite Sprite => _sprite;

        [SerializeField]
        private string _speciesName;
        public string SpeciesName => _speciesName;
    }
}