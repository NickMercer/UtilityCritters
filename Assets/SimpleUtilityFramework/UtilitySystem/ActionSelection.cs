using System;
using SimpleUtilityFramework.UtilitySystem;

namespace Natick.SimpleUtility
{
    [Serializable]
    public struct ActionSelection
    {
        public AIBehaviour Action;

        public ActionTarget Target;

        public float Score;

        public ActionSelection(AIBehaviour action, ActionTarget target, float score)
        {
            Action = action;
            Target = target;
            Score = score;
        }
    }
}