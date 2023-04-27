using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using Natick.Utilities;
using SimpleUtilityFramework.Environment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Move To Food", menuName = "AI Behaviours/Move To Food", order = 0)]
    public class MoveToFood : AIBehaviour
    {
        [SerializeField]
        private Emote _emotePrefab;

        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            var activeFoodSources = GlobalBlackboard.ActiveFoodSources.FoodSources;
            for (var i = 0; i < activeFoodSources.Count; i++)
            {
                var source = activeFoodSources[i];
                if (source == null)
                    continue;

                var foodType = blackboard.Animal.AnimalData.FoodSourceType;
                var sourceIsWrongFoodType = source.FoodType != foodType;
                if (sourceIsWrongFoodType)
                    continue;

                if (foodType == FoodType.Animal && source.GetComponent<Animal>().AnimalData.SpeciesName ==
                    blackboard.Animal.AnimalData.SpeciesName)
                    continue;

                var screenBounds = GlobalBlackboard.Bounds;
                var position = source.transform.position;
                var targetOffScreen = AIHelpers.IsOffScreen(position, screenBounds);
                if (targetOffScreen)
                    continue;
                
                yield return new ActionTarget<FoodSource>
                {
                    Target = source,
                    TargetObject = source.gameObject,
                    TargetLocation = source.transform.position + new Vector3(0, -0.5f, 0)
                };
            }
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var foodTarget = ((ActionTarget<FoodSource>) target).Target;
            if (foodTarget.IsAvailable == false)
                return FloatNormal.Zero;
            
            //If we just moved next to food, don't move to other food immediately
            var lastAction = blackboard.LastSelection.Action;
            if (lastAction != null && lastAction.name == name)
                return FloatNormal.Zero;
            
            //If we're next to the food already, exit early
            var animalPosition = blackboard.Self.transform.position;
            var distanceToFood = Vector3.Distance(animalPosition, target.TargetLocation);
            if (distanceToFood < 0.5f)
                return FloatNormal.Zero;

            //If we're not that hungry, exit early
            var hungerScore = blackboard.Animal.Stats.HungerPercentage;
            if (hungerScore < 0.3f)
                return FloatNormal.Zero;
            
            //Get the hunger after eating this food
            var foodAmount = foodTarget.FoodAmount;
            var restorationScore = foodAmount / 30; //We'll say 30 is average food. if a food is worth more than 30 that's a good bonus to go to it.
            
            //Prioritize Closest food
            var distanceReference = GlobalBlackboard.Bounds.width;
            var closenessScore = Mathf.Max(0.5f, 1f - (distanceToFood / distanceReference));;
            
            var randomScore = Random.Range(0.95f, 1.05f);

            return new FloatNormal(hungerScore * restorationScore * closenessScore * randomScore);
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var foodLocation = target.TargetObject.transform.position + new Vector3(0, -0.5f, 0);
            var transform = blackboard.Self.transform;
            var moveSpeed = blackboard.Animal.AnimalData.WalkSpeed/10;

            var distance = Vector3.Distance(transform.position, foodLocation);
            var travelTime = distance / moveSpeed;
            
            var foodTarget = ((ActionTarget<FoodSource>) target).Target;
            var emoteType = foodTarget.FoodType == FoodType.Animal ? EmoteType.Chase : EmoteType.Hunger;
            AIHelpers.Emote(_emotePrefab, transform, emoteType);
            
            //DoTween Shenanigans. Basically this will make the animal follow the target until the time runs out.
            var moveToTarget = DOTween.To(
                () => transform.position - target.TargetObject.transform.position, 
                x => transform.position = x + target.TargetObject.transform.position, 
                new Vector3(0, -0.5f, 0), 
                travelTime);
            moveToTarget.SetTarget(transform);
            moveToTarget.Play();
            yield return new WaitForSeconds(travelTime);
            
            onComplete?.Invoke();
        }
    }
}