using System;
using UnityEngine;

namespace SimpleUtilityFramework.Environment
{
    public class FoodSource : MonoBehaviour, IConsumable
    {
        [SerializeField]
        private FoodSourceList _activeFoodSources;
        
        [SerializeField]
        private int _foodAmount;
        public int FoodAmount => _foodAmount;

        [SerializeField]
        private ReplenishableObject _replenishable;

        private bool _isOccupied;

        public bool IsAvailable
        {
            get
            {
                if (_isOccupied)
                    return false;
                
                if (_replenishable != null)
                    return _replenishable.IsAvailable;

                return true;
            }
        }
        
        private void Awake()
        {
            _activeFoodSources.Register(this);
        }

        private void OnDestroy()
        {
            _activeFoodSources.Unregister(this);
        }

        public void Occupy()
        {
            _isOccupied = true;
        }

        public void Consume(IConsumer consumer)
        {
            consumer.Feed(_foodAmount);
            
            if (_replenishable != null)
            {
                _replenishable.Consume(consumer);   
            }
            else
            {
                Destroy(gameObject);
            }
            
            _isOccupied = false;
        }
    }
}