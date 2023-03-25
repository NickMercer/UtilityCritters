using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Natick.SimpleUtility;
using SimpleUtilityFramework.UtilitySystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Return to Area", menuName = "AI Behaviours/Return To Area", order = 0)]
    public class ReturnToArea : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override float Score(AIBlackboard blackboard, ActionTarget target)
        {
            var screenBounds = GlobalBlackboard.Instance.Bounds;
            var position = blackboard.Self.transform.position;

            var outOfHorizontalView = position.x > screenBounds.xMax || position.x < screenBounds.xMin;
            var outOfVerticalView = position.y > screenBounds.yMax || position.y < screenBounds.yMin;
            
            return outOfHorizontalView || outOfVerticalView ? 1f : 0f;
        }

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var wanderTime = Random.Range(2f, 5f);
            var screenBounds = GlobalBlackboard.Instance.Bounds;
            var transform = blackboard.Self.transform;
            var moveSpeed = Random.Range(0, blackboard.Animal.AnimalData.WalkSpeed / 10);
            
            var targetLocation = AIHelpers.CalculateMoveTarget(screenBounds.center, transform.position, moveSpeed, wanderTime);

            transform.DOMove(targetLocation, wanderTime);
            yield return new WaitForSeconds(wanderTime);
            
            onComplete?.Invoke();
        }
    }
}