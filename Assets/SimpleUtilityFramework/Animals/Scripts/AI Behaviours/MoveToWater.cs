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
    [CreateAssetMenu(fileName = "New Move To Water", menuName = "AI Behaviours/Move To Water", order = 0)]
    public class MoveToWater : AIBehaviour
    {
        [SerializeField]
        private Emote _emotePrefab;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            var activeWaterSources = GlobalBlackboard.ActiveWaterSources.WaterSources;
            for (var i = 0; i < activeWaterSources.Count; i++)
            {
                var source = activeWaterSources[i];
                if (source.IsAvailable)
                {
                    yield return new ActionTarget<WaterSource>
                    {
                        Target = source,
                        TargetObject = source.gameObject,
                        TargetLocation = source.transform.position + new Vector3(0, -0.5f, 0)
                    };
                }
            }
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var waterTarget = ((ActionTarget<WaterSource>) target).Target;
            if (waterTarget.IsAvailable == false)
                return FloatNormal.Zero;
            
            //If we just moved next to water, don't move to other water immediately
            var lastAction = blackboard.LastSelection.Action;
            if (lastAction != null && lastAction.name == name)
                return FloatNormal.Zero;
            
            //If we're next to the water already, exit early
            var animalPosition = blackboard.Self.transform.position;
            var distanceToWater = Vector3.Distance(animalPosition, target.TargetLocation);
            if (distanceToWater < 0.5f)
                return FloatNormal.Zero;

            //Get the current Thirst of the animal
            var thirstScore = blackboard.Animal.Stats.ThirstPercentage;
            
            //Get the thirst after eating this food
            var thirstToQuench = waterTarget.ThirstToQuench;
            var restorationScore = thirstToQuench / 10; //We'll say 10 is average water. if a water source is worth more than 10 that's a good bonus to go to it.
            
            //Prioritize Closest water
            var distanceReference = GlobalBlackboard.Bounds.width;
            var closenessScore = Mathf.Max(0.5f, 1f - (distanceToWater / distanceReference));

            //Randomize a bit
            var randomScore = Random.Range(0.90f, 1.1f);
            
            return new FloatNormal(thirstScore * restorationScore * closenessScore * randomScore);
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var waterLocation = target.TargetLocation;
            var transform = blackboard.Self.transform;
            var moveSpeed = blackboard.Animal.AnimalData.WalkSpeed/10;

            var distance = Vector3.Distance(transform.position, waterLocation);
            var travelTime = distance / moveSpeed;

            AIHelpers.Emote(_emotePrefab, transform, EmoteType.Thirst);
            
            transform.DOMove(waterLocation, travelTime);
            yield return new WaitForSeconds(travelTime);
            
            onComplete?.Invoke();
        }
    }
}