using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using Natick.Utilities;
using UnityEngine;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Drink", menuName = "AI Behaviours/Drink", order = 0)]
    public class Drink : AIBehaviour
    {
        public override int Priority { get; } = 2;
        
        [SerializeField]
        private float _drinkAnimationSeconds = 2f;

        [SerializeField]
        private float _animationResetSeconds = 1f;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            foreach (var waterSource in AIHelpers.GetInRange<WaterSource>(0.5f, blackboard.Self.transform.position))
            {
                if (waterSource.IsAvailable)
                {
                    yield return new ActionTarget<WaterSource>
                    {
                        Target = waterSource,
                        TargetLocation = waterSource.transform.position,
                        TargetObject = waterSource.gameObject
                    };
                }
            }
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            var water = ((ActionTarget<WaterSource>) target).Target;
            if (water.IsAvailable == false)
                return FloatNormal.Zero;

            var thirstScore = blackboard.Animal.Stats.ThirstPercentage;
            var waterQuality = water.ThirstToQuench / 10f;
            
            return new FloatNormal(thirstScore * waterQuality);
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var animal = blackboard.Animal;
            var water = ((ActionTarget<WaterSource>) target).Target;
            
            animal.AnimateDrinking(_drinkAnimationSeconds);
            water.Occupy();
            yield return new WaitForSeconds(_drinkAnimationSeconds);
            
            water.Consume(blackboard.Animal);
            
            animal.ResetAnimation(_animationResetSeconds);
            yield return new WaitForSeconds(_animationResetSeconds);
            water.Vacate();
            
            onComplete?.Invoke();
        }
    }
}