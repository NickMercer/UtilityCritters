using System;
using System.Collections;
using System.Collections.Generic;
using Natick.SimpleUtility;
using Natick.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SimpleUtilityFramework.Animals.AI_Behaviours
{
    [CreateAssetMenu(fileName = "New Idle", menuName = "AI Behaviours/Idle", order = 0)]
    public class Idle : AIBehaviour
    {
        [SerializeField]
        private float _minIdleSeconds = 3f;

        [SerializeField]
        private float _maxIdleSeconds = 6f;
        
        public override IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard)
        {
            yield return new ActionTarget();
        }

        public override FloatNormal Score(AIBlackboard blackboard, ActionTarget target) => new FloatNormal(0.25f + Random.Range(-0.1f, 0.1f));

        public override IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete)
        {
            var idleTime = Random.Range(_minIdleSeconds, _maxIdleSeconds);
            yield return new WaitForSeconds(idleTime);
            
            onComplete?.Invoke();
        }
    }
}