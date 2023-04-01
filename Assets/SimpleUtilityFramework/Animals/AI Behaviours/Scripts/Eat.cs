using System;
using System.Collections;
using System.Collections.Generic;
using Natick.SimpleUtility;
using SimpleUtilityFramework.Environment;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Eat", menuName = "AI Behaviours/Eat", order = 0)]
    public class Eat : AIBehaviour
    {
        [SerializeField]
        private float _secondsToEat = 1f;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            //Get all foods within eating range
            foreach (var foodSource in AIHelpers.GetInRange<FoodSource>(0.5f, blackboard.Self.transform.position))
            {
                if (foodSource.FoodType == blackboard.Animal.AnimalData.FoodSourceType && foodSource.IsAvailable)
                {
                    yield return new ActionTarget<FoodSource>
                    {
                        Target = foodSource,
                        TargetLocation = foodSource.transform.position,
                        TargetObject = foodSource.gameObject
                    };
                }
            }
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var food = ((ActionTarget<FoodSource>) target).Target;
            if (food.IsAvailable == false)
                return FloatNormal.Zero;

            var hungerScore = blackboard.Animal.Stats.HungerPercentage;
            var foodQuality = food.FoodAmount / 30f;
            
            return new FloatNormal(hungerScore * foodQuality);
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var animal = blackboard.Animal;
            var food = ((ActionTarget<FoodSource>) target).Target;
            
            animal.AnimateEating(_secondsToEat);
            food.Occupy();
            yield return new WaitForSeconds(_secondsToEat/2);
            
            food.Consume(blackboard.Animal);
 
            yield return new WaitForSeconds(_secondsToEat/2);
            onComplete?.Invoke();
        }
    }
}