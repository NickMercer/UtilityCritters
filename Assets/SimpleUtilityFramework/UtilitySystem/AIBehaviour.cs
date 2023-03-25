using System;
using System.Collections;
using System.Collections.Generic;
using Natick.SimpleUtility;
using UnityEngine;

namespace SimpleUtilityFramework.UtilitySystem
{
    public abstract class AIBehaviour : ScriptableObject
    {
        public abstract IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard);
        
        public abstract float Score(AIBlackboard blackboard, ActionTarget target);

        public abstract IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete);
    }
}