using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.Environment;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Eat", menuName = "AI Behaviours/Eat", order = 0)]
    public class Eat : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            //Get all foods within eating range (1f)
            var colliders = new Collider2D[5];
            var size = Physics2D.OverlapCircleNonAlloc(blackboard.Self.transform.position, 1f, colliders);
            for (var i = 0; i < size; i++)
            {
                var collider = colliders[i];
                var food = collider.GetComponent<FoodSource>();
                if (food != null)
                {
                    yield return new ActionTarget<FoodSource>
                    {
                        Target = food,
                        TargetLocation = food.transform.position,
                        TargetObject = food.gameObject
                    };
                }
            }
        }

        public override float Score(AIBlackboard blackboard, ActionTarget target)
        {
            var food = (target as ActionTarget<FoodSource>).Target;
            if (food.IsAvailable == false)
                return 0f;

            var currentHunger = blackboard.Animal.Stats.Hunger;
            var maxHunger = blackboard.Animal.Stats.MaxHunger;
            var hungerScore = currentHunger / (float)maxHunger;
            var foodQuality = food.FoodAmount / 30f;
            
            return hungerScore * foodQuality;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var food = (target as ActionTarget<FoodSource>).Target;
            
            blackboard.Self.transform.DOShakeRotation(1f, 30f);
            food.Occupy();
            yield return new WaitForSeconds(0.5f);
            
            food.Consume(blackboard.Animal);

            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
    }
}