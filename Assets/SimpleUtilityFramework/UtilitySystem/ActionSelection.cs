using System;

namespace Natick.SimpleUtility
{
    [Serializable]
    public struct ActionSelection
    {
        public IPotentialAction Action;

        public ActionTarget Target;

        public float Score;

        public ActionSelection(IPotentialAction action, ActionTarget target, float score)
        {
            Action = action;
            Target = target;
            Score = score;
        }
    }
}