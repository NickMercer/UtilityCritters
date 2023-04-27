using System;
using Natick.Utilities;

namespace Natick.SimpleUtility
{
    [Serializable]
    public struct ActionSelection
    {
        public AIBehaviour Action;

        public ActionTarget Target;

        public FloatNormal Score;

        public ActionSelection(AIBehaviour action, ActionTarget target, FloatNormal score)
        {
            Action = action;
            Target = target;
            Score = score;
        }
    }
}