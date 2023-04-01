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
    [CreateAssetMenu(fileName = "New Wander", menuName = "AI Behaviours/Wander", order = 0)]
    public class Wander : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target) => new FloatNormal(0.3f + Random.Range(-0.1f, 0.1f));

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var wanderTime = Random.Range(2f, 5f);
            var direction = Random.insideUnitCircle.normalized;
            var speed = Random.Range(0, blackboard.Animal.AnimalData.WalkSpeed/10);
            var transform = blackboard.Self.transform;
            var targetVector = direction * (speed * wanderTime);
            var targetLocation = transform.position + new Vector3(targetVector.x, targetVector.y, 0);
            
            transform.DOMove(targetLocation, wanderTime);
            yield return new WaitForSeconds(wanderTime);
            
            onComplete?.Invoke();
        }
    }
}