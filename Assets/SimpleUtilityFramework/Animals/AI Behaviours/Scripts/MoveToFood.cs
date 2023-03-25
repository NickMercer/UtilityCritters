using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.Environment;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Move To Food", menuName = "AI Behaviours/Move To Food", order = 0)]
    public class MoveToFood : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            var activeFoodSources = GlobalBlackboard.ActiveFoodSources.FoodSources;
            for (var i = 0; i < activeFoodSources.Count; i++)
            {
                var source = activeFoodSources[i];
                yield return new ActionTarget<FoodSource>
                {
                    Target = source,
                    TargetObject = source.gameObject,
                    TargetLocation = source.transform.position
                };
            }
        }

        public override float Score(AIBlackboard blackboard, ActionTarget target)
        {
            var foodTarget = (target as ActionTarget<FoodSource>).Target;
            if (foodTarget.IsAvailable == false)
                return 0f;
            
            //If we're next to the food already, exit early
            var animalPosition = blackboard.Self.transform.position;
            var distanceToFood = Vector3.Distance(animalPosition, target.TargetLocation);
            if (distanceToFood < 1)
                return 0f;

            //Get the current Hunger of the animal
            var currentHunger = blackboard.Animal.Stats.Hunger;
            var maxHunger = blackboard.Animal.Stats.MaxHunger;
            var hungerScore = currentHunger / (float)maxHunger;
            
            //Early Exit if we're not that hungry
            if (hungerScore < 0.3f)
                return 0f;
            
            //Get the hunger after eating this food
            var foodAmount = foodTarget.FoodAmount;
            var restorationScore = foodAmount / 30; //We'll say 30 is average food. if a food is worth more than 30 that's a good bonus to go to it.
            
            //Prioritize Closest food
            var distanceReference = GlobalBlackboard.Instance.Bounds.width;
            var closenessScore = Mathf.Max(0.5f, 1f - (distanceToFood / distanceReference));;
            
            //Randomize a bit
            var randomScore = Random.Range(0.9f, 1.1f);

            return hungerScore * restorationScore * closenessScore * randomScore;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var foodLocation = target.TargetLocation;
            var transform = blackboard.Self.transform;
            var moveSpeed = blackboard.Animal.AnimalData.WalkSpeed/10;

            var distance = Vector3.Distance(transform.position, foodLocation);
            var travelTime = distance / moveSpeed;

            var emote = Instantiate(GlobalBlackboard.EmotePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
            emote.Initialize(EmoteType.Hunger);
            
            transform.DOMove(foodLocation, travelTime);
            yield return new WaitForSeconds(travelTime);
            
            onComplete?.Invoke();
        }
    }
}