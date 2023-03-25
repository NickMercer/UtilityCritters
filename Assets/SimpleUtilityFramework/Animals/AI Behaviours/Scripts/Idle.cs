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
    [CreateAssetMenu(fileName = "New Idle", menuName = "AI Behaviours/Idle", order = 0)]
    public class Idle : AIBehaviour
    {
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override float Score(AIBlackboard blackboard, ActionTarget target) => 0.25f + Random.Range(-0.1f, 0.1f);

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var idleTime = Random.Range(3f, 6f);
            yield return new WaitForSeconds(idleTime);
            
            onComplete?.Invoke();
        }
    }
}