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

        [SerializeField, Range(30, 500)] 
        private int _maxHealth;
        public int MaxHealth => _maxHealth;
        
        [SerializeField, Range(20, 150)] 
        private int _maxHunger;
        public int MaxHunger => _maxHunger;
        
        [SerializeField, Range(20, 150)] 
        private int _maxThirst;
        public int MaxThirst => _maxThirst;
        
        [SerializeField, Range(35, 150)] 
        private int _maxEnergy;
        public int MaxEnergy => _maxEnergy;
        
        [SerializeField, Range(0, 30)] 
        private int _sleepThreshold;
        public int SleepThreshold => _sleepThreshold;
        
        public AnimalStats GetAnimalStats()
        {
            return new AnimalStats(MaxHealth, MaxHunger, MaxThirst, MaxEnergy, SleepThreshold);
        }
    }
}