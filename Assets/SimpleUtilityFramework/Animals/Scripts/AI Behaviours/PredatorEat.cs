using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using Natick.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Predator Eat", menuName = "AI Behaviours/Predator Eat", order = 0)]
    public class PredatorEat : AIBehaviour
    {
        [SerializeField]
        private float _eatTimeSeconds = 1f;

        [SerializeField]
        private int _minDamage = 30;

        [SerializeField]
        private int _maxDamage = 70;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            foreach (var food in AIHelpers.GetInRange<Animal>(0.5f, blackboard.Self.transform.position))
            {
                if(food.FoodSource != null && food.gameObject != blackboard.Animal.gameObject && food.FoodSource.IsAvailable)
                {
                    yield return new ActionTarget<Animal>
                    {
                        Target = food,
                        TargetLocation = food.transform.position,
                        TargetObject = food.gameObject
                    };
                }
            }
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var animal = ((ActionTarget<Animal>) target).Target;
            if(animal.FoodSource.IsAvailable == false)
                return FloatNormal.Zero;

            var lastAction = blackboard.LastSelection.Action;
            var previouslyAttacked = lastAction != null && (lastAction.name == name || lastAction.name == "MoveToFood");
            var momentumBonus = previouslyAttacked ? 1.1f : 1f;
            
            var preyHealth = animal.Stats.HealthPercentage;
            var preyHealthScore = Mathf.Min(1, 1.5f - preyHealth);
            
            var hungerScore = blackboard.Animal.Stats.HungerPercentage;
            var foodQuality = animal.FoodSource.FoodAmount / 30f;
            
            return new FloatNormal(hungerScore * foodQuality * preyHealthScore * momentumBonus);
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var animal = blackboard.Animal;
            var food = ((ActionTarget<Animal>) target).Target;

            animal.AnimateEating(_eatTimeSeconds);
            
            if ((food.Stats.HealthPercentage) < 0.2f) // Eat the animal
            {
                food.FoodSource.Occupy();
                yield return new WaitForSeconds(_eatTimeSeconds/2);
                food.FoodSource.Consume(blackboard.Animal); 
            }
            else // Damage the animal
            {
                yield return new WaitForSeconds(_eatTimeSeconds/2);
                food.Stats.UpdateHealth(-Random.Range(_minDamage, _maxDamage));
            }
            
            yield return new WaitForSeconds(_eatTimeSeconds/2);
            onComplete?.Invoke();
        }
    }
}