using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using Natick.Utilities;
using UnityEngine;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Die", menuName = "AI Behaviours/Die", order = 0)]
    public class Die : AIBehaviour
    {
        public override int Priority { get; } = 10;

        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target)
        {
            if (blackboard.Animal.Stats.Health <= 0 && blackboard.Animal.FoodSource.IsAvailable)
                return FloatNormal.One;

            return FloatNormal.Zero;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            blackboard.Self.transform.DORotate(new Vector3(-90, 0, 0), 2f);
            yield return new WaitForSeconds(0.5f);
            Destroy(blackboard.Self);
            
            onComplete?.Invoke();
        }
    }
}