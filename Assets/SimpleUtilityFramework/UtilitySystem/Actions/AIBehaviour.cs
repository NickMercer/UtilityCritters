using System;
using System.Collections;
using System.Collections.Generic;
using Natick.Utilities;
using UnityEngine;

namespace Natick.SimpleUtility
{
    public abstract class AIBehaviour : ScriptableObject
    {
        public abstract IEnumerable<ActionTarget> GetTargets(AIBlackboard blackboard);
        
        public abstract FloatNormal Score(AIBlackboard blackboard, ActionTarget target);

        public abstract IEnumerator Act(AIBlackboard blackboard, ActionTarget target, Action onComplete);
        public virtual int Priority { get; } = 1;
    }
}